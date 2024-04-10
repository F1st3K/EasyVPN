import { Visibility, VisibilityOff } from "@mui/icons-material";
import { FilledInput } from "@mui/material";
import FormControl from "@mui/material/FormControl";
import IconButton from "@mui/material/IconButton";
import InputAdornment from "@mui/material/InputAdornment";
import InputLabel from "@mui/material/InputLabel";
import { FC, useState } from "react";

interface SecretFieldProps {
    label?: string
    value?: unknown
    onChange?: React.ChangeEventHandler<HTMLInputElement | HTMLTextAreaElement> | undefined
}
 
const SecretFilledField: FC<SecretFieldProps> = (props: SecretFieldProps) => {
    const [show, setShow] = useState(false)

    return ( 
<FormControl variant="filled" sx={{width: "22.9ch"}}>
    <InputLabel>{props.label}</InputLabel>
    <FilledInput 
    value={props.value}
    onChange={props.onChange}
    type={show ? 'text' : 'password'}
    endAdornment={
        <InputAdornment position="end">
        <IconButton
            aria-label="toggle password visibility"
            onClick={() => setShow(s => !s)}
            onMouseDown={e => e.preventDefault()}
            edge="start"
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