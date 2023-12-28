using System.ComponentModel.DataAnnotations;
using ClassLibrary_AdressBook.Interfaces;
using Newtonsoft.Json;

namespace ClassLibrary_AdressBook.Models;

public class Contact : IContact
{
    public Contact()
    {

    }

    public Contact(string firstName, string lastName, string email, string phone, string address, Guid id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        Id = id;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [JsonProperty("firstName")]
    public string FirstName { get; set; } = null!;
    [Required]
    [JsonProperty("lastName")]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    [JsonProperty("phone")]
    public string Phone { get; set; } = null!;
    [Required]
    [JsonProperty("address")]
    public string Address { get; set; } = null!;
}
