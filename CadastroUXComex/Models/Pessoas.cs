namespace CadastroUXComex.Models
{
    public class Pessoas
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public List<Enderecos>? Enderecos { get; set; }

    }
}
