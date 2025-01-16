class TransactionBox {
    constructor(transactionID, amount, bankAccount, dateTime, actionType, actionStatus) {
        this.transactionID = transactionID;
        this.amount = amount;
        this.bankAccount = bankAccount;
        this.dateTime = dateTime;
        this.actionType = actionType;
        this.actionStatus = actionStatus;
    }
}

export default TransactionBox