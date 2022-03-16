using RepositorioGitHub.Dominio.Entidades;
using System.Collections.Generic;

namespace RepositorioGitHub.Dominio
{
    public class RepositoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Owner Owner { get; set; } 
        public long TotalCount { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public List<GitHubRepository> Repositories { get; set; }
    }
}
