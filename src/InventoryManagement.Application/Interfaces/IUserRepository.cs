using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> SearchByText(string text);
}
