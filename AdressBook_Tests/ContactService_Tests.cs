using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Services;
using Moq;
using System.Diagnostics;

namespace AdressBook_Tests;

public class ContactService_Tests
{
    [Fact]
    public void AddContact_Should_AddOneContactToContactList_ThenReturnTrue()
    {
        //Arrange
        var writerMock = new Mock<IJsonWriter>();
        IContact contact = new Contact { FirstName = "Ted", LastName = "Pieplow", Email = "ted@gmail.com", Address = "Vildsvinsvägen 23", Phone = "0763233614" };
        IContactService contactService = new ContactService(writerMock.Object, new List<IContact>());

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
        IContactService contactService = new ContactService(writerMock.Object, new List<IContact>());
        IContact contact = new Contact { Email = "test@test.se" };

        // Act
        contactService.AddContact(contact);
        var result = contactService.AddContact(contact);

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
        IContactService contactService = new ContactService(writerMock.Object, new List<IContact>());

        // Assert
        bool result = contactService.AddContact(contact);
    }

    [Fact]
    public void GetContact_Should_Return_SpecificContact_UsingEmail()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        var expectedContact = new Mock<IContact>();
        expectedContact.SetupGet(c => c.Email).Returns("test@test.com");

        var contactService = new ContactService(writerMock.Object, new List<IContact> { expectedContact.Object });

        // Act
        var result = contactService.GetContact("test@test.com");

        // Assert
        Assert.Equal(expectedContact.Object, result);
    }

    [Fact]
    public void GetContact_Should_ReturnNull_WhenContactNotFound()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        var contactService = new ContactService(writerMock.Object, new List<IContact>());

        // Act
        var result = contactService.GetContact("thismaildoesntexist@test.com");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetContact_Should_HandleExeption_AndReturnNull()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();

        var contactService = new ContactService(writerMock.Object, new List<IContact>());

        // Act
        var result = contactService.GetContact("test@test.com");

        // Assert
        Assert.Null(result);

    }

    [Fact]
    public void GetContacts_Should_GetAllContactssFromList_ThenReturnListOfContacts()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        IContactService contactService = new ContactService(writerMock.Object, new List<IContact>());
        IContact contact = new Contact { FirstName = "Ted", LastName = "Pieplow", Email = "ted@gmail.com", Address = "Vildsvinsvägen 23", Phone = "0763233614" };
        contactService.AddContact(contact);

        // Act
        IEnumerable<IContact> result = contactService.GetContacts();

        // Assert
        Assert.NotNull(result);
        Assert.True(((IEnumerable<Contact>)result).Any());
    }

    [Fact]
    public void UpdateContact_Should_UpdateCurrentContact()
    {
        // Arrange
        var writerMock = new Mock<IJsonWriter>();
        IContactService contactService = new ContactService(writerMock.Object, new List<IContact>());
        IContact initialContact = new Contact { FirstName = "Initial", LastName = "Last name", Email = "test@test.com" };
        contactService.AddContact(initialContact);

        IContact updatedContact = new Contact { FirstName = "Updated", LastName = "Last name", Email = "test@test.com" };

        // Act
        contactService.UpdateContact(updatedContact, "whateverfilename");

        // Assert
        IContact result = contactService.GetContact("test@test.com");

        Assert.NotNull(result);
        Assert.Equal("Updated", result.FirstName);
    }

}
