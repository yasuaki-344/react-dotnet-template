import React, { FC } from "react";
import { Container } from "@material-ui/core";
import { NavMenu } from "./NavMenu";

type LayoutProps = {
  children: any;
};

export const Layout: FC<LayoutProps> = ({ children }): JSX.Element => (
  <div>
    <NavMenu />
    <Container>{children}</Container>
  </div>
);
Layout.displayName = Layout.name;
