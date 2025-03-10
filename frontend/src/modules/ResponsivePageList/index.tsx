import { Inbox, Mail } from '@mui/icons-material';
import {
    Divider,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
} from '@mui/material';
import React from 'react';

import PageInfo from '../../api/responses/PageInfo';

export type PageRoutes = {
    page: PageInfo;
    childrens: PageRoutes[];
};

export const ResponsivePageList = (props: {
    parentRoute: string;
    routes: PageRoutes[];
    onNavigate: (route: string) => void;
}) => {
    return (
        <>
            {props.routes.map((route, index) => (
                <List key={route.page.route}>
                    <ListItem disablePadding>
                        <ListItemButton
                            onClick={() =>
                                props.onNavigate(
                                    props.parentRoute + '/' + route.page.route,
                                )
                            }
                        >
                            <ListItemIcon>
                                {index % 2 === 0 ? <Inbox /> : <Mail />}
                            </ListItemIcon>
                            <ListItemText primary={route.page.title} />
                        </ListItemButton>
                    </ListItem>
                    {route.childrens.length > 0 && (
                        <ResponsivePageList
                            parentRoute={props.parentRoute + '/' + route.page.route}
                            routes={route.childrens}
                            onNavigate={props.onNavigate}
                        />
                    )}
                </List>
            ))}
        </>
    );
};

export default ResponsivePageList;
