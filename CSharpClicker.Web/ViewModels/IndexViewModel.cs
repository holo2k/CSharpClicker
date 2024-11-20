using CSharpClicker.Web.UseCases.GetBoosts;

namespace CSharpClicker.Web.ViewModels
{
    public class IndexViewModel
    {
        public UserDto User { get; init; }
        public IReadOnlyCollection<BoostDto> Boosts { get; init; }
        public long OrcHealth { get; set; } = 100;
    }
}
