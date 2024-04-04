import React from 'react';
import config from './config.json'
import './App.css';

function App() {
    const [info, setInfo] = React.useState<any>();

    React.useEffect(() => {
        fetch(`${config.ApiUrl}/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: `{
            "login": "F1st3K",
            "password": "fisty123"
        }`,
    }).then(response => response.json().then(t => setInfo(t.lastName)));
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
