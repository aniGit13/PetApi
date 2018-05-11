using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetApi.Services;
using PetApiRepository;

namespace PetApi.Tests.Services
{
    [TestClass]
    public class PetServiceTest
    {
        private PetService<Owners> _petService;

        [TestInitialize]
        public void TestSetup()
        {
            var petRepositoryMock = new Mock<IRepository<Owners>>();
            const string testDataString = @"[{""name"":""Bob"",""gender"":""Male"",""age"":23,""pets"":[{""name"":""Garfield"",""type"":""Cat""},{""name"":""Fido"",""type"":""Dog""}]},{""name"":""Jennifer"",""gender"":""Female"",""age"":18,""pets"":[{""name"":""Garfield"",""type"":""Cat""}]},{""name"":""Steve"",""gender"":""Male"",""age"":45,""pets"":null},{""name"":""Fred"",""gender"":""Male"",""age"":40,""pets"":[{""name"":""Tom"",""type"":""Cat""},{""name"":""Max"",""type"":""Cat""},{""name"":""Sam"",""type"":""Dog""},{""name"":""Jim"",""type"":""Cat""}]},{""name"":""Samantha"",""gender"":""Female"",""age"":40,""pets"":[{""name"":""Tabby"",""type"":""Cat""}]},{""name"":""Alice"",""gender"":""Female"",""age"":64,""pets"":[{""name"":""Simba"",""type"":""Cat""},{""name"":""Nemo"",""type"":""Fish""}]}]";
            var testData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Owners>>(testDataString);
            petRepositoryMock.Setup(x => x.GetAll()).Returns(testData);
            _petService = new PetService<Owners>(petRepositoryMock.Object);
        }

        [TestMethod]
        public void TestGetAllResultShouldHaveMaleAndFemaleGroups()
        {
            var result = _petService.GetAll();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Male", result[0].Gender);
            Assert.AreEqual("Female", result[1].Gender);
        }

        [TestMethod]
        public void TestGetAllResultShouldHave4CatsUnderMaleGroups()
        {
            var result = _petService.GetAll();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(4, result[0].Cats.Count);
        }
    }
}
