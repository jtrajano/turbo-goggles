using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Mapping;
using InventoryManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Queries.Users;

public record SearchByTextQuery (string text): IRequest<IEnumerable<UserDto>>;

public class SearchByTextQueryHandler(IUserRepository _repository) : IRequestHandler<SearchByTextQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(SearchByTextQuery request, CancellationToken cancellationToken)
    {
        var data = await _repository.SearchByText(request.text);

        return data.Select(user => user.ToDto()).ToList().AsReadOnly();
      
    }
}
