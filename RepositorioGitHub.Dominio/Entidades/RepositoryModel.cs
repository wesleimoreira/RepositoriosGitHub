using Newtonsoft.Json;
using System.Collections.Generic;

namespace RepositorioGitHub.Dominio.Entidades
{
    public  class RepositoryModel
    {
        [JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }

        [JsonProperty("items")]
        public List<GitHubRepository> Repositories { get; set; }
    }
}
