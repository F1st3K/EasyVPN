import { useMediaQuery, createTheme, ThemeProvider } from "@mui/material";
import { blue, lightBlue } from "@mui/material/colors";
import { FC, useMemo } from "react";

interface AutoThemeProviderProps {
    children?: React.ReactNode
}
 
const AutoThemeProvider: FC<AutoThemeProviderProps> = (props: AutoThemeProviderProps) => {
    const prefersDarkMode = useMediaQuery('(prefers-color-scheme: dark)');
    const theme = useMemo(
      () => createTheme({
        palette: {
          mode: prefersDarkMode ? 'dark' : 'light',
        },
      }),
      [prefersDarkMode],
    );

    return (
        <ThemeProvider theme={theme}>
            {props.children}
        </ThemeProvider>
        );
}
 
export default AutoThemeProvider;