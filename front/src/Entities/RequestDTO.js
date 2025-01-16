class RequestDTO {
    constructor(transactionID, userID, bankNumber, amount, actionType) {
        this.TransactionID = transactionID;
        this.UserID = userID;
        this.BankNumber = bankNumber;
        this.Amount = amount;
        this.ActionType = actionType;
    }
}

export default RequestDTO;