using AutoMapper;
using CSharpClicker.Web.Domain;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetCurrentUser;
using CSharpClicker.Web.UseCases.GetLeaderboard;
using CSharpClicker.Web.UseCases.GetUserSettings;

namespace CSharpClicker.Web.UseCases
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Boost, BoostDto>();
            CreateMap<UserBoost, UserBoostDto>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<ApplicationUser, LeaderboardUserDto>();
            CreateMap<ApplicationUser, UserSettingsDto>();
        }
    }
}
