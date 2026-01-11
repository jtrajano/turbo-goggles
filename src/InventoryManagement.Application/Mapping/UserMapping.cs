using InventoryManagement.Application.DTOs;
using InventoryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Mapping;

public static class UserMapping
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto(
            user.Id,
            $"{user.FirstName} {user.LastName}",
            user.Status,
            user.Role,
            user.Email,
            user.Avatar,
            user.LastActive.ToString("o")
         );
    }
}
