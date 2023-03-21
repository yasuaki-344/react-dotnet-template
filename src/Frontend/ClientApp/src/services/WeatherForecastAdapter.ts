import type { WeatherForecastService } from "../application/Port";
import { WeatherForecastUseCase } from "../application/WeatherForecastUseCase";

export const useWeatherForecast = (): WeatherForecastService => {
  return WeatherForecastUseCase();
};
