import {
  type WeatherForecast,
  WeatherForecastApi,
  Configuration,
} from "../api-gateways";
import type { WeatherForecastDto } from "../domain/WeatherForecastDto";

export const useApi = (): any => {
  const configuration = new Configuration({
    basePath: "",
  });

  const weatherForecastApi = new WeatherForecastApi(configuration);

  const getWeatherForecasts = async (): Promise<WeatherForecastDto[]> => {
    const entities = await weatherForecastApi.getWeatherForecasts();
    return entities.map((e: WeatherForecast) => {
      const x: WeatherForecastDto = {
        date: e.date,
        temperatureC: e.temperatureC,
        temperatureF: e.temperatureF,
        summary: e.summary,
      };
      return x;
    });
  };

  return { getWeatherForecasts };
};
