import { CountUseCase } from "../application/CountUseCase";
import type { CountService } from "../application/Port";
import { useCountStorage } from "./StorageAdapter";

export const useCount = (): CountService => {
  const { count, updateCount } = useCountStorage();

  return CountUseCase(count, updateCount);
};
