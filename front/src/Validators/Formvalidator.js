const HebrewNameRequiredMessage = "Hebrew name is required.";
const HebrewNameValidationMessage = "Hebrew name must be up to 20 Hebrew characters, allowing only spaces, '-' and ' characters.";
const EnglishNameRequiredMessage = "English name is required.";
const EnglishNameValidationMessage =  "English name must be up to 15 English characters, allowing only spaces, '-' and ' characters.";
const DateNameRequiredMessage = "Date is required.";
const DateTimeValidatorMessage = "Invalid date format.";
const IDRequiredMessage = "ID is required.";
const IDValidatorMessage = "ID must be exactly 9 digits.";
const ActionRequiredMessage = "action is required.";
const AmountRequiredMessage = "Amount is required.";
const AmountalidatorMessage = "Amount must be up to 10 digits.";
const BankAccountRequiredMessage = "Bank Aacount is required.";
const BankAccountValidatorMessage = "Bank account must be up to 10 digits.";


export const validateForm = (formData) => {
    const newErrors = {};
  
    // Validate Hebrew Name
    const hebrewRegex = /^[\u0590-\u05FF\s'\-]{1,20}$/;
    if (!formData.hebrewName.trim()) {
      newErrors.hebrewName = HebrewNameRequiredMessage;
    } else if (!hebrewRegex.test(formData.hebrewName)) {
      newErrors.hebrewName = HebrewNameValidationMessage;
    }
  
    // Validate English Name
    const englishRegex = /^[a-zA-Z\s'\-]{1,15}$/;
    if (!formData.englishName.trim()) {
      newErrors.englishName = EnglishNameRequiredMessage
    } else if (!englishRegex.test(formData.englishName)) {
      newErrors.englishName = EnglishNameValidationMessage
    }
  
    // Validate Date
    if (!formData.date.trim()) {
      newErrors.date = DateNameRequiredMessage;
    } else if (isNaN(Date.parse(formData.date))) {
      newErrors.date = DateTimeValidatorMessage;
    }
  
    // Validate ID
    const idRegex = /^\d{9}$/;
    if (!formData.id.trim()) {
      newErrors.id = IDRequiredMessage;
    } else if (!idRegex.test(formData.id)) {
      newErrors.id = IDValidatorMessage;
    }
    
    // Validate Action
    if (!formData.action.trim()) {
        console.log(formData.action)
        newErrors.action = ActionRequiredMessage
    }
  
    // Validate Amount
    const amountRegex = /^\d{1,10}$/;
    if (!formData.amount.trim()) {
      newErrors.amount = AmountRequiredMessage
    } else if (!amountRegex.test(formData.amount)) {
      newErrors.amount = AmountalidatorMessage
    }
  
    // Validate Bank Account
    if (!formData.bankAccount.trim()) {
      newErrors.bankAccount = BankAccountRequiredMessage
    } else if (!amountRegex.test(formData.bankAccount)) {
      newErrors.bankAccount = BankAccountValidatorMessage
    }
  
    return newErrors;
  };