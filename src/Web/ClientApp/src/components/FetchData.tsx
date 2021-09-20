import React, { FC, useState, useEffect } from "react";
import {
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@material-ui/core";
import authService from "../api-authorization/AuthorizeService";

export const FetchData: FC = (): JSX.Element => {
  const [loading, setLoading] = useState(true);
  const [forecasts, setForecasts] = useState([]);

  useEffect(() => {
    populateWeatherData();
  }, []);

  const populateWeatherData = async () => {
    const token = await authService.getAccessToken();
    const response = await fetch("weatherforecast", {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    });
    const data = await response.json();
    setLoading(false);
    setForecasts(data);
  };

  const renderForecastsTable = (data: any): JSX.Element => (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Date</TableCell>
            <TableCell>Temp. (C)</TableCell>
            <TableCell>Temp. (F)</TableCell>
            <TableCell>Summary</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((forecast: any) => (
            <TableRow key={forecast.date}>
              <TableCell>{forecast.date}</TableCell>
              <TableCell>{forecast.temperatureC}</TableCell>
              <TableCell>{forecast.temperatureF}</TableCell>
              <TableCell>{forecast.summary}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );

  const contents = loading ? (
    <p>
      <em>Loading...</em>
    </p>
  ) : (
    renderForecastsTable(forecasts)
  );

  return (
    <div>
      <h1 id="tabelLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {contents}
    </div>
  );
};

FetchData.displayName = FetchData.name;
