import type { WeatherForecastDto } from "../domain/WeatherForecastDto";
import type { WeatherForecastService } from "./Port";

interface Dependencies {
  getWeatherForecasts: () => Promise<WeatherForecastDto[]>;
}

export const WeatherForecastUseCase = (
  dependencies: Dependencies
): WeatherForecastService => {
  const getWeatherForecasts = async (): Promise<WeatherForecastDto[]> =>
    await dependencies.getWeatherForecasts();

  return { getWeatherForecasts };
};
