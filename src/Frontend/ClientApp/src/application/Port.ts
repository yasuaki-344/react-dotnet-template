import type { WeatherForecastDto } from "../domain/WeatherForecastDto";

export interface CountStorageService {
  count: number;
  updateCount: (count: number) => void;
}

export interface CountService {
  updateCount: (count: number) => void;
}

export interface WeatherForecastService {
  getWeatherForecasts: () => WeatherForecastDto[];
}
