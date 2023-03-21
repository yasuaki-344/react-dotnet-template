import type { CountService } from "./Port";

export const CountUseCase = (
  count: number,
  updateCount: (count: number) => void
): CountService => {
  return {
    updateCount,
  };
};
