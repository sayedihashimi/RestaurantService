import { useEffect, useState } from 'react';
import './App.css';
function App() {
    const [forecasts, setForecasts] = useState();
    useEffect(() => {
        populateWeatherData();
    }, []);
    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Category</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.id}>
                        <td>{forecast.name}</td>
                        <td>{forecast.description}</td>
                        <td>{forecast.category}</td>
                        <td>{forecast.price}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    return (
        <div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        const response = await fetch('api/menuitem');
        const data = await response.json();
        setForecasts(data);
    }
}
export default App;