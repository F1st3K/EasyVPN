import '@mdxeditor/editor/style.css';
import './style.css';

import { MDXEditor } from '@mdxeditor/editor';
import { CancelSharp, Edit, SaveAs } from '@mui/icons-material';
import { Box, Fab, useTheme } from '@mui/material';
import React, { useState } from 'react';

import { HeaderSpace } from '../Header';
import { GetAllPlugins } from './_boilerplate';

interface MarkDownXProps {
    uniqKey: () => string;
    mdInit: string;
    editable?: boolean;
    isEdit?: boolean;
    onChange?: (md: string) => void;
    onSave?: (md: string) => void;
}

const MarkDownX = (props: MarkDownXProps) => {
    const theme = useTheme().palette.mode;
    const [readonly, setReadonly] = useState(!(props.isEdit ?? false));
    const plugins = () =>
        GetAllPlugins(theme === 'dark', readonly !== true, props.mdInit);

    return (
        <Box>
            <MDXEditor
                key={props.uniqKey().concat(theme).concat(String(readonly))}
                readOnly={readonly}
                className={`${theme}-theme ${theme}-editor ${theme}-code`}
                markdown={props.mdInit}
                onChange={(md) => props.onChange && props.onChange(md)}
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
                                props.onSave && props.onSave(props.mdInit);
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
                                props.onChange && props.onChange(props.mdInit);
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
