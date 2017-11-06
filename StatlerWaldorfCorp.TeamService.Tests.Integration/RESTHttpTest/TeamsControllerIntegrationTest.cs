using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using StatlerWaldorfCorp.TeamService.Models;
using Xunit;

namespace StatlerWaldorfCorp.TeamService.Tests.Integration.RESTHttpTest
{
    public class TeamsControllerIntegrationTest
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;

        private readonly Team teamFootball;

        public TeamsControllerIntegrationTest()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();
            teamFootball = new Team
            {
                ID = new Guid(),
                Name = "Barselona"
            };
        }

        [Fact]
        public async void TestTeamPostAndGet()
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(teamFootball)
                ,UnicodeEncoding.UTF8,"application/json");

            HttpResponseMessage postResponse = await testClient.PostAsync("/teams", stringContent);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await testClient.GetAsync("/teams");
            getResponse.EnsureSuccessStatusCode();

            string raw = await getResponse.Content.ReadAsStringAsync();

            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(raw);

            Assert.Equal(2, teams.Count);
            Assert.Equal("Barselona", teams[0].Name);
            Assert.Equal(teamFootball.ID, teams[0].ID);
        }

    }
}
