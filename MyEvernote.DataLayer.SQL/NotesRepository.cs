using System;
using MyEvernote.Model;
using System.Data.SqlClient;
using System.Data;

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
                    note.Id = Guid.NewGuid();
                    command.CommandText = "insert into Note(Id,Title,DateCreated,Creator,Category,Text) values(@id,@title,@dateCreated,@creator,@category,@text)";
                    command.Parameters.AddWithValue("@id", note.Id);
                    command.Parameters.AddWithValue("@name", note.Title);
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
                    command.CommandText = "update Note set Text = @text,Title = @title  where Id = @noteId";
                    command.Parameters.AddWithValue("@text" , note.Text);
                    command.Parameters.AddWithValue("@title", note.Title);
                    command.Parameters.AddWithValue("@noteId", note.Id);
                    command.ExecuteNonQuery();
                }
                
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Changes(Id,DateChange) values(@id,@dateChange)";
                    command.Parameters.AddWithValue("@id", note.Id);
                    command.Parameters.AddWithValue("@dateChange", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }
        public Note Get(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Id,Title,DateCreated,Creator,Category,Text from Note where Id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"note с Id {id} не найденa");
                         
                        var note = new Note
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Created = reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                            Category = reader.GetGuid(reader.GetOrdinal("Category")),
                            Text = reader.GetString(reader.GetOrdinal("Text")),
                            Creator = reader.GetGuid(reader.GetOrdinal("Creator"))
                            
                        };
                        return note;
                    }

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

       
    }
}
