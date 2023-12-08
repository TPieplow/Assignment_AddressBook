﻿using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Services;
using Moq;

namespace AdressBook_Tests;

public class ContactService_Tests
{
    [Fact]
    public void AddContact_Should_AddOneContactToContactList_ThenReturnTrue()
    {
        //Arrange
        var writerMock = new Mock<IJsonWriter>();
        IContact contact = new Contact { FirstName = "Ted", LastName = "Pieplow", Email = "ted@gmail.com", Address = "Vildsvinsvägen 23", Phone = "0763233614" };
        IContactService contactService = new ContactService(writerMock.Object);

        //Act
        bool result = contactService.AddContact(contact);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void AddContact_Should_NotAddContactToList_WhenContactAlreadyExists()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        var contactService = new ContactService(writerMock.Object);
        var testContact = new Contact { Email = "test@test.se" };

        // Act
        contactService.AddContact(testContact);
        var result = contactService.AddContact(testContact);

        // Assert
        Assert.False(result);
        Assert.Single(contactService.GetContacts());
    }

    [Fact]
    public void AddContact_Should_ReturnFalse_WhenExceptionOccurs()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        writerMock.Setup(w => w.SaveToFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<IContact>>())).Throws(new Exception("Simulated exception"));

        // Act
        IContact contact = new Contact { FirstName = "Ted", LastName = "Pieplow", Email = "ted@gmail.com", Address = "Vildsvinsvägen 23", Phone = "0763233614" };
        IContactService contactService = new ContactService(writerMock.Object);

        // Assert
        bool result = contactService.AddContact(contact);
    }

    [Fact]
    public void GetContacts_Should_GetAllContactssFromList_ThenReturnListOfContacts()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        IContactService contactService = new ContactService(writerMock.Object);
        IContact contact = new Contact { FirstName = "Ted", LastName = "Pieplow", Email = "ted@gmail.com", Address = "Vildsvinsvägen 23", Phone = "0763233614" };
        contactService.AddContact(contact);

        // Act
        IEnumerable<IContact> result = contactService.GetContacts();

        // Assert
        Assert.NotNull(result);
        Assert.True(((IEnumerable<Contact>)result).Any());

    }
}
