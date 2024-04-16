import RoutesProvider from "./providers/RoutesProvider";
import AuthProvider from "./providers/AuthProvider";
import AutoThemeProvider from "./providers/AutoThemesProvider";


function App() {
    return (
    <AutoThemeProvider>
        <AuthProvider>
            <RoutesProvider/>
        </AuthProvider>
    </AutoThemeProvider>
    );
}

export default App;
