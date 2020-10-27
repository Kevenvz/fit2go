using System.Net.Http;

namespace Fit2go.Clients
{
    public class SportivityClient
    {
        private readonly HttpClient _client;

        public SportivityClient(HttpClient client)
        {
            _client = client;
        }
    }
}
