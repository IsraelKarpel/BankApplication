using BankServer.Domain.Enums;
using BankServer.Domain.Models;
using BankServer.Infrastructure.LoggerService;
using System.Text.RegularExpressions;

namespace BankServer.Application.Services.ValidationServices
{
    /// <summary>
    /// Service that responsible fro validation of the request
    /// </summary>
    public class ValidationService : IValidationService
    {
        private const string FailIDValidation = "validation failed, ID should be 9 digits. user id : ";
        private const string FailBankNumberValidation = "validation failed, The bank number should be at most 10 digits. user id : ";
        private const string FailAmountValidation = "validation failed, Amount should be at most 10 digits. user id :";
        private const string FailActionValidation = "validation failed, Not recognized action. user id :";
        private const string ArgumentExceptionIDValidation = "ID should be 9 digits.";
        private const string ArgumentExceptionBankNumberValidation = "The bank number should be at most 10 digits.";
        private const string ArgumentExceptionnAmountValidation = "Amount should be at most 10 digits";
        private const string ArgumentExceptionActionValidation = "Not recognized action";
        public const string bankNumber = " bank number: ";
        public const string amount = " amount: ";
        public const string actionType = " action type: ";

        private readonly ILoggerService _loggerService;
        public ValidationService(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }
        public void ValidateRequest(MainRequstDTO request)
        {
            if (!IsNineDigitNumber(request.UserID))
            {
                _loggerService.LogError(FailIDValidation + request.UserID);
                throw new ArgumentException(ArgumentExceptionIDValidation);
            }
            if (IsUpToTenDigitNumber(request.BankNumber))
            {
                _loggerService.LogError(FailBankNumberValidation + request.UserID + bankNumber +request.BankNumber);
                throw new ArgumentException(ArgumentExceptionBankNumberValidation);
            }
            if (IsUpToTenDigitNumber(request.Amount))
            {
                _loggerService.LogError(FailAmountValidation + request.UserID + amount + request.Amount);
                throw new ArgumentException(ArgumentExceptionnAmountValidation);
            }
            if (!IsValidAction(request.ActionType))
            {
                _loggerService.LogError(FailActionValidation + request.UserID + actionType + request.ActionType);
                throw new ArgumentException(ArgumentExceptionActionValidation);
            }
        }

        private bool IsNineDigitNumber(string input)
        {
            return Regex.IsMatch(input, @"^\d{9}$");
        }

        private bool IsUpToTenDigitNumber(string input)
        {
            return Regex.IsMatch(input, @"^\d{1, 10}$");
        }

        private bool IsUpToTenDigitNumber(double input)
        {
            // Convert the absolute value to a string to handle negative numbers gracefully
            string numberString = Math.Abs(input).ToString();

            // Remove the decimal point for digit length checking
            string cleanedString = numberString.Replace(".", "");

            // Check if the cleaned string contains at most 10 digits
            return cleanedString.Length > 10;
        }

        private bool IsValidAction(string action)
        {
            return !Enum.IsDefined(typeof(ActionType), action);
        }
    }
}
