import React, { useState } from "react";
import "./ActionForm.css";
import Button from "../Components/Button/Button";
import InputWithLabel from "../Components/InputWithLabel/InputWithLabel";
import DropdownWithLabel from "../Components/DropdownWithLabel/DropdownWithLabel"
import TransactionInfoBox from "../Components/TransactionInfoBox/TransactionInfoBox";
import NewTransactionProperties from "../Entities/NewTransactionProperties";
import { validateForm } from "../Validators/Formvalidator";
import { submitFormData} from "../api/apiService";
import { ResponseDTOToTransactionMapper } from "../Mappers/ResponseDTOToTransactionMapper";

function ActionForm() {
  const [transactionProperties, setTransactionProperty] = useState(new NewTransactionProperties("", "", "", "", "", "", ""));
  const [errors, setErrors] = useState({});
  const [transactionInfoBoxes, setTransactionInfoBoxes] = useState([]);
  const [currentTransactionInfo, setCurrentTransactionInfo] = useState([]);
  const [isResponse, setIsResponse] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setErrors((prevErrors) => ({ ...prevErrors, [name]: "" }));
    setTransactionProperty((prevData) => {
        const updatedData = { ...prevData }; // Create a shallow copy
        updatedData[name] = value;          // Update the specific field
        return updatedData;                 // Return the updated instance
      });
  };


  const handleSubmit = async (e) => {
    e.preventDefault();
    const formErrors = validateForm(transactionProperties);
    if (Object.keys(formErrors).length > 0) {
      setErrors(formErrors);
    } else {
      try {
        const response = await submitFormData(transactionProperties);
        setCurrentTransactionInfo([]);
        setTransactionInfoBoxes([]);
        response.transactionsDTO.forEach((transaction, index) => {
            const transactionInfoBox = ResponseDTOToTransactionMapper(transaction);
            if (index === 0) {
                setCurrentTransactionInfo((currentTransactionInfoBoxes) => ([ ...currentTransactionInfoBoxes, transactionInfoBox]));
            } else {
                setTransactionInfoBoxes((prevTransactionInfoBoxes) => ([ ...prevTransactionInfoBoxes, transactionInfoBox]))
            }
        })
        setIsResponse(true);
      } catch (error) {
        alert(`Failed to submit the form or fetch transaction box data: ${error}`);
      } 
    }
  };

  return (
    <div>
    <div className="form-wrapper">
      <form onSubmit={handleSubmit} className="form">
        <h2 className="form-headline">Simple React Form</h2>

        <InputWithLabel
          id="full-hebrew-name"
          label="Full Hebrew name"
          type="text"
          name="hebrewName"
          value={transactionProperties.hebrewName}
          onChange={handleChange}
          error={errors.hebrewName}
        />
        <InputWithLabel
          id="full-english-name"
          label="Full English name"
          type="text"
          name="englishName"
          value={transactionProperties.englishName}
          onChange={handleChange}
          error={errors.englishName}
        />
        <InputWithLabel
          id="birth-date"
          label="Birth Date"
          type="date"
          name="date"
          value={transactionProperties.date}
          onChange={handleChange}
          error={errors.date}
        />
        <InputWithLabel
          id="id"
          label="ID"
          type="text"
          name="id"
          value={transactionProperties.id}
          onChange={handleChange}
          error={errors.id}
        />
         <DropdownWithLabel
          id="action"
          label="Choose Action"
          name="action"
          options={[
            { label: "Deposit", value: "deposit" },
            { label: "Withdrawal", value: "withdrawal" },
          ]}
          value={transactionProperties.action}
          onChange={handleChange}
          error={errors.action}
        />
        <InputWithLabel
          id="amount"
          label="Amount"
          type="number"
          name="amount"
          value={transactionProperties.amount}
          onChange={handleChange}
          error={errors.amount}
        />
        <InputWithLabel
          id="bank-account"
          label="Bank Account"
          type="number"
          name="bankAccount"
          value={transactionProperties.bankAccount}
          onChange={handleChange}
          error={errors.bankAccount}
        />
         <Button text="Submit" onClick={handleSubmit} />
      </form>
    </div>
    <div>
        {isResponse && <TransactionInfoBox boxData={currentTransactionInfo} headLine={"Current Action"}/>}
        {isResponse && <TransactionInfoBox boxData={transactionInfoBoxes} headLine={"Previews Action"}/>}
    </div>
    </div>
  );
}

export default ActionForm;