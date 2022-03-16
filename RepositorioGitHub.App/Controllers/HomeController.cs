using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RepositorioGitHub.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGitHubApiBusiness _business;
        public HomeController(IGitHubApiBusiness business) => _business = business;

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                List<GitHubRepositoryViewModel> model = await _business.Get();

                if (model.Equals(null))
                    CarregandoMensagens("warning", "O Repositório não foi localizado!");

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(long id)
        {
            try
            {
                if (id == 0) return RedirectToAction("Index");               

                GitHubRepositoryViewModel model = await _business.GetById(id);

                if (model.Equals(null))
                    CarregandoMensagens("warning", "O Repositório não foi localizado!");

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetRepositorie(string name = "defunkt", string message = "", string typeMessage = "")
        {
            try
            {    
                string textMessage = message;
                string textTypeMessage = typeMessage;

                RepositoryViewModel model = await _business.GetByName(name);

                if (model.Equals(null) && string.IsNullOrEmpty(textMessage) && string.IsNullOrEmpty(textTypeMessage))
                    CarregandoMensagens("warning", "O Repositório não foi localizado!");

                if (!string.IsNullOrEmpty(textMessage) && !string.IsNullOrEmpty(textTypeMessage))
                    CarregandoMensagens(textTypeMessage, textMessage);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetRepositorie");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetRepositorie(RepositoryViewModel view)
        {
            try
            {
                if (string.IsNullOrEmpty(view.Name))
                {
                    CarregandoMensagens("warning", "O nome do repositório não foi preenchido!");
                    return RedirectToAction("GetRepositorie");
                }               

                RepositoryViewModel model = await _business.GetByName(view.Name);

                if (model.Equals(null))
                    CarregandoMensagens("warning", "O Repositório não foi localizado!");
                else
                    CarregandoMensagens("success", "Repositório localizado com sucesso!");

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetRepositorie");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DetailsRepository(long id, string login)
        {
            try
            {
                if (id == 0 || string.IsNullOrEmpty(login))
                    return RedirectToAction("GetRepositorie", "Home", new { typeMessage = "warning", message = "O Repositório não foi localizado!" });

                GitHubRepositoryViewModel model = await _business.GetRepository(login, id);

                if (model.Equals(null) || model.Owner == null)
                    return RedirectToAction("GetRepositorie", "Home", new { typeMessage = "warning", message = "O Repositório não foi localizado!" });

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("GetRepositorie");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Favorite()
        {
            List<FavoriteViewModel> model = await _business.GetFavoriteRepository();

            if (model == null)
                CarregandoMensagens("warning", "Não foram encontrados favoritos marcados!");

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> FavoriteSave(GitHubRepositoryViewModel view)
        {
            try
            {
                if (view.Id == 0 &&
                    string.IsNullOrEmpty(view.Language) &&
                    string.IsNullOrEmpty(view.Owner.Login) &&
                    string.IsNullOrEmpty(view.Description) &&
                    string.IsNullOrEmpty(view.Url.ToString()))
                    throw new Exception("Não foi possivel realizar esta operação");


                var response = await _business.SaveFavoriteRepository(new FavoriteViewModel
                {
                    Id = view.Id,
                    Login = view.Owner.Login,
                    Language = view.Language,
                    Url = view.Url.ToString(),
                    Description = view.Description,
                    UpdateLast = view.UpdatedAt.DateTime,
                });

                if (response == null)
                    return RedirectToAction("GetRepositorie", "Home", new { typeMessage = "warning", message = "Não foi possivel realizar esta operação!" });

                return RedirectToAction("GetRepositorie", "Home", new { typeMessage = "success", message = "O repositório foi favorecido com sucesso!" });

            }
            catch (Exception ex)
            {
                return RedirectToAction("GetRepositorie", "Home", new { typeMessage = "warning", message = ex.Message });
            }
        }

        private void CarregandoMensagens(string name, string message)
        {
            switch (name)
            {
                case "success":
                    TempData["success"] = message;
                    TempData["info"] = null;
                    TempData["warning"] = null;
                    TempData["error"] = null;
                    break;
                case "info":
                    TempData["info"] = message;
                    TempData["success"] = null;
                    TempData["warning"] = null;
                    TempData["error"] = null;
                    break;
                case "warning":
                    TempData["warning"] = message;
                    TempData["info"] = null;
                    TempData["success"] = null;
                    TempData["error"] = null;
                    break;
                case "error":
                    TempData["error"] = message;
                    TempData["info"] = null;
                    TempData["warning"] = null;
                    TempData["success"] = null;
                    break;
            }
        }

    }
}