using CSharpClicker.Web.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.ChangeUserName
{
    public class ChangeUserNameCommandHandler : IRequestHandler<ChangeUserNameCommand, Unit>
    {
        private readonly IAppDbContext dbContext;
        private readonly ICurrentUserAccessor currentUserAccessor;
        public ChangeUserNameCommandHandler(IAppDbContext dbContext, ICurrentUserAccessor currentUserAccessor)
        {
            this.dbContext = dbContext;
            this.currentUserAccessor = currentUserAccessor;
        }
        public async Task<Unit> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserAccessor.GetCurrentUserId();

            var user = await dbContext.ApplicationUsers.FirstAsync(user => user.Id == userId, cancellationToken);

            user.UserName = request.name;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
