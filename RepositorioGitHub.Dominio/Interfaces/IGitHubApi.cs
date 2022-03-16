using RepositorioGitHub.Dominio.Entidades;
using System.Threading.Tasks;

namespace RepositorioGitHub.Dominio.Interfaces
{
    public interface IGitHubApi
    {
        Task<string> GetRepository(string owner);
        Task<string> GetRepositoryByName(string name);
    }
}
