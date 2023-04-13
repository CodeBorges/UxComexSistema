namespace CadastroUXComex.Data
{
    public class IDbConnection
    {
        private static readonly string _dbConnection =
            "Data Source=DESKTOP-J0DJS0L; Database=SistemaUxComex; Uid=joao; Pwd=123456; Trusted_Connection=false; Encrypt=False";

        public static string DbConnection()
        {
#if DEBUG
            return _dbConnection;

#else
            return dbConnectionRelease;
#endif
        }
    }
}
