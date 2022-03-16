using Newtonsoft.Json;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Entidades;
using RepositorioGitHub.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositorioGitHub.Business
{
    public class GitHubApiBusiness : IGitHubApiBusiness
    {
        private readonly IContextRepository _context;
        private readonly IGitHubApi _gitHubApi;
        public GitHubApiBusiness(IContextRepository context, IGitHubApi gitHubApi)
        {
            _context = context;
            _gitHubApi = gitHubApi;
        }

        public async Task<List<GitHubRepositoryViewModel>> Get()
        {
            List<GitHubRepositoryViewModel> gitHubRepositoryViewModels = new List<GitHubRepositoryViewModel>();

            List<GitHubRepository> response = JsonConvert.DeserializeObject<List<GitHubRepository>>(await _gitHubApi.GetRepository("/users/wesleimoreira/repos"));

            if (response is null) return null;

            foreach (GitHubRepository gitHubRepository in response)
            {
                gitHubRepositoryViewModels.Add(new GitHubRepositoryViewModel
                {
                    Id = gitHubRepository.Id,
                    Name = gitHubRepository.Name,
                    FullName = gitHubRepository.FullName,
                    Description = gitHubRepository.Description,
                    Homepage = gitHubRepository.Homepage,
                    Language = gitHubRepository.Language,
                    Owner = gitHubRepository.Owner,
                    UpdatedAt = gitHubRepository.UpdatedAt,
                    Url = gitHubRepository.Url
                });
            }

            return gitHubRepositoryViewModels;
        }

        public async Task<GitHubRepositoryViewModel> GetById(long id)
        {
            GitHubRepositoryViewModel gitHubRepositoryViewModel = new GitHubRepositoryViewModel();

            try
            {
                var response = JsonConvert.DeserializeObject<List<GitHubRepository>>(await _gitHubApi.GetRepository("/users/wesleimoreira/repos"));

                if (response is null) return null;

                foreach (GitHubRepository gitHubRepository in response)
                {
                    if (gitHubRepository.Id == id)
                    {
                        gitHubRepositoryViewModel.Id = gitHubRepository.Id;
                        gitHubRepositoryViewModel.Url = gitHubRepository.Url;
                        gitHubRepositoryViewModel.Name = gitHubRepository.Name;
                        gitHubRepositoryViewModel.Owner = gitHubRepository.Owner;
                        gitHubRepositoryViewModel.Homepage = gitHubRepository.Homepage;
                        gitHubRepositoryViewModel.Language = gitHubRepository.Language;
                        gitHubRepositoryViewModel.FullName = gitHubRepository.FullName;
                        gitHubRepositoryViewModel.UpdatedAt = gitHubRepository.UpdatedAt;
                        gitHubRepositoryViewModel.Description = gitHubRepository.Description;
                    }
                }

                return gitHubRepositoryViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GitHubRepositoryViewModel> GetRepository(string owner, long id)
        {
            try
            {
                GitHubRepositoryViewModel model = new GitHubRepositoryViewModel();

                var response = JsonConvert.DeserializeObject<List<GitHubRepository>>(await _gitHubApi.GetRepository($"/users/{owner}/repos"));

                if (response.Count == 0 || response == null) return null;

                foreach (var repo in response)
                {
                    if (repo.Id == id)
                    {
                        model.Id = repo.Id;
                        model.Url = repo.Url;
                        model.Name = repo.Name;
                        model.Owner = repo.Owner;
                        model.Language = repo.Language;
                        model.FullName = repo.FullName;
                        model.Homepage = repo.Homepage;
                        model.UpdatedAt = repo.UpdatedAt;
                        model.Description = repo.Description;
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RepositoryViewModel> GetByName(string name)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<RepositoryModel>(await _gitHubApi.GetRepositoryByName($"search/repositories?q={name}"));

                if (response is null) return null;

                return new RepositoryViewModel
                {
                    TotalCount = response.TotalCount,
                    Repositories = response.Repositories
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FavoriteViewModel>> GetFavoriteRepository()
        {
            var texto = JsonConvert.SerializeObject(await _context.GetAll());

            if (string.IsNullOrEmpty(texto))
                return null;

            return JsonConvert.DeserializeObject<List<FavoriteViewModel>>(texto);
        }

        public async Task<FavoriteViewModel> SaveFavoriteRepository(FavoriteViewModel view)
        {
            var texto = await _context.Insert(JsonConvert.SerializeObject(view));

            if (string.IsNullOrEmpty(texto))
                return null;

            return JsonConvert.DeserializeObject<FavoriteViewModel>(texto);         
        }
    }
}
