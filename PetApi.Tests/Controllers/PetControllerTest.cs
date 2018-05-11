using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetApi.Controllers;
using PetApi.Services;
using PetApiRepository;

namespace PetApi.Tests.Controllers
{
    [TestClass]
    public class PetControllerTest
    {
        private PetController controller;

        [TestInitialize]
        public void TestSetup()
        {
            var petRepositoryMock = new Mock<IRepository<Owners>>();
            const string testDataString = @"[{""name"":""Bob"",""gender"":""Male"",""age"":23,""pets"":[{""name"":""Garfield"",""type"":""Cat""},{""name"":""Fido"",""type"":""Dog""}]},{""name"":""Jennifer"",""gender"":""Female"",""age"":18,""pets"":[{""name"":""Garfield"",""type"":""Cat""}]},{""name"":""Steve"",""gender"":""Male"",""age"":45,""pets"":null},{""name"":""Fred"",""gender"":""Male"",""age"":40,""pets"":[{""name"":""Tom"",""type"":""Cat""},{""name"":""Max"",""type"":""Cat""},{""name"":""Sam"",""type"":""Dog""},{""name"":""Jim"",""type"":""Cat""}]},{""name"":""Samantha"",""gender"":""Female"",""age"":40,""pets"":[{""name"":""Tabby"",""type"":""Cat""}]},{""name"":""Alice"",""gender"":""Female"",""age"":64,""pets"":[{""name"":""Simba"",""type"":""Cat""},{""name"":""Nemo"",""type"":""Fish""}]}]";
            var testData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Owners>>(testDataString);
            petRepositoryMock.Setup(x => x.GetAll()).Returns(testData);
            var petService = new PetService<Owners>(petRepositoryMock.Object);
            controller = new PetController(petService);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var result = controller.GetAll();
            Assert.AreEqual(result.GetType().Name, "OkNegotiatedContentResult`1");
        }
    }
}
