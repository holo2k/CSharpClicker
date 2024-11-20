using CSharpClicker.Web.Domain;
using System.Runtime.CompilerServices;

namespace CSharpClicker.Web.DomainServices
{
    public static class BoostsProfitCalculationExtensions
    {
        public static long GetProfit(this IEnumerable<UserBoost> userBoosts, bool shouldCalculateAutoBoosts = false)
        {
            if (shouldCalculateAutoBoosts)
            {
                return userBoosts
                    .Where(ub => ub.Boost.IsAuto)
                    .Sum(ub => ub.Quantity * ub.Boost.Profit);
            }

            return 1 + userBoosts
                    .Where(ub => !ub.Boost.IsAuto)
                    .Sum(ub => ub.Quantity * ub.Boost.Profit);
        }
    }
}
