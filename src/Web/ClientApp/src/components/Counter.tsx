import React, { FC, useState } from 'react';
import { Button } from '@material-ui/core';

export const Counter: FC = (): JSX.Element => {

  const [count, setCount] = useState(0);

  const incrementCounter = () => {
    setCount(count + 1);
  }

  return (
    <div>
      <h1>Counter</h1>

      <p>This is a simple example of a React component.</p>

      <p aria-live="polite">Current count: <strong>{count}</strong></p>
      <Button
        color='primary'
        variant='contained'
        onClick={incrementCounter}
      >Increment</Button>
    </div>
  );
}
Counter.displayName = Counter.name;
