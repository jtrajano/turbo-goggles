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

public record GetAllQuery() : IRequest<IEnumerable<UserDto>>;

public class GetAllQueryHandler(IUserRepository _repository) : IRequestHandler<GetAllQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        // Implementation to retrieve all users goes here.
        var users = await _repository.GetAllAsync(cancellationToken);

        return users.Select(user => user.ToDto()).ToList().AsReadOnly();
    }
}
