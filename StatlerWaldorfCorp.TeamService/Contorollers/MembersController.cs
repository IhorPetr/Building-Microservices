using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using StatlerWaldorfCorp.TeamService.Clients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StatlerWaldorfCorp.TeamService.Contorollers
{
    [Route("/teams/{teamId}/[controller]")]
    public class MembersController : Controller
    {
        private ITeamRepository repository;
        private ILocationClient locationClient;

        public MembersController(ITeamRepository repository, ILocationClient locationClient)
        {
            this.repository = repository;
            this.locationClient = locationClient;
        }


        [HttpGet]
        [Route("/teams/{teamId}/[controller]/{memberId}")]
        public async virtual Task<IActionResult> GetMember(Guid teamID, Guid memberId)
        {
            Team team = repository.Get(teamID);
            if (team == null)
            {
                return this.NotFound();
            }
            else
            {
                var q = team.Members.Where(m => m.ID == memberId);
                if (q.Count() < 1)
                {
                    return this.NotFound();
                }
                else
                {
                    Member member = (Member) q.First();
                    return this.Ok(new LocatedMember
                    {
                        ID = member.ID,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        LastLocation = await this.locationClient.GetLatestForMember(member.ID)
                    });
                }
            }
        }
    }
}
