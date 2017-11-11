﻿using System;
using MyEvernote.Model;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MyEvernote.DataLayer.SQL
{
    public class NotesRepository : INote
    {
        readonly string _ConnectionString;
        public NotesRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public Note Create(Note note)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    note.Id = note.Id == null ? Guid.NewGuid() : note.Id; 
                    command.CommandText = "insert into Note(Id,Title,DateCreated,Creator,Category,Text) values(@id,@title,@dateCreated,@creator,@category,@text)";
                    command.Parameters.AddWithValue("@id", note.Id);
                    command.Parameters.AddWithValue("@dateCreated", DateTime.Now);
                    command.Parameters.AddWithValue("@creator", note.Creator);
                    command.Parameters.AddWithValue("@category", note.Category);
                    command.Parameters.AddWithValue("@Title", note.Title);
                    command.Parameters.AddWithValue("@Text", note.Text);
                    command.ExecuteNonQuery();
                }
            }
            return note;
        }
        public void Change(Note note)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    
                    // Create transaction
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction(); //"Get Note Transaction"
                    command.Connection = connection;
                    command.Transaction = transaction; 

                    // First command
                    command.CommandText = "update Note set Text = @text,Title = @title  where Id = @noteId";
                    command.Parameters.AddWithValue("@text" , note.Text);
                    command.Parameters.AddWithValue("@title", note.Title);
                    command.Parameters.AddWithValue("@noteId", note.Id);
                    command.ExecuteNonQuery();

                    // Second command
                    command.CommandText = " if not exists(select Id from Changes where Id = @id) insert into Changes(Id,DateChange) values(@id,@dateChange) else update  Changes set DateChange = @dateChange where Id = @id";
                    command.Parameters.AddWithValue("@id", note.Id);
                    command.Parameters.AddWithValue("@dateChange", DateTime.Now);
                    command.ExecuteNonQuery();

                    
                    transaction.Commit();
                }
            }
        }
        public List<Note> GetNotes(Guid UserId)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                var Notes = new List<Note>();
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction(); 
                    command.Connection = connection;
                    command.Transaction = transaction;
                    
                    // Первая команда - забрать основную инфорцию о заметках
                    command.CommandText = "select * from Note where Creator = @id";
                    command.Parameters.AddWithValue("@id", UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        /*if (!reader.HasRows)
                            throw new ArgumentException($"note с Id {UserId} не найденa");*/

                        while (reader.Read())
                        {
                            var note = new Note
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Created = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                Category = reader.GetGuid(reader.GetOrdinal("Category")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),
                                Creator = reader.GetGuid(reader.GetOrdinal("Creator")),
                            };
                            Notes.Add(note);
                        }
                    }

                    // забрать пошаренные заметки  
                   /* command.CommandText = "select * from Note where id in (select NoteId from Shared where	UserId =  @id_)";
                    command.Parameters.AddWithValue("@id_", UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var note = new Note
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Created = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                Category = reader.GetGuid(reader.GetOrdinal("Category")),
                                Text = reader.GetString(reader.GetOrdinal("Text")),
                                Creator = reader.GetGuid(reader.GetOrdinal("Creator")),
                            };
                            Notes.Add(note);
                        }
                    }*/

                    /*if (Notes == null)// ЕСЛИ У ПОЛЬЗОВАТЕЛЯ нет заметок разу выходим .
                        return Notes;*/

                    // тертья команда - забрать инфорцию о владельцах заметок
                    // забираем все айди всех нот юзера , лезем  в шаред и выбираем юзеров 
                    command.CommandText = "select * from Shared where NoteId in (select Id from Note where Creator = @idd)";

                    command.Parameters.AddWithValue("@idd", UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var i = 0;
                            Notes[i].Shared = new List<Guid>();
                            Notes[i].Shared.Add(reader.GetGuid(reader.GetOrdinal("UserId")));
                            i++;
                        }
                    }
                    // четвертая команда - забрать инфорцию об изменениях
                    command.CommandText = "select DateChange from Changes where id in (select Id from Note where Creator = @iddd)";
                    command.Parameters.AddWithValue("@iddd", UserId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var i = 0;
                            Notes[i].Changed = reader.GetDateTime(reader.GetOrdinal("DateChange"));
                            i++;
                        }
                    }

                    transaction.Commit();
                    return Notes;
                }
            }
        }
        public void Share(Guid NoteId , Guid UserId)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Shared (NoteId , UserId) values (@NoteId , @UserId)";
                    command.Parameters.AddWithValue("@NoteId" , NoteId);
                    command.Parameters.AddWithValue("@UserId" , UserId);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Shared where NoteId=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Note where Id=@idd";
                    command.Parameters.AddWithValue("@idd", id);
                    command.ExecuteNonQuery();
                }

            }
        }

        public Note GetNote(Guid NoteId)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    var note = new Note();

                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction(); 
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandText = "select * from Note where Id = @noteId";
                    command.Parameters.AddWithValue("@noteId", NoteId);
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        note.Id = reader.GetGuid(reader.GetOrdinal("Id"));
                        note.Title = reader.GetString(reader.GetOrdinal("Title"));
                        note.Created = reader.GetDateTime(reader.GetOrdinal("DateCreated"));
                        note.Category = reader.GetGuid(reader.GetOrdinal("Category"));
                        note.Text = reader.GetString(reader.GetOrdinal("Text"));
                        note.Creator = reader.GetGuid(reader.GetOrdinal("Creator"));
                    }
                    // Вторая команда - забрать инфорцию о владельцах заметки
                    command.CommandText = "select UserId from Shared where NoteId  = @idd";
                    command.Parameters.AddWithValue("@idd", NoteId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var i = 0;
                            note.Shared = new List<Guid>();
                            note.Shared.Add(reader.GetGuid(reader.GetOrdinal("UserId")));
                            i++;
                        }
                    }

                    // Третья команда - забрать инфорцию об изменениях
                    command.CommandText = "select DateChange from Changes where id = @iddd";
                    command.Parameters.AddWithValue("@iddd", NoteId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var i = 0;
                            note.Changed = reader.GetDateTime(reader.GetOrdinal("DateChange"));
                            i++;
                        }
                    }


                    transaction.Commit();
                    return note;
                }
            }
        }
    }
}