import {
  Configuration,
  WeatherForecastApi,
  type WeatherForecast,
} from "@react-dotnet-template/application-core";
import type { WeatherForecastDto } from "../domain/WeatherForecastDto";
import { mapper } from "@react-dotnet-template/application-core";

export const useApi = (): any => {
  const configuration = new Configuration({
    basePath: "",
  });

  const weatherForecastApi = new WeatherForecastApi(configuration);

  const getWeatherForecasts = async (): Promise<WeatherForecastDto[]> => {
    const entities = await weatherForecastApi.getWeatherForecasts();

    return entities.map((entity: WeatherForecast) =>
      mapper.map<WeatherForecast, WeatherForecastDto>(
        entity,
        "WeatherForecast",
        "WeatherForecastDto"
      )
    );
  };

  return { getWeatherForecasts };
};
