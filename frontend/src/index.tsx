import ReactDOM from 'react-dom/client';
import App from './App';
import { createContext } from 'react';
import Store, { AuthStore } from './store';
import { BrowserRouter } from 'react-router-dom';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const store = {
    Auth: new AuthStore()
}
export const Context = createContext<Store>(store)

root.render(
    <Context.Provider value={store}>
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </Context.Provider>
);

