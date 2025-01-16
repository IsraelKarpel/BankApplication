import "./Button.css";

function Button({ onClick, text }) {
    return (
      <button type="submit" onClick={onClick} className="form-button">
        {text}
      </button>
    );
  }

  export default Button;