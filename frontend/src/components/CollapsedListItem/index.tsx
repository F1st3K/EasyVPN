import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { Collapse, IconButton, List, ListItem } from '@mui/material';
import React, { FC, ReactNode, useState } from 'react';

interface ColapsedListItemProps {
    item: ReactNode;
    children: ReactNode;
}

const ColapsedListItem: FC<ColapsedListItemProps> = (props: ColapsedListItemProps) => {
    const [isOpen, SetIsOpen] = useState<boolean>(false);

    return (
        <>
            <ListItem>
                <IconButton onClick={() => SetIsOpen(!isOpen)}>{isOpen ? <ExpandLess /> : <ExpandMore />}</IconButton>
                {props.item}
            </ListItem>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
                <List component="div" disablePadding>
                    {props.children}
                </List>
            </Collapse>
        </>
    );
};

export default ColapsedListItem;
