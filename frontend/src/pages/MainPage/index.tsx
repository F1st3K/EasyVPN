import { Button } from '@mui/material';
import React, { useState } from 'react';
import { FC } from 'react';

import MarkDownX from '../../components/MarkDownX';

const MainPage: FC = () => {
    const [p, setP] = useState(true);
    return (
        <>
            <Button onClick={() => setP((pp) => !pp)}>Click</Button>
            <MarkDownX
                md={`
        ---
name: How coding now!?
date: 11/12/2024
path: /Main/guidlines
---

## Hello

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
                prevMd={'## Hello'}
                readonly={p}
            />
        </>
    );
};
export default MainPage;
