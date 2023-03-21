import type { WeatherForecastService } from "@react-dotnet-template/application-core";
import { WeatherForecastUseCase } from "@react-dotnet-template/application-core";
import { useApi } from "./Api";

export const useWeatherForecast = (): WeatherForecastService => {
  const { getWeatherForecasts } = useApi();
  return WeatherForecastUseCase({ getWeatherForecasts });
};
