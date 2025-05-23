import { CheckBox, CheckBoxOutlineBlank, Save, SyncLock } from '@mui/icons-material';
import {
    Autocomplete,
    Box,
    Checkbox,
    Chip,
    IconButton,
    TableCell,
    TableRow,
    TextField,
    Typography,
} from '@mui/material';
import React, { FC, useState } from 'react';

import { Role, User } from '../../api';
import CenterBox from '../../components/CenterBox';

interface UserRowProps {
    user: User;
    currentUserId: string;
    onChangeRoles?: (id: string, roles: Role[]) => void;
    onChangePassword?: (id: string, pwd: string) => void;
}

const UserRow: FC<UserRowProps> = (props: UserRowProps) => {
    const [roles, setRoles] = useState<Role[]>(props.user.roles);
    const [newPwd, setNewPwd] = useState<string>();

    return (
        <TableRow hover>
            <TableRow>
                <TableCell rowSpan={2} sx={{ padding: '10px' }}>
                    <Box
                        component="img"
                        src={props.user.icon}
                        sx={{
                            width: '8ch',
                            height: '8ch',
                            borderRadius: '50%',
                            objectFit: 'cover',
                        }}
                    />
                    <CenterBox>
                        <Chip
                            label={props.user.login}
                            color={
                                props.currentUserId === props.user.id
                                    ? 'primary'
                                    : 'secondary'
                            }
                        />
                    </CenterBox>
                </TableCell>
                <TableCell
                    sx={{
                        padding: '10px',
                        whiteSpace: 'nowrap',
                        overflow: 'hidden',
                        textOverflow: 'ellipsis',
                        maxWidth: 'min(calc(50vw - 50px), 25vw)',
                    }}
                >
                    <Typography variant="caption">
                        {props.user.firstName} {props.user.lastName}
                    </Typography>
                </TableCell>
                <TableCell
                    sx={{
                        padding: '10px',
                        whiteSpace: 'nowrap',
                        overflow: 'hidden',
                        textOverflow: 'ellipsis',
                        maxWidth: 'min(calc(50vw - 50px), 30vw)',
                    }}
                >
                    <Typography variant="caption">{newPwd || '********'}</Typography>
                </TableCell>
                <TableCell sx={{ padding: '10px' }}>
                    <IconButton
                        sx={{ marginLeft: '5px' }}
                        onClick={() =>
                            setNewPwd((x) =>
                                x ? undefined : Math.random().toString(36).slice(2, 8),
                            )
                        }
                    >
                        <SyncLock />
                    </IconButton>
                </TableCell>
                <TableCell rowSpan={2} sx={{ padding: '10px' }}>
                    <IconButton
                        sx={{ marginLeft: '5px' }}
                        onClick={() => {
                            !(
                                props.user.roles.length === roles.length &&
                                props.user.roles.every((item) => roles.includes(item)) &&
                                roles.every((item) => props.user.roles.includes(item))
                            ) && props.onChangeRoles?.(props.user.id, roles);
                            newPwd && props.onChangePassword?.(props.user.id, newPwd);
                            setNewPwd(undefined);
                        }}
                        disabled={
                            props.user.roles.length === roles.length &&
                            props.user.roles.every((item) => roles.includes(item)) &&
                            roles.every((item) => props.user.roles.includes(item)) &&
                            newPwd === undefined
                        }
                    >
                        <Save />
                    </IconButton>
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell colSpan={3} sx={{ padding: '10px' }}>
                    <Autocomplete
                        multiple
                        options={Object.values(Role)}
                        limitTags={3}
                        disableCloseOnSelect
                        getOptionLabel={(option) => option}
                        renderOption={(rprops, option, { selected }) => {
                            const { ...optionProps } = rprops;
                            return (
                                <li {...optionProps}>
                                    <Checkbox
                                        icon={<CheckBoxOutlineBlank fontSize="small" />}
                                        checkedIcon={<CheckBox fontSize="small" />}
                                        style={{ marginRight: 8 }}
                                        checked={selected}
                                        disabled={
                                            option === Role.SecurityKeeper &&
                                            props.user.roles.includes(
                                                Role.SecurityKeeper,
                                            ) &&
                                            props.currentUserId === props.user.id
                                        }
                                    />
                                    {option}
                                </li>
                            );
                        }}
                        style={{ maxWidth: 'min(calc(100vw - 50px), 55vw)', width: 700 }}
                        renderInput={(params) => <TextField {...params} label="Roles" />}
                        value={roles}
                        onChange={(_, roles) =>
                            setRoles(
                                props.user.roles.includes(Role.SecurityKeeper) &&
                                    props.currentUserId === props.user.id
                                    ? [
                                          Role.SecurityKeeper,
                                          ...roles.filter(
                                              (r) => r !== Role.SecurityKeeper,
                                          ),
                                      ]
                                    : roles,
                            )
                        }
                    />
                </TableCell>
            </TableRow>
        </TableRow>
    );
};

export default UserRow;
