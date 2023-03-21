import type { WeatherForecastService } from "@react-dotnet-template/application-core";
import { WeatherForecastUseCase } from "@react-dotnet-template/application-core";
import { useApi } from "@react-dotnet-template/infrastructure";

export const useWeatherForecast = (): WeatherForecastService => {
  const { getWeatherForecasts } = useApi();
  return WeatherForecastUseCase({ getWeatherForecasts });
};
