// src/components/Page1.js
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import firebase from 'firebase/compat/app';
import 'firebase/compat/storage';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './css/Page1.css';

const Page1 = () => {
    const [loading, setLoading] = useState(false);
    const [image, setImage] = useState(null);

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            setImage(file);
        }
    };
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        quantity: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            setLoading(true);

            //lưu ảnh lên firebase
            const storageRef = firebase.storage().ref();
            const fileRef = storageRef.child(image.name);
            await fileRef.put(image);
            const url = await fileRef.getDownloadURL();
            console.log('Download URL:', url);
            //url là link ảnh vừa lưu

            const { name, description, quantity } = formData;
            const response = await fetch('https://lotteryapi20240124145714.azurewebsites.net/api/Prize', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    id: 0,
                    prizeName: name,
                    imageLink: url,
                    quantity: parseInt(quantity),
                    prizeDescription: description,
                    members: []
                }),
            });

            if (!response.ok) {
                throw new Error('Failed to submit form');
            } setFormData({
                name: '',
                description: '',
                quantity: ''
            });

            setLoading(false);
            toast("Thêm giải thưởng thành công");
            console.log('Server response:', response);
        } catch (error) {
            setLoading(false);
            console.error('Error submitting form:', error.message);
        }
    };

    return (
        <div className='page1main'>
            <ToastContainer />
            <Header />
            <div className='page1screen'>
                <div style={{}}>
                    <form onSubmit={handleSubmit} style={{ textAlign: 'center' }}>
                        <p className='title'>Thêm giải thưởng mới</p>
                        <label>
                            <p className='minititle'>Tên giải</p>
                            <input
                                type="text"
                                name="name"
                                value={formData.name}
                                onChange={handleChange}
                                autoComplete="new-password"
                            />
                        </label>
                        <label>
                            <p className='minititle'>Mô tả</p>
                            <input
                                type="text"
                                name="description"
                                value={formData.description}
                                onChange={handleChange}
                                autoComplete="new-password"
                            />
                        </label>
                        <label>
                            <p className='minititle'>Số lượng giải</p>
                            <input
                                type="number"
                                name="quantity"
                                value={formData.quantity}
                                onChange={handleChange}
                                autoComplete="new-password"
                            />
                        </label>
                        <input type="file" onChange={handleFileChange} />
                        {!loading && <button className='btnPage1' type="submit">Thêm</button>}
                        {loading && <p className='loading' >Loading...</p>}
                    </form>

                    <br />
                    <br />
                </div>
            </div>

        </div>
    );
};

export default Page1;
