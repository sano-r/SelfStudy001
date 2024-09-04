import { useState, useEffect } from "react";
import { TextField, Box, Button, List, ListItem, IconButton, ListItemText } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";

function PizzaList({name, data, onCreate, onUpdate, onDelete, error}){
    const [formData, setFormData] = useState({id:'', name:'', description:''});
    const [editingId, setEditingId] = useState(null);

    useEffect(() => {
        if(editingId === null){
            setFormData({id:'', name:'', description:''});
        }else{
            const currentItem = data.find(item => item.id === editingId);
            setFormData(currentItem);
        }
    },[editingId, data]);

    const handleFormChange = (e) => {
        console.log(`handleFormChange: ${e.target.name} ${e.target.value}`);

        const {name, value} = e.target;
        setFormData(prevData => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        console.log(`formData: ${JSON.stringify(formData)}`);

        if (editingId !== null) {
            console.log(`update item: ${JSON.stringify(formData)}`);
            onUpdate(formData);
        } else {
            onCreate(formData);
        }
        setFormData({ id: '', name: '', description: '' });
        setEditingId(null);
    };

    const handleEdit = id => {
        setEditingId(id);
    }

    const handleCancelEdit = () => {
        setFormData({id:'', name:'', description:''});
        setEditingId(null);
    }

    const handleDelete = id => {
        onDelete(id);
    }

    
    return(
        <Box className="Box" sx={{display: 'flex', flexDirection: 'column', alignItems:'center'}}>
            <h2>{name}</h2>
            <form onSubmit={handleSubmit} style={{display:'flex', flexDirection: 'row',alignItems: 'center', gap:8}}>
                <TextField
                    label="Name"
                    name="name"
                    value={formData.name}
                    onChange={handleFormChange}
                />
                <TextField
                    label="Description"
                    name="description"
                    value={formData.description}
                    onChange={handleFormChange}
                />
                <Button sx={{mr:1}} variant="contained" type="submit">{editingId === null ? 'Create' : 'Update'}</Button>
                {editingId !== null && <Button variant="contained" color="secondary" onClick={handleCancelEdit}>Cancel</Button>}
            </form>
            {error && <div>{error.message}</div>}
            <h2>{name}s</h2>
            <List sx={{ width: '100%', maxWidth: 360 }}>
                {data.map(item => (
                    <ListItem key={item.id} secondaryAction={
                        <>
                            <IconButton edge="end" area-label="edit" onClick={() => handleEdit(item.id)}>
                                <Edit />
                            </IconButton>
                            <IconButton edge="end" area-label="delete" onClick={() => onDelete(item.id)}>
                                <Delete />
                            </IconButton>
                        </>
                    }>
                        <ListItemText primary={item.name} secondary={item.description} />
                    </ListItem>
                ))}
            </List>
        </Box>
    );

}

export default PizzaList;