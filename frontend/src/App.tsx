import RoutesProvider from "./providers/RoutesProvider";
import AuthProvider from "./providers/AuthProvider";
import AutoThemeProvider from "./providers/AutoThemesProvider";
import { CssBaseline } from "@mui/material";


function App() {
    return (
    <AutoThemeProvider>
        <CssBaseline/>
        <AuthProvider>
            <RoutesProvider/>
        </AuthProvider>
    </AutoThemeProvider>
    );
}

export default App;
