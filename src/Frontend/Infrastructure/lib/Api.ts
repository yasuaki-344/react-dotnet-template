import { type WeatherForecastDto } from "@react-dotnet-template/application-core";
import {
  Configuration,
  WeatherForecastApi,
  type WeatherForecast,
} from "./api-gateways";
import { mapper } from "./mapper/Mapper";

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
