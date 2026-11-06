using System.Net.Http.Json;
using _21WithEmilyWeb.Contracts.Dtos;

namespace _21WithEmilyWeb.App.Client.Services
{
    public class GameApiClient
    {
        private readonly HttpClient httpClient;

        public GameApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GameResponse> NewGameAsync()
        {
            using var response = await httpClient.PostAsync("Game/new-game", content: null);
            return await ReadGameResponseAsync(response);
        }

        public async Task<GameResponse> CountAsync(CountRequest request)
        {
            using var response = await httpClient.PostAsJsonAsync("Game/count", request);
            return await ReadGameResponseAsync(response);
        }

        private static async Task<GameResponse> ReadGameResponseAsync(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<GameResponse>()
                ?? throw new InvalidOperationException("The game API returned an empty response.");
        }
    }
}
