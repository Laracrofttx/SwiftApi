using Microsoft.Data.Sqlite;
using System.Data;

namespace SwiftApi.Data
{
	public class SwiftApiDbContext
	{
		private readonly string connectionString;
		private IDbConnection connection;

        public SwiftApiDbContext(string connectionString)
        {
           this.connectionString = connectionString;
           connection = new SqliteConnection(connectionString);
        }


    }
}
