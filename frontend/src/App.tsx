import useRequest from './hooks/useRequest';
import { AxiosError, AxiosResponse } from 'axios';
import EasyVpn, { ApiError, User } from './api';



function App() {
    const [data, loading, error] = 
        useRequest<AxiosResponse<User>, AxiosError<ApiError>>(() =>
            EasyVpn.auth.login("F1st3K", "fisty123"))

    if (loading)
        return (<>Loading...</>)

    if (error != null) {
        console.error(error)
        return (<>{error.response?.data.title}</>)
    }

    return (
        <div className="App">
        <header className="App-header">
            <p>
                Edit <code>src/App.tsx</code> and save to reload.
            </p>
            {data?.data.firstName}
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
