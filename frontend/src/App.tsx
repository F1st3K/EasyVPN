import React from 'react';

import AuthProvider from './providers/AuthProvider';
import AutoThemeProvider from './providers/AutoThemesProvider';
import RoutesProvider from './providers/RoutesProvider';

function App() {
    return (
        <AutoThemeProvider>
            <AuthProvider>
                <RoutesProvider />
            </AuthProvider>
        </AutoThemeProvider>
    );
}

export default App;
