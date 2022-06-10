import React, { Component } from "react";
import "./App.css";
import SearchResults from "./SearchResults.js";
import "bootstrap/dist/css/bootstrap.min.css";

class App extends Component {
  render() {
    return (
      <div className="App">
        <SearchResults />
      </div>
    );
  }
}

export default App;
