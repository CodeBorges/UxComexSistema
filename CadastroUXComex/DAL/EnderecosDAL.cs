using CadastroUXComex.Data;
using CadastroUXComex.Models;
using CadastroUXComex.ViewModel;
using Dapper;
using System.Data.SqlClient;

namespace CadastroUXComex.DAL
{
    public class EnderecosDAL
    {
        public static IEnumerable<Enderecos> ObterEnderecosPessoaId(int pessoaId)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "SELECT * FROM Enderecos WHERE PessoaId = @PessoaId";

                return conexao.Query<Enderecos>(sql, new { PessoaId = pessoaId });
            }
        }

        public static IEnumerable<Enderecos> ObterEnderecoPorPessoaId(int pessoaId)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "SELECT B.Endereco FROM Pessoas AS A LEFT JOIN Enderecos AS B ON A.Id = B.PessoaId WHERE A.Id = @PessoaId";

                return conexao.Query<Enderecos>(sql, new { PessoaId = pessoaId });
            }
        }

        public static void CadastrarEndereco(Enderecos endereco)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "INSERT INTO Enderecos (PessoaId, Endereco, CEP, Cidade, Estado) " +
                  "VALUES (@PessoaId, @Endereco, @CEP, @Cidade, @Estado)";

                conexao.Query(sql, endereco);
            }
        }

        public static void EditarEndereco(Enderecos endereco)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = @"UPDATE Enderecos SET Endereco = @Endereco, 
                                            CEP = @CEP, 
                                            Cidade = @Cidade,
                                            Estado = @Estado 
                                            WHERE Id = @Id";

                conexao.Execute(sql, endereco);
            }
        }

        public static void ExcluirEnderecoPorId(int id)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "DELETE FROM Enderecos WHERE Id = @Id;";

                conexao.Execute(sql, new { Id = id });
            }
        }

    }
}
