using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Database;
using Phonebook.Repository;
using Phonebook.Service;
using Phonebook.Web.ViewModels;
using Phonebook.Web.Controllers;
using Phonebook.Web.Controllers.Api;
using PhonebookTestController = Phonebook.Web.Controllers.PhonebookController;
using PhonebookApiController = Phonebook.Web.Controllers.Api.PhonebookController;
using Phonebook.Web.Dtos;

namespace Phonebook.Test.Controllers
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<IPhoneBookService> _phonebookServiceMock;
        private Mock<IPhoneBookTypeService> _phonebookTypeServiceMock;
        private Mock<IPhoneBookEntryService> _phonebookEntryServiceMock;

        PhonebookTestController _phoneBookController;
        PhonebookApiController _phoneBookApiController;

        List<PhoneBook> listContacts;
        List<PhoneBookType> listPhoneTypes;
        List<PhoneNumberEditorViewModel> listNumbers;

        PhonebookDto phonebookDto;

        PhonebookEditViewModel _phonebookEditViewModel;


        [TestInitialize]
        public void Initialize()
        {

            _phonebookServiceMock = new Mock<IPhoneBookService>();
            _phonebookTypeServiceMock = new Mock<IPhoneBookTypeService>();
            _phonebookEntryServiceMock = new Mock<IPhoneBookEntryService>();

            _phoneBookController = new PhonebookTestController(_phonebookServiceMock.Object, _phonebookEntryServiceMock.Object, _phonebookTypeServiceMock.Object);
            _phoneBookApiController = new PhonebookApiController(_phonebookServiceMock.Object, _phonebookEntryServiceMock.Object, _phonebookTypeServiceMock.Object);

            listPhoneTypes = new List<PhoneBookType>() {
             new PhoneBookType() { Id = 1, Description = "Home" },
             new PhoneBookType() { Id = 2, Description = "Work" },
             new PhoneBookType() { Id = 3, Description = "Cell" }
            };

            listNumbers = new List<PhoneNumberEditorViewModel>()
            {
                new PhoneNumberEditorViewModel() { PhoneBookTypeId = "1", Number="0216657874"},
                new PhoneNumberEditorViewModel() { PhoneBookTypeId = "2", Number="0214563214"},
                new PhoneNumberEditorViewModel() { PhoneBookTypeId = "3", Number="0815400171"}
            };
        }

        [TestMethod]
        public void PhoneTypes_Get_All()
        {
            _phonebookTypeServiceMock.Setup(x => x.GetAll()).Returns(listPhoneTypes);

            var result = ((_phoneBookController.Index() as ViewResult).Model) as List<PhoneBookType>;

            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("Home", result[0].Description);
            Assert.AreEqual("Work", result[1].Description);
            Assert.AreEqual("Cell", result[2].Description);

        }

        [TestMethod]
        public void Valid_Contact_Create()
        {
            _phoneBookController = new PhonebookTestController();

            var contact = new PhoneBook() { Name = "Alwyn", Surname="Underwood", CreatedDate=DateTime.Now };

            _phonebookEditViewModel = new PhonebookEditViewModel
            {
                FirstName = contact.Name,
                LastName = contact.Surname,
                Numbers = listNumbers
            };

            var result = (RedirectToRouteResult)_phoneBookController.Save(_phonebookEditViewModel);

            _phonebookServiceMock.Verify(m => m.Create(contact), Times.Once);

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Invalid_Contact_Create()
        {
            _phoneBookController.ModelState.AddModelError("Error", "Something went wrong when trynig to save contact to Phonebook!");

            _phonebookEditViewModel = new PhonebookEditViewModel
            {
                FirstName = "",
                LastName = "",
                Numbers = null
            };

            var result = (ViewResult)_phoneBookController.Save(_phonebookEditViewModel);

            _phonebookServiceMock.Verify(m => m.Create(new PhoneBook()), Times.Never);
            Assert.AreEqual("", result.ViewName);
        }
    }
}
