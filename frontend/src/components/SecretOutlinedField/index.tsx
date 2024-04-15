import { Label, Visibility, VisibilityOff } from "@mui/icons-material";
import { Box, FilledInputProps, FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, Paper, SxProps, Typography } from "@mui/material";
import { FC, useState } from "react";

interface SecretOutlinedProps extends FilledInputProps {
    label?: string
    sx?: SxProps
    error?: boolean
}
 
const SecretOutlinedField: FC<SecretOutlinedProps> = ({label, sx, error, ...props}: SecretOutlinedProps) => {
    const [show, setShow] = useState(false)

    return ( 
<FormControl variant="outlined" sx={sx} error={error}>
    <InputLabel sx={{background:""}} variant="outlined">{label}</InputLabel>
    <OutlinedInput
    {...props}
    label={label}
    type={show ? 'text' : 'password'}
    endAdornment={
        <InputAdornment position="end" >
            <IconButton
                aria-label="toggle password visibility"
                onClick={() => setShow(s => !s)}
                onMouseDown={e => e.preventDefault()}
                edge="end"
            >
            {show ? <VisibilityOff /> : <Visibility />}
        </IconButton>
        </InputAdornment>
    }
    />
</FormControl>
     );
}
 
export default SecretOutlinedField;