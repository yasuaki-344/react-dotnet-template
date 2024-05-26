import { CountUseCase } from "@react-dotnet-template/application-core";
import type { CountService } from "@react-dotnet-template/application-core";
import { useCountStorage } from "./StorageAdapter";

export const useCount = (): CountService => {
  const { count, updateCount } = useCountStorage();

  return CountUseCase(count, updateCount);
};
