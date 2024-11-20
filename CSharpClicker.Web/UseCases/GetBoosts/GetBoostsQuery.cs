using CSharpClicker.Web.Domain;
using MediatR;
using System.Linq;
using System.Security.Claims;

namespace CSharpClicker.Web.UseCases.GetBoosts
{
    public record GetUserQuery : IRequest<IReadOnlyCollection<BoostDto>>;
}
