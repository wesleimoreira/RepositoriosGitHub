using RepositorioGitHub.Dominio.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.ApiGitHub
{
    public class GitHubApi : IGitHubApi
    {
        private readonly HttpClient _httpClient;

        public GitHubApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "request");
            _httpClient.BaseAddress = new Uri("https://api.github.com");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetRepository(string owner)
        {
            var response = await _httpClient.GetAsync(owner);

            if (!response.StatusCode.Equals(HttpStatusCode.OK)) return null;

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetRepositoryByName(string name)
        {
            var response = await _httpClient.GetAsync(name);

            if (!response.StatusCode.Equals(HttpStatusCode.OK)) return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}
