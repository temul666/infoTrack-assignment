using infoTrack.ScrapeJob.Models;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;
using System.Web;

namespace infoTrack.ScrapeJob.Client.ApiClient
{
    public class ApiClient: IApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public ApiClient(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task SaveResults(List<MatchingResult> results)
        {
            var settings = _config.GetSection("JobSettings").Get<JobSettings>();

            var client = _clientFactory.CreateClient();
            UriBuilder builder = new UriBuilder(settings.ApiUrl);
            builder.Path = "/api/ResultLog";
            builder.Port = settings.ApiPort;

            var json = JsonConvert.SerializeObject(results);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(builder.ToString(), data);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error calling api");
            }
        }
    }
}
