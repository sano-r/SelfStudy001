import {useState, useEffect} from 'react';
import PizzaList from './PizzaList';

const term = "Pizza";

function Pizza(){
    const [data, setData] = useState([]);
    const [maxId, setMaxId] = useState(0);

    useEffect(()=>{
        fetchPizzaData();
    }, []);

    const fetchPizzaData = () => {
        // Simulate fetching data from API
        const pizzaData = [
            {id: 1, name: 'Margherita', description: 'Tomato sause, mozzarella, and basil'},
            {id: 2, name: 'Pepperoni', description: 'Tomato sause, mozzarella, and pepperoni'},
            {id: 3, name: 'Hawaiian', description: 'Tomato sause, mozzarella, and pineapple'},
        ];

        setData(pizzaData);
        setMaxId(Math.max(...pizzaData.map(pizza => pizza.id)));
    };

    const handleCreate = (item) => {
        // Simulate creating item on API
        const newItem = {...item, id: data.length + 1};
        setData([...data, newItem]);
        setMaxId(maxId + 1);
    };

    const handleUpdate = (item) => {
        // Simulate updating item on API
        const updatedData = data.map(pizza => pizza.id === item.id ? item : pizza);
        setData(updatedData);
    };

    const handleDelete = (id) => {
        // Simulate deleting item on API
        const updatedData = data.filter(pizza => pizza.id !== id); // 削除していないピザで配列を作る
        setData(updatedData);
        console.log('aaa');
    };

    return(
        <div>
            <PizzaList
                name={term}
                data={data}
                onCreate={handleCreate}
                onUpdate={handleUpdate}
                onDelete={handleDelete}
            />
        </div>
    );
}

export default Pizza;