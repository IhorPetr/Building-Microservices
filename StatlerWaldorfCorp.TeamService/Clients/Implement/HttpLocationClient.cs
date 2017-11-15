using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Clients.Implement
{
    public class HttpLocationClient : ILocationClient
    {
        public String URL { get; set; }

        public HttpLocationClient(string url)
        {
            this.URL = url;
        }
        public async Task<LocationRecord> GetLatestForMember(Guid memberId)
        {
            LocationRecord locationRecord = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await httpClient.GetAsync(String.Format("/locations/{0}/latest", memberId));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    locationRecord = JsonConvert.DeserializeObject<LocationRecord>(json);
                }
            }
            return locationRecord;
        }
    }
}
