import type { WeatherForecastService } from "../application/Port";
import { WeatherForecastUseCase } from "../application/WeatherForecastUseCase";
import { useApi } from "./Api";

export const useWeatherForecast = (): WeatherForecastService => {
  const { getWeatherForecasts } = useApi();
  return WeatherForecastUseCase({ getWeatherForecasts });
};
