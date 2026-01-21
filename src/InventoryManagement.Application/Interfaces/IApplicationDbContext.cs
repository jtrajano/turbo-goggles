using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Interfaces;

public interface IApplicationDbContext
{
     DbSet<User> Users { get; set; }
     DbSet<Product> Products { get; set; }
}
