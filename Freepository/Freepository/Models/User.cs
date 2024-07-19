using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Freepository.Models;

public class User : IdentityUser
{
  public string FirstName { get; set; }
  public string LastName { get; set; }

    public ICollection<Resource> Resources { get; set; } = [];

    public User()
    {
        
    }

    public User(string userName, string email) : base(userName)
    {
        Email = email;
        UserName = userName;
    }
}