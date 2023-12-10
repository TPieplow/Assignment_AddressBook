using ClassLibrary_AdressBook.Interfaces;
using ClassLibrary_AdressBook.Models;
using ClassLibrary_AdressBook.Services;
using Moq;

namespace AdressBook_Tests;

public class JsonHandling_Tests
{
    [Fact]
    public void JsonReader_Should_ReadAFile_ToApplication()
    {
        // Arrange
        var readerMock = new Mock<IJsonReader>();
        List<IContact> expectedContacts = new List<IContact>()
        {
            new Contact { FirstName = "Ted", LastName = "Pieplow", Email = "ted.pieplow@gmail.com"}
        };
        readerMock.Setup(r => r.LoadFromFile("testfile.json")).Returns(expectedContacts);

        // Act
        IEnumerable<IContact> actualContacts = readerMock.Object.LoadFromFile("testfile.json");

        // Assert

        Assert.Equal(expectedContacts, actualContacts);
    }

    [Fact]
    public void JsonWriter_Should_WriteAFile_ToFolder()
    {
        // Arrange

        // Act

        // Assert
    }
}
