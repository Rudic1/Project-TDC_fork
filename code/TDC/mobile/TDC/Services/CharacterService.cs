using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TDC.IService;

namespace TDC.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient httpClient = new();

        public async Task<string> GetDefaultCharacterImage()
        {
            var url = ConnectionUrls.development + "/api/Character/getDefaultCharacterImage";

            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return string.Empty;

            string responseContent = await response.Content.ReadAsStringAsync();

            // Falls der Server ein einfacher string mit Anführungszeichen zurückliefert ("https://...")
            return responseContent.Trim('"');
        }

        // Platzhalter für spätere Erweiterungen (optional)
        public Task<bool> UpdateCharacterImage(string username, byte[] imageData)
        {
            throw new NotImplementedException(); // TODO: Add when BE is ready
        }
    }
}
