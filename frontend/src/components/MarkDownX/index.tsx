import '@mdxeditor/editor/style.css';
import './style.css';

import { MDXEditor } from '@mdxeditor/editor';
import { useTheme } from '@mui/material';
import React, { ReactNode, useEffect, useState } from 'react';

import { GetAllPlugins } from './_boilerplate';

interface MarkDownXProps {
    md: string;
    prevMd: string;
    readonly?: boolean;
    onChange?: (md: string) => void;
    onSave?: (md: string) => void;
}

const MarkDownX = (props: MarkDownXProps) => {
    const theme = useTheme().palette.mode;
    const plugins = () =>
        GetAllPlugins(theme === 'dark', props.readonly !== true, props.prevMd);

    const [rerenders, setRerenders] = useState(0);

    useEffect(() => {
        setRerenders((r) => r + 1);
    }, [theme, props.readonly, props.prevMd]);

    return (
        <MDXEditor
            key={rerenders}
            readOnly={props.readonly}
            className={`${theme}-theme ${theme}-editor ${theme}-code`}
            markdown={props.md}
            onChange={props.onChange}
            plugins={plugins()}
        />
    );
};

export default MarkDownX;
