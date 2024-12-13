import React from 'react';
import { FC } from 'react';

import MarkDownX from '../../components/MarkDownX';

const MainPage: FC = () => {
    return <MarkDownX md={'## Hello'} prevMd={'## Hello'} />;
};
export default MainPage;
