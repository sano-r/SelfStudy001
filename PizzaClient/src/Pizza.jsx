import {useState, useEffect} from 'react';
import PizzaList from './PizzaList';

const term = "Pizza";
const API_URL = '/pizzas';
const headers = {
    'Content-Type': 'application/json',
};

function Pizza(){
    const [data, setData] = useState([]);
    const [error, setError] = useState(null);

    useEffect(()=>{
        fetchPizzaData();
    }, []);

    const fetchPizzaData = () => {
        // APIからデータを取得
        fetch(API_URL)
            .then(response => response.json())
            .then(data => setData(data))
            .catch(error => setError(error));
    };

    const handleCreate = (item) => {
        // APIで挿入
        console.log(`add item: ${JSON.stringify(item)}`);

        fetch(API_URL, {
            method: 'POST',
            headers,
            body: JSON.stringify({name: item.name, description: item.description}),
        })
            .then(response => response.json())
            .then(returnedItem => setData([...data, returnedItem]))
            .catch(error => setError(error));
    };

    const handleUpdate = (updatedItem) => {
        // APIで更新
        console.log(`add item: ${JSON.stringify(updatedItem)}`);

        fetch(`${API_URL}/${updatedItem.id}`, {
            method: 'PUT',
            headers,
            body: JSON.stringify(updatedItem),
        })
            .then(() => setData(data.map(pizza => pizza.id === updatedItem.id ? updatedItem : pizza)))
            .catch(error => setError(error));

    };

    const handleDelete = (id) => {
        // APIで削除
        fetch(`${API_URL}/${id}`, {
            method: 'DELETE',
            headers,
        })
            .then(() => setData(data.filter(pizza => pizza.id !== id)))
            .catch(error => console.error('Error deleting item:', error));
    };

    return(
        <div>
            <PizzaList
                name={term}
                data={data}
                error={error}
                onCreate={handleCreate}
                onUpdate={handleUpdate}
                onDelete={handleDelete}
            />
        </div>
    );
}

export default Pizza;