using AutoMapper;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetUserSettings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserDto>
    {
        private readonly IMapper mapper;
        private readonly IAppDbContext appDbContext;
        private readonly ICurrentUserAccessor currentUserAccessor;

        public GetUserProfileQueryHandler(IMapper mapper, IAppDbContext appDbContext, ICurrentUserAccessor currentUserAccessor)
        {
            this.mapper = mapper;
            this.appDbContext = appDbContext;
            this.currentUserAccessor = currentUserAccessor;
        }

        public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {;

            var user = await appDbContext.ApplicationUsers
                .Include(user => user.UserBoosts)
                .ThenInclude(ub => ub.Boost)
                .FirstAsync(user => user.Id == request.id);

            return mapper.Map<UserDto>(user);
        }
    }
}
