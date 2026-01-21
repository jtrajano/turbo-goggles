using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace InventoryManagement.Application.Interfaces;

public interface IApplicationDbContext
{
     DbSet<User> Users { get; set; }
     DbSet<Product> Products { get; set; }
    DbSet<TEntity> Set<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields | DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties | DynamicallyAccessedMemberTypes.Interfaces)] TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
     

}
