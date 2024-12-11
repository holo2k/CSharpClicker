using MediatR;

namespace CSharpClicker.Web.UseCases.ChangeUserName
{
    public record ChangeUserNameCommand(string name) : IRequest<Unit>;
}
