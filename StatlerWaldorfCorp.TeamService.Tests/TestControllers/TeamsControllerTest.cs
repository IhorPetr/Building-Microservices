using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using StatlerWaldorfCorp.TeamService.Contorollers;
using StatlerWaldorfCorp.TeamService.Models;
using StatlerWaldorfCorp.TeamService.Persistence;
using Xunit;

namespace StatlerWaldorfCorp.TeamService.Tests.TestControllers
{
    public class TeamsControllerTest
    {
        private readonly Mock<ITeamRepository> teamrepo;
        private readonly TeamsController teamcontroller;
        public TeamsControllerTest()
        {
           teamrepo= new Mock<ITeamRepository>();
           teamcontroller= new TeamsController(teamrepo.Object);
        }

        [Fact]
        public void QueryTeamListReturnsCorrectTeams()
        {
            //Arrange
            teamrepo.Setup(r => r.GetTeams()).Returns(new List<Team>
            {
                new Team
                {
                   ID = new Guid(),
                   Name = "Barselona"
                },
                new Team
                {
                    ID = new Guid(),
                    Name = "Real Madrid"
                }
            });
            //Act
            var result = teamcontroller.GetAllTeams();
            //Assert
            Assert.Equal(result.Count(), 2);
        }
    }
}
