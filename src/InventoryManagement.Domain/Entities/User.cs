using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Entities;

public class User : IBaseEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Status { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string Avatar { get; set; }
    public DateTime LastActive { get; set; }


    //public string PasswordHash { get; set; }
}
