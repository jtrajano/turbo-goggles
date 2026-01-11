using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.Users;


public record GetByIdQuery(Guid id) : IRequest<UserDto>;

public class GetByIdQueryHandler(IUserRepository _repository) : IRequestHandler<GetByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        // Implementation to retrieve all users goes here.
        var users = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (users == null)
        {
            throw new KeyNotFoundException($"User with ID {request.id} not found.");
        }
        return users.ToDto();
    }
}
