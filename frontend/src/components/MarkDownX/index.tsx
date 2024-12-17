import '@mdxeditor/editor/style.css';
import './style.css';

import { MDXEditor } from '@mdxeditor/editor';
import { CancelSharp, Edit, SaveAs } from '@mui/icons-material';
import { Fab, useTheme } from '@mui/material';
import React, { useEffect, useState } from 'react';

import { GetAllPlugins } from './_boilerplate';

interface MarkDownXProps {
    md: string;
    mdInit: string;
    editable?: boolean;
    onChange?: (md: string) => void;
    onSave?: (md: string) => void;
}

const MarkDownX = (props: MarkDownXProps) => {
    const theme = useTheme().palette.mode;
    const [readonly, setReadonly] = useState(true);
    const [md, setMd] = useState(props.md);
    const plugins = () =>
        GetAllPlugins(theme === 'dark', readonly !== true, props.mdInit);

    useEffect(() => {
        props.onChange && props.onChange(md);
    }, [md]);

    useEffect(() => {
        setMd(props.mdInit);
    }, [props.mdInit]);

    return (
        <>
            <MDXEditor
                key={String(readonly).concat(theme).concat(props.mdInit)}
                readOnly={readonly}
                className={`${theme}-theme ${theme}-editor ${theme}-code`}
                markdown={md}
                onChange={setMd}
                plugins={plugins()}
            />
            {props.editable && readonly && (
                <Fab
                    aria-label="add"
                    color="primary"
                    sx={{
                        position: 'fixed',
                        height: '8ch',
                        width: '8ch',
                        top: '10ch',
                        right: '4ch',
                    }}
                    onClick={() => setReadonly(false)}
                >
                    <Edit />
                </Fab>
            )}
            {readonly !== true && (
                <>
                    <Fab
                        color="secondary"
                        aria-label="save"
                        sx={{
                            position: 'fixed',
                            height: '8ch',
                            width: '8ch',
                            top: '15ch',
                            right: '4ch',
                        }}
                        onClick={() => {
                            props.onSave && props.onSave(md);
                            setReadonly(true);
                        }}
                    >
                        <SaveAs />
                    </Fab>
                    <Fab
                        color="warning"
                        aria-label="cancel"
                        size="small"
                        sx={{
                            position: 'fixed',
                            top: '22ch',
                            right: '1ch',
                        }}
                        onClick={() => {
                            setMd(props.mdInit);
                            setReadonly(true);
                        }}
                    >
                        <CancelSharp />
                    </Fab>
                </>
            )}
        </>
    );
};

export default MarkDownX;
