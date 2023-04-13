using CadastroUXComex.Models;

namespace CadastroUXComex.DAL
{
    public class ViaCepDAL
    {
        public static bool IsValidEnderecoViaCEP(EnderecoViaCEP enderecoViaCEP)
        {
            if (enderecoViaCEP == null || string.IsNullOrEmpty(enderecoViaCEP.cep) || string.IsNullOrEmpty(enderecoViaCEP.logradouro) || string.IsNullOrEmpty(enderecoViaCEP.localidade) || string.IsNullOrEmpty(enderecoViaCEP.uf))
            {
                return false;
            }

            return true;
        }

    }
}
