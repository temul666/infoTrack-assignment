import { Table } from "react-bootstrap";
import React, { useEffect, useState } from "react";

function SearchResults() {
  const [records, setRecords] = useState([]);

  useEffect(() => {
    const interval = setInterval(() => {
      fetch("http://localhost:5100/api/ResultLog?offset=0&returnCount=100")
        .then((response) => response.json())
        .then((data) => {
          setRecords(data);
        })
        .catch((error) => console.log(error));
    }, 1000);

    return () => clearInterval(interval);
  });

  return (
    <Table striped bordered hover>
      <thead>
        <tr>
          <th>Search Result Rank</th>
          <th>Search Site</th>
          <th>Date and Time</th>
          <th>Search Keyword</th>
          <th>Matching Url</th>
        </tr>
      </thead>
      <tbody>
        {records.map((data) => {
          const bestResult = Math.min(...data.resultRank);
          const className =
            bestResult === 0
              ? "table-danger"
              : bestResult < 5
              ? "table-success"
              : bestResult < 10
              ? "table-warning"
              : "table-danger";

          const rank =
            data.resultRank.join(",") === "0"
              ? "No Matching Result"
              : data.resultRank.join(",");
          return (
            <tr className={className}>
              <td>{rank}</td>
              <td>{data.searchSite}</td>
              <td>{data.searchTimeStamp}</td>
              <td>{data.searchPhrase}</td>
              <td>{data.matchingUrl}</td>
            </tr>
          );
        })}
      </tbody>
    </Table>
  );
}

export default SearchResults;
