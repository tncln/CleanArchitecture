using CleanArchitecture.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.AuthFeatures.Command
{
    public record RegisterCommand(
        string Email,
        string UserName,
        string NameLastName, 
        string Password): IRequest<MessageResponse>;
}
