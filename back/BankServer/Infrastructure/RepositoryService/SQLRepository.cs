using BankServer.Domain.DTOS;
using BankServer.Domain.Enums;
using BankServer.Infrastructure.LoggerService;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System.Globalization;
namespace BankServer.Infrastructure.RepositoryService
{
    /// <summary>
    /// repository for connection with mysql DB
    /// </summary>
    public class SQLRepository : IRepository
    {
        private const string InsertActionQuery = @" INSERT INTO bank.actions (ID, UserID, ActionType, Amount, Status, BankAccount, Date) 
                                                        VALUES(@ID, @userID, @actionType, @amount, @status, @bankAccount, @date)";
        private const string ParamID = "@ID";
        private const string ParamUserID = "@userID";
        private const string ParamActionType = "@actionType";
        private const string ParamAmount = "@Amount";
        private const string ParamStatus = "@status";
        private const string ParamBankAccount = "@bankAccount";
        private const string ParamDate = "@Date";
        private const string AddransactionSucceeded = "New transaction was added to the DB, transaction ID: ";
        private const string AddransactionFailed = "Failed to add new transaction was added to the DB, transaction ID: ";
        private const string SQLError = "SQL Error: ";
        private const string Error = "Error: ";
        private const string GetUserRowsQuery = "SELECT * FROM bank.actions WHERE userID = @userID";
        private const string GetUserRowsSuccess = "Transaction of user id: ";
        private const string GetUserRowsSuccessParams = " where fetched from the DB, number of rows: ";

        private readonly string _connectionString;
        private readonly ILoggerService _loggerService;

        public SQLRepository(string connectionString, ILoggerService loggerService)
        {
            _connectionString = connectionString;
            _loggerService = loggerService;
        }
        public bool AddRow(TransactionDTO transactionDTO)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var command = new MySqlCommand(InsertActionQuery, connection);
                    command.Parameters.AddWithValue(ParamID, transactionDTO.TransactionID);
                    command.Parameters.AddWithValue(ParamUserID, transactionDTO.UserID);
                    command.Parameters.AddWithValue(ParamActionType, transactionDTO.ActionType);
                    command.Parameters.AddWithValue(ParamAmount, transactionDTO.Amount);
                    command.Parameters.AddWithValue(ParamStatus, transactionDTO.ActionStatus);
                    command.Parameters.AddWithValue(ParamBankAccount, transactionDTO.BankNumber);
                    command.Parameters.AddWithValue(ParamDate, transactionDTO.dateTime);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        _loggerService.LogInfo(AddransactionSucceeded + transactionDTO.TransactionID);
                        return true;
                    }
                    else
                    {
                        _loggerService.LogInfo(AddransactionFailed + transactionDTO.TransactionID);
                        return false;
                    }
                }
            }

            catch (MySqlException ex)
            {
                _loggerService.LogError(SQLError + ex.Message);
                return false; // Return false if an exception occurs
            }
            catch (Exception ex)
            {
                _loggerService.LogError(Error + ex.Message);
                return false; // Return false if an exception occurs
            }
        }

        public RepositoryResponseDTO GetUserRows(string userID)
        {
            var result = new RepositoryResponseDTO
            {
                IsSuccess = false,
                ErrorMessage = "",
                Data = new List<TransactionDTO>()
            };
            var rows = new List<TransactionDTO>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var command = new MySqlCommand(GetUserRowsQuery, connection);
                    command.Parameters.AddWithValue(ParamUserID, userID);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = new TransactionDTO
                            {
                                TransactionID = null,
                                UserID = reader["UserID"].ToString(),
                                Amount = (double)reader["Amount"],
                                BankNumber = reader["BankAccount"].ToString(),
                                dateTime = null,
                                ActionType = null,
                                ActionStatus = null,
                            };
                            if (reader["ID"] != DBNull.Value && Guid.TryParse(reader["ID"].ToString(), out Guid parsedGuid))
                            {
                                entity.TransactionID = parsedGuid;
                            }
                            if (reader["Date"] != DBNull.Value && DateTime.TryParse(reader["Date"].ToString(), out DateTime parsedDate))
                            {
                                entity.dateTime = parsedDate;
                            }
                            if (reader["ActionType"] != DBNull.Value && Enum.TryParse(typeof(ActionType), reader["ActionType"].ToString(), out var parsedActionType))
                            {
                                entity.ActionType = (ActionType)parsedActionType;
                            }
                            if (reader["Status"] != DBNull.Value && Enum.TryParse(typeof(ActionStatusType), reader["Status"].ToString(), out var parsedActionStatusType))
                            {
                                entity.ActionStatus = (ActionStatusType)parsedActionStatusType;
                            }

                            rows.Add(entity);
                        }
                    }
                }

                result.IsSuccess = true; // Query succeeded
                result.Data = rows;
                _loggerService.LogInfo(GetUserRowsSuccess + userID + GetUserRowsSuccessParams + rows.Count);
            }
            catch (MySqlException ex)
            {
                _loggerService.LogError(SQLError + ex.Message);
            }
            catch (Exception ex)
            {
                _loggerService.LogError(Error + ex.Message);
            }

            return result;
        }

        public RepositoryResponseDTO DeleteRows(Guid transactionID)
        {
            var result = new RepositoryResponseDTO
            {
                IsSuccess = false,
                ErrorMessage = "",
                Data = new List<TransactionDTO>()
            };
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var command = new MySqlCommand($"DELETE From bank.actions where id={transactionID}", connection);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        _loggerService.LogInfo($"Transaction ID: {transactionID.ToString()} was deleted from the DB");
                        result.IsSuccess = true; // Returns true if at least one row was deleted
                    }
                    else
                    {
                        _loggerService.LogInfo($"Transaction ID: {transactionID.ToString()} was not deleted from the DB");
                    }
                }
            }

            catch (MySqlException ex)
            {
                _loggerService.LogError($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                _loggerService.LogError($"Error: {ex.Message}");
            }
            return result;
        }
    }
}
