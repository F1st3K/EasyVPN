import { Box, Link } from '@mui/material';
import React from 'react';

import { PageRoutes } from '../../api/common/PageRoutes';
import CollapsedListItem from '../../components/CollapsedListItem';

export const ResponsivePageList = (props: {
    parentRoute: string;
    routes: PageRoutes[];
    topLevelIsOpen?: boolean;
    onNavigate: (route: string) => void;
}) => {
    const Item = (p: { route: string; title: string }) => {
        return (
            <Link
                underline="hover"
                onClick={() => props.onNavigate(props.parentRoute + '/' + p.route)}
            >
                {p.title}
            </Link>
        );
    };

    return (
        <>
            {props.routes.map((route) => (
                <>
                    {route.childrens.length > 0 && (
                        <CollapsedListItem
                            isOpen={props.topLevelIsOpen}
                            marginLeft={2}
                            key={route.page.route}
                            item={
                                <Item route={route.page.route} title={route.page.title} />
                            }
                            listTooltip="Links"
                        >
                            <ResponsivePageList
                                parentRoute={props.parentRoute + '/' + route.page.route}
                                routes={route.childrens}
                                onNavigate={props.onNavigate}
                            />
                        </CollapsedListItem>
                    )}
                    {route.childrens.length <= 0 && (
                        <Box marginLeft={2}>
                            <Item route={route.page.route} title={route.page.title} />
                        </Box>
                    )}
                </>
            ))}
        </>
    );
};

export default ResponsivePageList;
