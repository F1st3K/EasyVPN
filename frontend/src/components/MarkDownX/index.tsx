import '@mdxeditor/editor/style.css';
import './dark-editor.css';

import { MDXEditor } from '@mdxeditor/editor';
import { useTheme } from '@mui/material';
import React from 'react';

import { GetAllPlugins } from './_boilerplate';

interface MarkDownXProps {
    md: string;
    prevMd: string;
    isEdit?: boolean;
    onChange?: (md: string) => void;
    onSave?: (md: string) => void;
}

const MarkDownX = (props: MarkDownXProps) => {
    const theme = useTheme().palette.mode;
    return (
        <MDXEditor
            className={`${theme}-theme ${theme}-editor ${theme}-code`}
            markdown={props.md}
            onChange={props.onChange}
            plugins={GetAllPlugins(props.prevMd, theme === 'dark')}
        />
    );
};

export default MarkDownX;
