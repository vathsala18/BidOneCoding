using BidOneCoding.Controllers;
using BidOneCoding.Models;
using BidOneCoding.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BidOneCoding.Tests.Controller
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonRepository> _mockRepository;
        private readonly PersonController _personController;
        private readonly Mock<ILogger<PersonController>> _mockLogger; 

        public PersonControllerTests()
        {
            _mockRepository = new Mock<IPersonRepository>();
            _mockLogger = new Mock<ILogger<PersonController>>();
            _personController = new PersonController(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _personController.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsExactNumberOfPersons()
        {
            _mockRepository.Setup(repo => repo.GetAll())
                .Returns(new List<Person>() { new Person(), new Person(), new Person() });

            var result = _personController.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var persons = Assert.IsType<List<Person>>(viewResult.Model);

            Assert.Equal(3, persons.Count);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {
            var result = _personController.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_InvalidModelState_CreatePersonNeverExecutes()
        {
            _personController.ModelState.AddModelError("FirstName", "FirstName is required");

            var person = new Person { LastName = "Grace" };

            _personController.Create(person);

            _mockRepository.Verify(x => x.CreatePerson(It.IsAny<Person>()), Times.Never);
        }

        [Fact]
        public void Create_ValidModelState_CreatePersonCalledOnce()
        {
            Person? pers = null;

            _mockRepository.Setup(r => r.CreatePerson(It.IsAny<Person>()))
                .Callback<Person>(x => pers = x);

            var person = new Person
            {
                FirstName = "Ann",
                LastName = "Claire"
            };

            _personController.Create(person);
            _mockRepository.Verify(x => x.CreatePerson(It.IsAny<Person>()), Times.Once);

            Assert.Equal(pers.FirstName, person.FirstName);
            Assert.Equal(pers.LastName, person.LastName);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectToIndexAction()
        {
            var person = new Person
            {
                FirstName = "Angel",
                LastName = "Bob"
            };

            var result = _personController.Create(person);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index" , redirectToActionResult.ActionName);
        }
    }
}
