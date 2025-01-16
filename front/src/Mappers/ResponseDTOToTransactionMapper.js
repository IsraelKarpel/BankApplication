import TransactionBox from "../Entities/TransactionBox"


export const ResponseDTOToTransactionMapper = (transactionDTO) => {
        return new TransactionBox(
            transactionDTO.transactionID,
            transactionDTO.amount,
            transactionDTO.bankNumber,
            transactionDTO.dateTime,
            transactionDTO.actionType === 1 ? "Deposit" : "Withdrowal",
            transactionDTO.actionStatus === 0 ? "Success" : "Failed"
        )
}