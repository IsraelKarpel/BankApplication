import RequestDTO from "../Entities/RequestDTO"

export const TransactionToRequestDTOMapper = (transaction) => {
    return new RequestDTO("",
         transaction.id,
          transaction.bankAccount,
           parseFloat(transaction.amount),
            transaction.action);
};