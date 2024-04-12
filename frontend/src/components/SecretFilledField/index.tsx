import { Visibility, VisibilityOff } from "@mui/icons-material";
import { FilledInput, FilledInputProps, SxProps } from "@mui/material";
import FormControl from "@mui/material/FormControl";
import IconButton from "@mui/material/IconButton";
import InputAdornment from "@mui/material/InputAdornment";
import InputLabel from "@mui/material/InputLabel";
import { FC, useState } from "react";

interface SecretFieldProps extends FilledInputProps {
    label?: string
    sx?: SxProps
    error?: boolean
}
 
const SecretFilledField: FC<SecretFieldProps> = ({label, sx, error, ...props}: SecretFieldProps) => {
    const [show, setShow] = useState(false)

    return ( 
<FormControl variant="filled" sx={sx} error={error}>
    <InputLabel>{label}</InputLabel>
    <FilledInput
    {...props}
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
 
export default SecretFilledField;