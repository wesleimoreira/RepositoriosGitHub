using Newtonsoft.Json;
using RepositorioGitHub.Dominio.Entidades;
using RepositorioGitHub.Dominio.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.Repositorio
{
    public class ContextRepository : IContextRepository
    {
        private static readonly string _dbPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("RepositorioGitHub.App\\bin", "RepositorioGitHub.Dominio\\Db\\tb_favorite.txt").Replace("file:\\", "");

        public async Task<List<Favorite>> GetAll()
        {
            List<Favorite> favorite = new List<Favorite>();          

            if (!File.Exists(_dbPath)) File.Create(_dbPath);

            using (StreamReader sr = File.OpenText(_dbPath))
            {
                string txt = string.Empty;

                while ((txt = await sr.ReadLineAsync()) != null)
                {
                    favorite.Add(JsonConvert.DeserializeObject<Favorite>(txt));
                }
            }

            return favorite;
        }

        public async Task<string> Insert(string favorite)
        {
            if (!File.Exists(_dbPath))
            {
                using (StreamWriter streamWriter = File.CreateText(_dbPath))
                {
                    await streamWriter.WriteLineAsync(favorite);
                }
            }

            using (StreamWriter streamWriter = File.AppendText(_dbPath))
            {
                await streamWriter.WriteLineAsync(favorite);
            }

            return favorite;
        }
    }
}
