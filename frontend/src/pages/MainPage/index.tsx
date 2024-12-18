import React, { useContext } from 'react';
import { FC } from 'react';

import { Context } from '../..';
import { Role } from '../../api';
import MarkDownX from '../../modules/MarkDownX';

const MainPage: FC = () => {
    const { Auth } = useContext(Context);

    return (
        <>
            <MarkDownX
                /// TODO: replace Administrator to PageModerator
                editable={Auth.roles.includes(Role.Administrator)}
                onSave={(pr) => console.log(pr)}
                md={`---
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
                mdInit={`---
name: How coding now!?
date: 01/12/2024
path: /Main/guidlines
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
        </>
    );
};
export default MainPage;
