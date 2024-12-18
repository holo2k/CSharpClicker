using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetUserSettings;
using MediatR;

namespace CSharpClicker.Web.UseCases.GetUserProfile
{
    public record GetUserProfileQuery(Guid id) : IRequest<UserDto>;
}
