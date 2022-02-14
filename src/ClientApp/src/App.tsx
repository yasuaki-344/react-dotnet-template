import React from "react";
import logo from "./logo.svg";
import "./App.css";

function App() {
  const onClick = async () => {
    console.log("clicked");
    const response = await fetch("http://localhost:5000/weatherforecast");
    const data = await response.json();
    console.log(data);
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <button type="button" onClick={onClick}>fetch test</button>
      </header>
    </div>
  );
}

export default App;
