import React, { FC, ReactNode } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

type LayoutProps = {
  children: ReactNode
}

export const Layout: FC<LayoutProps> = ({ children }): JSX.Element => {
  return (
    <div>
      <NavMenu />
      <Container>
        {children}
      </Container>
    </div>
  );
}
Layout.displayName = Layout.name;

