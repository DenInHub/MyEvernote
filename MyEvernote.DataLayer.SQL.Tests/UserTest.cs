using System;
using MyEvernote.Model;
using MyEvernote.DataLayer.SQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NLog;
using MyEvernote.Logger;


namespace MyEvernote.DataLayer.SQL.Tests
{
    [TestClass]
    public class UserTest
    {
        #region var
        string _connectionstring = @"Data Source=DESKTOP-IC679A3;Initial Catalog=MyEvernote;Integrated Security=True";
        static  List<Guid> UsersForTests = new List<Guid>();
        static  List<Guid> CategoriesForTests = new List<Guid>();
        static  List<Guid> NotesForTests = new List<Guid>();
        
        #endregion
        [TestMethod]
        public void ShouldCreateUser()
        {
            Log.Instance.Info("from test user");//
            //arrange
            var user = new User
            {
                Name = "TestCreate"
            };

            var repository = new UsersRepository(_connectionstring);
            //act
            var result = repository.Create(user);
            UsersForTests.Add(result.Id);
            var UserFromDB = repository.Get(result.Id);
            //assert
            Assert.AreEqual(result.Name, UserFromDB.Name);

            //ShouldCleanUp();
        }

        [TestMethod]
        public void ShouldDeleteUser()
        {
            //arrange
            var flag=false;
            var user = new User
            {
                Name = "testDelete"
            };
            var repository = new UsersRepository(_connectionstring);
            var CreatedUser = repository.Create(user);

            //act
            repository.Delete(CreatedUser.Id);
            var UserNotFound = repository.Get(CreatedUser.Id);
            //assert

            Assert.AreEqual(null, UserNotFound);
        }

        [TestMethod]
        public void ShouldCreateCategory()
        {
            //arrange
            var category = new Category()
            {
                Name = "TestCategory"
            };
            var repository = new CategoriesRepository(_connectionstring);
            
            //act
            var CreatedCategory = repository.Create(category);
            CategoriesForTests.Add(CreatedCategory.Id);
            var CategoryFromDB = repository.Get(CreatedCategory.Id);
            
            //assert
            Assert.AreEqual(CreatedCategory.Id,CategoryFromDB.Id);
        }

        [TestMethod]
        public void ShouldCreateNote()
        {
            //arrange
            var Categ = new CategoriesRepository(_connectionstring).Create(new Category() { Name = "TestCategory", Id = Guid.NewGuid() });
            var GuidCreator = new UsersRepository(_connectionstring).Create(new User() { Name = "TestUser" }).Id;
            var GuidCategory = Categ.Id;
            UsersForTests.Add(GuidCreator);
            CategoriesForTests.Add(GuidCategory);
            var note = new Note()
            {
                Title = "TestNote",
                Text = "TestText",
                Creator = GuidCreator,
                Category = GuidCategory
            };
            var repository = new NotesRepository(_connectionstring);

            //act
            var CreatedNote = repository.Create(note);
            NotesForTests.Add(CreatedNote.Id);
            var NoteFromDB = repository.GetNote(CreatedNote.Id);

            //assert
            Assert.AreEqual(CreatedNote.Id, NoteFromDB.Id);
        }
        
        [TestMethod]
        public void ShouldGetAllUsers()
        {
            var AllUsersFromDB = new UsersRepository(_connectionstring).GetAll();
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void ShouldCleanUp()
        {
            var NoteRepository = new NotesRepository(_connectionstring);
            foreach (var id in NotesForTests)
                NoteRepository.Delete(id);

            var UsersRepository = new UsersRepository(_connectionstring);
            foreach (var id in UsersForTests)
                UsersRepository.Delete(id);

            var CategoriesRepository = new CategoriesRepository(_connectionstring);
            foreach (var id in CategoriesForTests)
                CategoriesRepository.Delete(id);

        }
    }
}
