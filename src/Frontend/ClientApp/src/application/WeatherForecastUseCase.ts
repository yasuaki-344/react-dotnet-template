import type { WeatherForecastDto } from "../domain/WeatherForecastDto";
import type { WeatherForecastService } from "./Port";

export const WeatherForecastUseCase = (): WeatherForecastService => {
  const getWeatherForecasts = (): WeatherForecastDto[] => [];

  return { getWeatherForecasts };
};
