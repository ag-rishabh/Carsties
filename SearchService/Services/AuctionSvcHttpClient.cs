using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Services
{
    public class AuctionSvcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _configuration = config;
            _httpClient = httpClient;
        }

        public async Task<List<Item>> GetItemForSearchDb()
        {
            var lastUpdated = await DB.Find<Item, string>().Sort(x => x.Descending(x => x.UpdateAt)).Project(x => x.UpdateAt.ToString()).ExecuteAsync();

            return await _httpClient.GetFromJsonAsync<List<Item>>(_configuration["AuctionServiceUrl"] + "/api/auctions?data=" + lastUpdated);
        }
    }
}
