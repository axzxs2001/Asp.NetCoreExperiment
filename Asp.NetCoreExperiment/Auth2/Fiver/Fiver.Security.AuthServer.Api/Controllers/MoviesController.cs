using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiver.Security.AuthServer.Api.Controllers
{
    [Authorize]
    [Route("movies")]
    public class MoviesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new[]
            {
                new { Id = 1, Title = "Never Say Never Again", ReleaseYear = 1983, Summary = "A SPECTRE agent has stolen two American nuclear warheads, and James Bond must find their targets before they are detonated." },
                new { Id = 2, Title = "Diamonds Are Forever ", ReleaseYear = 1971, Summary = "A diamond smuggling investigation leads James Bond to Las Vegas, where he uncovers an evil plot involving a rich business tycoon." },
                new { Id = 3, Title = "You Only Live Twice ", ReleaseYear = 1967, Summary = "Agent 007 and the Japanese secret service ninja force must find and stop the true culprit of a series of spacejackings before nuclear war is provoked." }
            });
        }
    }
}
