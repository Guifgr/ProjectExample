using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BRW.Domain.Entities;

public class User : Basis
{
    public string Email { get; set; }
    public string Password { get; set; }
    
}
