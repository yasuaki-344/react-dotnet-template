import { WeatherForecastApi } from "../api-gateways";
import type { WeatherForecastDto } from "../domain/WeatherForecastDto";

export const useApi = (): any => {
  const weatherForecastApi = new WeatherForecastApi();

  const getWeatherForecasts = async (): Promise<WeatherForecastDto[]> => {
    await weatherForecastApi.getWeatherForecasts();
    // const entities = await weatherForecastApi.getWeatherForecasts();
    throw new Error("not implemented yet");
  };

  return { getWeatherForecasts };
};
