using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Fit2go.Converters;
using Fit2go.Exceptions;
using Fit2go.Models;

namespace Fit2go.Clients
{
    public class SportivityClient
    {
        private const string BaseUrl = "https://bossnl.mendixcloud.com/rest/v2/";

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions;

        public SportivityClient(HttpClient client)
        {
            _client = client;
            _jsonOptions = new JsonSerializerOptions();

            _jsonOptions.Converters.Add(new SportivityDateTimeOffsetConverter());
        }

        public async Task<GetLessonsResponse> GetLessons(GetLessonsRequest request, CancellationToken cancellationToken = default) =>
            await PostAsync<GetLessonsRequest, GetLessonsResponse>("getLessonsId_V2", request, cancellationToken);

        public async Task<JoinLessonResponse> JoinLesson(JoinLessonRequest request, CancellationToken cancellationToken = default) =>
            await PostAsync<JoinLessonRequest, JoinLessonResponse>("joinLesson_V2", request, cancellationToken);

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request, CancellationToken cancellationToken = default)
        {
            string url = GetUrl(endpoint);
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, request, _jsonOptions, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new SportivityApiException(response.StatusCode, content);
            }

            return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken);
        }

        private static string GetUrl(string endpoint) => BaseUrl + endpoint;
    }
}
