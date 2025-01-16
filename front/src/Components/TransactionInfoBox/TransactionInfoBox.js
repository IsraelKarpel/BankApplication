import React from "react";
import "./TransactionInfoBox.css";

function TransactionInfoBox({ boxData, headLine }) {
  return (
    <div className="dynamic-boxes">
      <h1 className="headline">{headLine}</h1>
      <div className="boxes-container">
        {boxData.map((box, index) => (
          <div className="box" key={index}>
            <p><strong>Action:</strong> {box.actionType}</p>
            <p><strong>Amount:</strong> {box.amount}</p>
            <p><strong>Date:</strong> {box.dateTime}</p>
            <p><strong>BankNumber:</strong> {box.bankAccount}</p>
            <p><strong>Success:</strong> {box.actionStatus}</p>
          </div>
        ))}
      </div>
    </div>
  );
}

export default TransactionInfoBox;