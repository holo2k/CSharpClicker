using AutoMapper;
using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.Login;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CSharpClicker.Web.UseCases.GetBoosts
{
    public class GetBoostsQueryHandler : IRequestHandler<GetBoostsQuery, IReadOnlyCollection<BoostDto>>
    {
        private IAppDbContext appDbContext;
        private IMapper mapper;

        public GetBoostsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        public async Task<IReadOnlyCollection<BoostDto>> Handle(GetBoostsQuery request, CancellationToken cancellationToken)
        {
            return await mapper
                .ProjectTo<BoostDto>(appDbContext.Boosts)
                .ToArrayAsync();
        }
    }
}
