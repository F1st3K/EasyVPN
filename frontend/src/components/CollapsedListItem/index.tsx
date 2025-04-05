import { ExpandLess, ExpandMore } from '@mui/icons-material';
import { Collapse, IconButton, List, ListItem, Tooltip } from '@mui/material';
import React, { FC, ReactNode, useState } from 'react';

interface ColapsedListItemProps {
    item: ReactNode;
    children: ReactNode;
    listTooltip?: ReactNode;
    marginLeft?: number;
    isOpen?: boolean;
}

const ColapsedListItem: FC<ColapsedListItemProps> = (props) => {
    const [isOpen, SetIsOpen] = useState<boolean>(props.isOpen ?? false);

    return (
        <>
            <ListItem>
                {props.item}
                <Tooltip
                    title={props.listTooltip}
                    slotProps={{
                        popper: {
                            modifiers: [
                                {
                                    name: 'offset',
                                    options: {
                                        offset: [0, -8],
                                    },
                                },
                            ],
                        },
                    }}
                >
                    <IconButton onClick={() => SetIsOpen(!isOpen)}>
                        {isOpen ? <ExpandLess /> : <ExpandMore />}
                    </IconButton>
                </Tooltip>
            </ListItem>
            <Collapse in={isOpen} timeout="auto" unmountOnExit>
                <List
                    sx={{ marginLeft: props.marginLeft ?? 8 }}
                    component="div"
                    disablePadding
                >
                    {props.children}
                </List>
            </Collapse>
        </>
    );
};

export default ColapsedListItem;
