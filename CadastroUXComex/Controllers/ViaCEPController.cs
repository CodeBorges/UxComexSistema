using CadastroUXComex.DAL;
using CadastroUXComex.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CadastroUXComex.Controllers
{
    public class ViaCEPController : Controller
    {
        [HttpGet]
        [Route("Home/ConsultarCEP/{cep}")]
        public async Task<IActionResult> ConsultarCEP(string cep)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new { errorMessage = "Erro ao consultar o CEP" });
                }

                var enderecoViaCep = JsonConvert.DeserializeObject<EnderecoViaCEP>(result);

                if (ViaCepDAL.IsValidEnderecoViaCEP(enderecoViaCep ?? new EnderecoViaCEP()))
                {
                    var endereco = new Enderecos();

                    endereco.CEP = enderecoViaCep.cep;
                    endereco.Endereco = enderecoViaCep.logradouro;
                    endereco.Cidade = enderecoViaCep.localidade;
                    endereco.Estado = enderecoViaCep.uf;

                    return Json(endereco);
                }
                else
                {
                    return Json(new { errorMessage = "Erro ao consultar o CEP" });
                }
            }
        }
    }
}
