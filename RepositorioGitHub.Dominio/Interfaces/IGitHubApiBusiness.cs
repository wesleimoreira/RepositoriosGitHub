using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositorioGitHub.Dominio.Interfaces
{
    public interface IGitHubApiBusiness
    {
        Task<List<GitHubRepositoryViewModel>> Get();
        Task<RepositoryViewModel> GetByName(string name);
        Task<GitHubRepositoryViewModel> GetById(long id);
        Task<List<FavoriteViewModel>> GetFavoriteRepository();
        Task<GitHubRepositoryViewModel> GetRepository(string owner, long id);
        Task<FavoriteViewModel> SaveFavoriteRepository(FavoriteViewModel view);
    }
}
