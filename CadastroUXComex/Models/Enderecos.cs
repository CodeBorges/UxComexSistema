namespace CadastroUXComex.Models
{
    public class Enderecos
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public string Endereco { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }
}
