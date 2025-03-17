import '@mdxeditor/editor/style.css';
import './style.css';

import { MDXEditor } from '@mdxeditor/editor';
import { CancelSharp, Edit, SaveAs } from '@mui/icons-material';
import { Box, Fab, useTheme } from '@mui/material';
import React, { useEffect, useState } from 'react';

import { HeaderSpace } from '../Header';
import { GetAllPlugins } from './_boilerplate';

interface MarkDownXProps {
    mdInit: string;
    editable?: boolean;
    isEdit?: boolean;
    onChange?: (md: string) => void;
    onSave?: (md: string) => void;
}

const MarkDownX = (props: MarkDownXProps) => {
    const theme = useTheme().palette.mode;
    const [readonly, setReadonly] = useState(!(props.isEdit ?? false));
    const [md, setMd] = useState(props.mdInit);
    const plugins = () =>
        GetAllPlugins(theme === 'dark', readonly !== true, props.mdInit);

    useEffect(() => {
        props.onChange && props.onChange(md);
    }, [md]);

    useEffect(() => {
        setMd(props.mdInit);
    }, [props.mdInit]);

    return (
        <Box>
            <MDXEditor
                key={String(readonly).concat(theme).concat(props.mdInit)}
                readOnly={readonly}
                className={`${theme}-theme ${theme}-editor ${theme}-code`}
                markdown={md}
                onChange={setMd}
                plugins={plugins()}
            />
            <Box position="fixed" top="8ch" right="4ch" zIndex={2}>
                <HeaderSpace />
                {props.editable && readonly && (
                    <Fab
                        aria-label="add"
                        color="primary"
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
                            onClick={() => {
                                setMd(props.mdInit);
                                setReadonly(true);
                            }}
                        >
                            <CancelSharp />
                        </Fab>
                    </>
                )}
            </Box>
        </Box>
    );
};

export default MarkDownX;
