import type { CountStorageService } from "@react-dotnet-template/application-core";
import { useStorage } from "./StorageProvider";

export const useCountStorage = (): CountStorageService => useStorage();
