using CadastroUXComex.Models;
using CadastroUXComex.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace CadastroUXComex.DAL
{
    public class PessoasDAL
    {
        public static IEnumerable<Pessoas> ObterPessoas()
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                var sql = new StringBuilder();

                sql
               .Append("SELECT * FROM Pessoas");

                return conexao.Query<Pessoas>(sql.ToString()).ToList();
            }
        }

        public static Pessoas ObterPessoasPorId(int id)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "SELECT * FROM Pessoas WHERE Id = @Id";

                return conexao.QueryFirstOrDefault<Pessoas>(sql, new { Id = id });
            }
        }

        public static int CadastrarPessoaRetornarId(Pessoas pessoa)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "INSERT INTO Pessoas (Nome, Telefone, CPF) VALUES(@Nome, @Telefone, @CPF); SELECT CAST(SCOPE_IDENTITY() as int) AS Id;";

                pessoa.Id = conexao.ExecuteScalar<int>(sql, pessoa);

                return pessoa.Id;
            }
        }

        public static void EditarPessoa(Pessoas pessoa)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sql = "UPDATE Pessoas SET Nome = @Nome, Telefone = @Telefone, CPF = @CPF WHERE Id = @Id";

                conexao.Execute(sql, pessoa);
            }
        }

        public static void ExcluirPessoaPorId(int id)
        {
            using (var conexao = new SqlConnection(IDbConnection.DbConnection()))
            {
                conexao.Open();

                string sqlExcluirEnderecos = "DELETE FROM Enderecos WHERE PessoaId = @PessoaId";
                conexao.Execute(sqlExcluirEnderecos, new { PessoaId = id });

                string sql = "DELETE FROM Pessoas WHERE Id = @Id";

                conexao.Execute(sql, new { Id = id });
            }
        }
    }
}
