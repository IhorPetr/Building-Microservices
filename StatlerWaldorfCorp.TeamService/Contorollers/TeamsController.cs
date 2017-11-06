using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StatlerWaldorfCorp.TeamService.Contorollers
{
    [Route("[controller]")]
    public class TeamsController : Controller
    {
        ITeamRepository repository;
        public TeamsController(ITeamRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public  virtual IEnumerable<Team> GetAllTeams()
        {
            var createTest = new List<Team>
            {
                new Team
                {
                    ID = new Guid(),
                    Name = "Barselona"
                },
                new Team
                {
                    ID = new Guid(),
                    Name = "Manchster United"
                }
            };
            return createTest;
        }
        [HttpPost]
        public virtual IActionResult CreateTeam([FromBody]Team newTeam)
        {
            repository.AddTeam(newTeam);
			
            return Created($"/teams/{newTeam.ID}", newTeam);
        }
    }
}
