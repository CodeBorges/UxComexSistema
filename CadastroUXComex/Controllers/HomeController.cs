using CadastroUXComex.DAL;
using CadastroUXComex.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CadastroUXComex.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Section Pessoas

        public IActionResult ListaPessoas()
        {
            var model = PessoasDAL.ObterPessoas();

            return View(model);
        }

        [HttpPost]
        public IActionResult CadastroPessoa([FromBody] Pessoas pessoa)
        {
            var idPessoa = PessoasDAL.CadastrarPessoaRetornarId(pessoa);

            TempData["Sucesso"] = "Cadastrado com sucesso!";

            return Json(new { redirectToUrl = Url.Action("ViewEditarPessoa", "Home", new { id = pessoa.Id }) });
        }

        public IActionResult CadastroPessoa()
        {
            return View();
        }

        [Route("Home/ViewEditarPessoa/{id}")]
        public IActionResult ViewEditarPessoa(int id)
        {
            var model = PessoasDAL.ObterPessoasPorId(id);

            if (model != null)
            {
                model.Enderecos = EnderecosDAL.ObterEnderecosPessoaId(id).ToList();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult EditarPessoa([FromBody] Pessoas pessoa)
        {
            PessoasDAL.EditarPessoa(pessoa);

            TempData["Sucesso"] = "Editado com sucesso!";

            return Json(new { redirectToUrl = Url.Action("ListaPessoas", "Home", new { id = pessoa.Id }) });
        }

        [Route("Home/ExcluirPessoa/{id}")]
        public IActionResult ExcluirPessoa(int id)
        {
            PessoasDAL.ExcluirPessoaPorId(id);

            TempData["Sucesso"] = "Pessoa excluida com sucesso!";

            return RedirectToAction("ListaPessoas");
        }

        #endregion

        #region Section Enderecos 

        [HttpPost]
        public IActionResult CadastrarEndereco([FromBody] Enderecos endereco)
        {
            EnderecosDAL.CadastrarEndereco(endereco);

            TempData["Sucesso"] = "Endereço cadastrado com sucesso!";

            return Json(new { redirectToUrl = Url.Action("ListaPessoas", "Home") });
        }

        [HttpPost]
        public IActionResult EditarEndereco([FromBody] Enderecos endereco)
        {
            EnderecosDAL.EditarEndereco(endereco);

            TempData["Sucesso"] = "Endereço editado com sucesso!";

            return Json(new { redirectToUrl = Url.Action("ListaPessoas", "Home") });
        }

        [Route("Home/ExcluirEndereco/{id}")]
        public IActionResult ExcluirEndereco(int id)
        {
            EnderecosDAL.ExcluirEnderecoPorId(id);

            TempData["Sucesso"] = "Endereco Excluido com sucesso!";

            string urlReferrer = Request.Headers["Referer"].ToString();

            return Redirect(urlReferrer);
        }

        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}