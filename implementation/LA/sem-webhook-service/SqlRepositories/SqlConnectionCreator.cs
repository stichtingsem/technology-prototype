using System.Data.SqlClient;

namespace SqlRepositories
{
    public abstract class SqlConnectionCreator
    {
        private readonly string connectionString;

        public SqlConnectionCreator(string connectionString) => this.connectionString = connectionString;

        public SqlConnection Create() => new SqlConnection(connectionString);
    }
}
