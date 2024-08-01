using Dapper;
using Microsoft.Data.Sqlite;
using SwiftApi.Data.GlobalConstants;
using SwiftApi.Data.Models;
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

        public async Task<ResponseMessage<string>> InsertDataAsync(Message message)
        {
            var response = new ResponseMessage<string>();

            try
            {
                var tableCreation = await CreateDataTableAsync();
                if (!tableCreation.IsSuccessful)
                {
                    response.IsSuccessful = false;
                    return response;
                }

                var insertIntoTable = await InsertDataAsync(message);
                if (!insertIntoTable.IsSuccessful)
                {
                    response.IsSuccessful = false;
                    return response;
                }
                response.Data = GlobalConstants.GlobalConstants.SuccessfulTableCreation;
            }
            catch (SqliteException)
            {
                response.IsSuccessful = false;
                throw new InvalidDataException(GlobalConstants.GlobalConstants.ErrorDuringDatabaseOperations);
            
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                throw new ArgumentException(GlobalConstants.GlobalConstants.ErrorDuringDatabaseOperations, ex.Message);
            }

            return response;
        }
        public async Task<ResponseMessage<string>> CreateDataTableAsync()
        { 
            var response = new ResponseMessage<string>();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                await connection.ExecuteAsync(GlobalConstants.GlobalConstants.CreateSqlTable);
                response.Data = GlobalConstants.GlobalConstants.SuccessfulTableCreation;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                throw new ArgumentException(GlobalConstants.GlobalConstants.ErrorDuringDatabaseOperations, ex.Message);
            }

            return response;
        }

        public async Task<ResponseMessage<IDbConnection>> InsertIntoTable(Message message)
        { 
            var response = new ResponseMessage<IDbConnection>();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                await connection.ExecuteAsync(GlobalConstants.GlobalConstants.Insert);
                response.Data = connection;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                throw new ArgumentException(GlobalConstants.GlobalConstants.ErrorDuringDatabaseOperations, ex.Message);
            }

            return response;
        
        }
    }
}
