import React, { createContext, useContext, useMemo, useState } from "react";

const StorageContext = createContext<any>({});
export const useStorage = (): any => useContext(StorageContext);

export const StorageProvider = ({
  children,
}: {
  children: React.ReactNode;
}): JSX.Element => {
  const [count, setCount] = useState(0);

  const value = useMemo(
    () => ({
      count,
      updateCount: setCount,
    }),
    [count]
  );

  return (
    <StorageContext.Provider value={value}>{children}</StorageContext.Provider>
  );
};
