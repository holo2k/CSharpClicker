using CSharpClicker.Web.UseCases.AddPoints;
using CSharpClicker.Web.UseCases.Common;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("OrcHealth") == null)
                HttpContext.Session.SetInt32("OrcHealth", 100);
            var boosts = await mediator.Send(new GetUserQuery());
            var user = await mediator.Send(new GetCurrentUserQuery());
            var viewModel = new IndexViewModel
            {
                Boosts = boosts,
                User = user,
                OrcHealth = HttpContext.Session.GetInt32("OrcHealth") ?? 100
            };
            return View(viewModel);
        }


        [HttpPost("score")]
        public async Task<ScoreDto> AddToScore(AddPointsCommand command)
        {
            var user = await mediator.Send(new GetCurrentUserQuery());

            var currentOrcHealth = HttpContext.Session.GetInt32("OrcHealth") ?? 100;
            var newOrcHealth = currentOrcHealth - user.ProfitPerClick - user.ProfitPerSecond;

            if (newOrcHealth <= 0)
            {
                //поменять картинку
            }
            else
            {
                HttpContext.Session.SetInt32("OrcHealth", (int)newOrcHealth > 0 ? (int)newOrcHealth : 0);
            }

            return await mediator.Send(command);
        }
    }
}
