import React from 'react';
import './App.css';

function App() {
    const [info, setInfo] = React.useState<any>();

    React.useEffect(() => {
        fetch("http://easy-vpn-backend:80/auth/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: `{
            "firstName": "Freak",
            "lastName": "Fister",
            "login": "F1st3K",
            "password": "fisty123"
        }`,
    }).then(response => response.json().then(t => setInfo(t.title)));
    }, [])
    return (
        <div className="App">
        <header className="App-header">
            <p>
                Edit <code>src/App.tsx</code> and save to reload.
            </p>
            {info}
            <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
            >
                Learn React
            </a>
        </header>
        </div>
    );
}

export default App;
