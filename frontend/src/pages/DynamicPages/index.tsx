import { Box, Paper } from '@mui/material';
import React, { useContext } from 'react';
import { FC } from 'react';
import { useLocation } from 'react-router-dom';

import { Context } from '../..';
import { Role } from '../../api';
import MarkDownX from '../../modules/MarkDownX';

const DyncamicPages: FC = () => {
    const { Auth } = useContext(Context);
    const dynamicRoute = '/'.concat(useLocation().pathname.split('/').slice(2).join('/'));

    return (
        <Box margin={2} display="flex" justifyContent="center">
            <Paper
                sx={{
                    borderRadius: 2,
                    minWidth: '40ch',
                    width: '100%',
                    maxWidth: '120ch',
                    padding: '10px',
                }}
            >
                <MarkDownX
                    /// TODO: replace Administrator to PageModerator
                    editable={Auth.roles.includes(Role.Administrator)}
                    onSave={(pr) => console.log(pr)}
                    mdInit={`---
name: How coding now!?
date: 01/12/2024
path: ${dynamicRoute}
---

:::youtube[Video]{#bdWFh_udq34}
:::

\`\`\`txt
class Operator : IOperator
{
    public void Activate()
    {
    
    }
}
\`\`\`



***





| how |    |         |
| --- | -- | ------- |
|     | to |         |
|     |    | creaete |
        `}
                />
            </Paper>
        </Box>
    );
};
export default DyncamicPages;
