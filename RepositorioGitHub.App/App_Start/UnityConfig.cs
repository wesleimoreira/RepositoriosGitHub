using RepositorioGitHub.Business;
using RepositorioGitHub.Dominio.Interfaces;
using RepositorioGitHub.Infra.ApiGitHub;
using RepositorioGitHub.Infra.Repositorio;
using System;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace RepositorioGitHub.App
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IGitHubApi, GitHubApi>();
            container.RegisterFactory<HttpClient>(x => new HttpClient());
            container.RegisterType<IContextRepository, ContextRepository>();
            container.RegisterType<IGitHubApiBusiness, GitHubApiBusiness>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}