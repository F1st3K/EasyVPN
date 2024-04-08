import React from 'react';
import config from './config.json'
import useRequest from './hooks/useRequest';
import ApiError from './api/types/ApiError'
import axios, { AxiosError } from 'axios';

type Planet = {
    name: string
}

function App() {
    const [data, loading, error] = useRequest<Planet, AxiosError<ApiError>>(() => axios.post("http://localhost:80/auth/login", {
            login: "F1st3K",
            password: "fisty1234"
        }))

    if (loading)
        return (<>Loading...</>)

    if (error != null) {
        
        console.error(error)
        return (<>Ошибка</>)
    }

    return (
        <div className="App">
        <header className="App-header">
            <p>
                Edit <code>src/App.tsx</code> and save to reload.
            </p>
            {data?.name}
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
