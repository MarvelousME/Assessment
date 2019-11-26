using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Phonebook.Database;
using Phonebook.Repository;
using Phonebook.Service;
using Phonebook.Web.ViewModels;

namespace Phonebook.Test.Services
{
    [TestClass]
    public class ServiceTests
    {
        private Mock<IPhoneBookService> _phonebookServiceMock;
        private Mock<IPhoneBookTypeService> _phonebookTypeServiceMock;
        private Mock<IPhoneBookEntryService> _phonebookEntryServiceMock;

        private Mock<IPhoneBookRepository> _mockPhoneBookRepository;
        private Mock<IPhoneBookTypeRepository> _mockPhoneBookTypeRepository;
        private Mock<IPhoneBookEntryRepository> _mockPhoneBookEntryRepository;

        private IPhoneBookRepository _phoneBookRepository;
        private IPhoneBookTypeRepository _phoneBookTypeRepository;
        private IPhoneBookEntryRepository _phoneBookEntryRepository;

        Mock<IUnitOfWork> _mockUnitWork;

        List<PhoneBook> listContacts;
        List<PhoneBookType> listPhoneTypes;
        List<PhoneBookEntry> listPhonebookEntries;
        List<PhoneNumberEditorViewModel> listNumbers;

        PhonebookEditViewModel _phonebookEditViewModel;


        [TestInitialize]
        public void Initialize()
        {

            _phonebookServiceMock = new Mock<IPhoneBookService>();
            _phonebookTypeServiceMock = new Mock<IPhoneBookTypeService>();
            _phonebookEntryServiceMock = new Mock<IPhoneBookEntryService>();


            listContacts = new List<PhoneBook>() {
             new PhoneBook() { Id = 1, Name = "Marvin", Surname = "Saunders"},
             new PhoneBook() { Id = 2, Name = "Lloyd",  Surname = "Vere" },
             new PhoneBook() { Id = 3, Name = "Keagan", Surname = "Lubbe" }
            };

            listPhoneTypes = new List<PhoneBookType>() {
             new PhoneBookType() { Id = 1, Description = "Home" },
             new PhoneBookType() { Id = 2, Description = "Work" },
             new PhoneBookType() { Id = 3, Description = "Cell" }
            };

            listPhonebookEntries = new List<PhoneBookEntry>() {
             new PhoneBookEntry() { Id = 1,  PhoneBookId = 1, PhoneBookTypeId = 1, Number = "0815411236" },
             new PhoneBookEntry() { Id = 2,  PhoneBookId = 2, PhoneBookTypeId = 2, Number = "0815411236" },
             new PhoneBookEntry() { Id = 3,  PhoneBookId = 3, PhoneBookTypeId = 3, Number = "0815411236" },
             new PhoneBookEntry() { Id = 4,  PhoneBookId = 1, PhoneBookTypeId = 3, Number = "0815411236" },
             new PhoneBookEntry() { Id = 5,  PhoneBookId = 1, PhoneBookTypeId = 1, Number = "0815411236" },
             new PhoneBookEntry() { Id = 6,  PhoneBookId = 2, PhoneBookTypeId = 2, Number = "0815411236" }
            };

            listNumbers = new List<PhoneNumberEditorViewModel>()
            {
                new PhoneNumberEditorViewModel() { PhoneBookTypeId = "1", Number="0216657874"},
                new PhoneNumberEditorViewModel() { PhoneBookTypeId = "2", Number="0214563214"},
                new PhoneNumberEditorViewModel() { PhoneBookTypeId = "3", Number="0815400171"}
            };
        }

        [TestMethod]
        public void Contacts_Get_All()
        {
            //Arrange
            _phoneBookRepository.Setup(x => x.GetAll()).Returns(listCountry);

            //Act
            List<Country> results = _service.GetAll() as List<Country>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }
    }
}
