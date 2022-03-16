using RepositorioGitHub.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositorioGitHub.Dominio.Interfaces
{
    public interface IContextRepository
    {
        Task<string> Insert(string favorite);
        Task<List<Favorite>> GetAll();
    }
}
