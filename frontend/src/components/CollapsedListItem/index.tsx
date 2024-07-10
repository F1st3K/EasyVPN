import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { Collapse, IconButton, List, ListItem, Tooltip } from '@mui/material';
import React, { FC, ReactNode, useState } from 'react';

interface ColapsedListItemProps {
    item: ReactNode;
    children: ReactNode;
    listTooltip?: ReactNode;
}

const ColapsedListItem: FC<ColapsedListItemProps> = (props: ColapsedListItemProps) => {
    const [isOpen, SetIsOpen] = useState<boolean>(false);

    return (
        <>
            <ListItem>
                {props.item}
                <Tooltip title={props.listTooltip}>
                    <IconButton onClick={() => SetIsOpen(!isOpen)}>
                        {isOpen ? <ExpandLess /> : <ExpandMore />}
                    </IconButton>
                </Tooltip>
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
