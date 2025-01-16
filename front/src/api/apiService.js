import axios from "axios";
import { TransactionToRequestDTOMapper } from "../Mappers/TransactionToRequestDTOMapper"

const SubmitFormErrorMessage = "An error occurred while submitting the form.";
const API_BASE_URL = process.env.REACT_APP_URL;

export const submitFormData = async (transaction) => {
  try {
    const requestDTO = TransactionToRequestDTOMapper(transaction)
    const response = await axios.post(`${API_BASE_URL}`, requestDTO);
    return response.data;
  } catch (error) {
    throw error.response?.data || SubmitFormErrorMessage;
  }
};