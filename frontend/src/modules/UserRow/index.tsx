import { CheckBox, CheckBoxOutlineBlank, OpenInNew, SyncLock } from '@mui/icons-material';
import {
    Autocomplete,
    Checkbox,
    Chip,
    IconButton,
    TableCell,
    TableRow,
    TextField,
    Typography,
} from '@mui/material';
import React, { FC } from 'react';

import { Role, User } from '../../api';

interface UserRowProps {
    user: User;
    onChangeRoles?: (id: string, roles: Role[]) => void;
    onChangePassword?: (id: string, pwd: string) => void;
}

const UserRow: FC<UserRowProps> = (props: UserRowProps) => {
    return (
        <TableRow hover>
            <TableCell sx={{ padding: '10px' }}>
                <Chip label={props.user.login} color="primary" />
            </TableCell>
            <TableCell
                sx={{
                    padding: '10px',
                    whiteSpace: 'nowrap',
                    overflow: 'hidden',
                    textOverflow: 'ellipsis',
                    maxWidth: 'min(calc(100vw - 600px), 25vw)',
                }}
            >
                <Typography variant="caption">
                    {props.user.firstName} {props.user.lastName}
                </Typography>
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <Autocomplete
                    multiple
                    id="checkboxes-tags-demo"
                    options={Object.values(Role)}
                    disableCloseOnSelect
                    getOptionLabel={(option) => option}
                    renderOption={(props, option, { selected }) => {
                        const { ...optionProps } = props;
                        return (
                            <li {...optionProps}>
                                <Checkbox
                                    icon={<CheckBoxOutlineBlank fontSize="small" />}
                                    checkedIcon={<CheckBox fontSize="small" />}
                                    style={{ marginRight: 8 }}
                                    checked={selected}
                                />
                                {option}
                            </li>
                        );
                    }}
                    style={{ width: 500 }}
                    renderInput={(params) => <TextField {...params} label="Roles" />}
                    value={props.user.roles}
                    onChange={(_, roles) =>
                        props.onChangeRoles && props.onChangeRoles(props.user.id, roles)
                    }
                />
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <IconButton
                    onClick={() =>
                        props.onChangePassword &&
                        props.onChangePassword(props.user.id, '234')
                    }
                >
                    <SyncLock />
                </IconButton>
            </TableCell>
        </TableRow>
    );
};

export default UserRow;
