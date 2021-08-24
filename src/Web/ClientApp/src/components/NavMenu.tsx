import React, { FC, useState } from 'react';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import {
  AppBar, IconButton, Toolbar, Typography,
  List, ListItem, SwipeableDrawer
} from '@material-ui/core';
import MenuIcon from '@material-ui/icons/Menu';
import { Link } from 'react-router-dom';
import './NavMenu.css';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    textDark: {
      color: '#000000',
      textDecoration: 'none',
    },
    textWhite: {
      color: '#FFFFFF',
      textDecoration: 'none',
    },
  })
);

export const NavMenu: FC = (): JSX.Element => {
  const classes = useStyles();
  const [open, setOpen] = useState(false);

  return (
    <header>
      <AppBar position='static'>
        <Toolbar>
          <IconButton
            edge='start' aria-label='menu' color='inherit'
            onClick={() => setOpen(true)}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant='h6' noWrap color='inherit'>
            <Link className={classes.textWhite} to='/' color='primary'>
              Template
            </Link>
          </Typography>
          <SwipeableDrawer
            anchor='left'
            open={open}
            onClose={() => setOpen(false)}
            onOpen={() => setOpen(true)}
          >
            <div
              onClick={() => setOpen(false)}
              onKeyDown={() => setOpen(false)}
            >
              <List>
                <ListItem button>
                  <Link className={classes.textDark} to='/'>Home</Link>
                </ListItem>
                <ListItem button>
                  <Link className={classes.textDark} to='/counter'>Counter</Link>
                </ListItem>
                <ListItem button>
                  <Link className={classes.textDark} to='/fetch-data'>Fetch data</Link>
                </ListItem>
              </List>
            </div>
          </SwipeableDrawer>
        </Toolbar>
      </AppBar>
    </header>
  );
}

NavMenu.displayName = NavMenu.name;
