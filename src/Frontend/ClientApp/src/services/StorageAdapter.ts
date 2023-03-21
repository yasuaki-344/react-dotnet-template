import type { CountStorageService } from "../application/Port";
import { useStorage } from "./StorageProvider";

export const useCountStorage = (): CountStorageService => useStorage();
