using Dapper;
using Microsoft.Data.SqlClient;

namespace AzureMvc.Models
{
    public class ReadOnlyRepository
    {

        private string connectionString = "Server=tcp:lucasbds.database.windows.net,1433;Initial Catalog=LucasBd;Persist Security Info=False;User ID=Lucas;Password=24221371Lc!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<Estado> GetAll()
        {
            var sql = @"SELECT C.ESTADOID,
                               C.NOME,
                               C.IMAGEMESTADO,
                               CAT.PAISID,
                               CAT.PAISNOME 
                        FROM ESTADO C
                        INNER JOIN PAIS CAT
                        ON C.PAISID = CAT.PAISID;
                       ";

            var result = new List<Estado>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                result = sqlConnection.Query<Estado>(sql).ToList();
                return result;
            }
        }

        public IEnumerable<Estado> VerTodosEstadosporPais(string Pais)
        {
            var sql = @"SELECT @""SELECT C.ESTADOID,
                               C.NOME,
                               C.IMAGEMESTADO,
                               CAT.PAISID,
                               CAT.PAISNOME 
                        FROM ESTADO C
                        INNER JOIN PAIS CAT
                        ON C.PAISID = CAT.PAISID;
                        WHERE CAT.PAISNAME = @P1;
                       ";

            var result = new List<Estado>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                result = sqlConnection.Query<Estado>(sql, new { P1 = Pais }).ToList();
                return result;
            }
        }

        public Estado GetById(int id)
        {
            var sql = @"SELECT @""SELECT C.ESTADOID,
                               C.NOME,
                               C.IMAGEMESTADO,
                               CAT.PAISID,
                               CAT.PAISNOME
                        FROM ESTADO C
                        INNER JOIN PAIS CAT
                        ON C.PAISID = CAT.PAISID;
                        WHERE C.ESTADOID = @P1;
                       ";

            Estado result;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                result = sqlConnection.QueryFirstOrDefault<Estado>(sql, new { P1 = id });
                return result;
            }
        }

    }
}

