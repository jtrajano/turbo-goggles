using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Status,
    string Role,
    string Email,
    string Avatar,
    string LastActive
);

