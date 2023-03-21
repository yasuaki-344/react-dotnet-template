import { createMap, createMapper } from "@automapper/core";
import { pojos, PojosMetadataMap } from "@automapper/pojos";
import { type WeatherForecast } from "../api-gateways";
import { type WeatherForecastDto } from "@react-dotnet-template/application-core";

// entity mete data
PojosMetadataMap.create<WeatherForecast>("WeatherForecast", {
  date: Date,
  temperatureC: Number,
  temperatureF: Number,
  summary: String,
});

// DTO mete data
PojosMetadataMap.create<WeatherForecastDto>("WeatherForecastDto", {
  date: Date,
  temperatureC: Number,
  temperatureF: Number,
  summary: String,
});

// create mapper
const entityMapper = createMapper({
  strategyInitializer: pojos(),
});
createMap<WeatherForecast, WeatherForecastDto>(
  entityMapper,
  "WeatherForecast",
  "WeatherForecastDto"
);

export const mapper = entityMapper;
