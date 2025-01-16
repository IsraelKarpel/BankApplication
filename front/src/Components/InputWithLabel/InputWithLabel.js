import React from "react";
import "./InputWithLabel.css"
import "../../App.css"

function InputWithLabel({ id, label, type, name, value, onChange, error }) {
  return (
    <div className="form-group">
      <label htmlFor={id}  className="form-label">
        {label}
      </label>
      <input
        type={type}
        id={id}
        name={name}
        value={value}
        onChange={onChange}
        className="form-input"
      />
      {error && <p className="form-error">{error}</p>}
    </div>
  );
}

export default InputWithLabel;