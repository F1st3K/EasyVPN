import { createContext } from 'react';
import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';

import App from './App';
import Store, { AuthStore } from './store';

const root = ReactDOM.createRoot(document.getElementById('root') as HTMLElement);

const store = {
    Auth: new AuthStore(),
};
export const Context = createContext<Store>(store);

root.render(
    <Context.Provider value={store}>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </Context.Provider>,
);
