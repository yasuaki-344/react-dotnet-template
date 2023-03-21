export interface CountStorageService {
  count: number;
  updateCount: (count: number) => void;
}

export interface CountService {
  updateCount: (count: number) => void;
}
