import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import './css/Page2.css'
const Page2 = () => {
    const navigate = useNavigate();
    const [data, setData] = useState([]);

    useEffect(() => {
        // Gọi API ở đây và cập nhật state "data"
        // Ví dụ:
        const fetchData = async () => {
            try {
                const response = await fetch('https://lotteryapi20240124145714.azurewebsites.net/api/Prize');
                if (!response.ok) {
                    throw new Error('Failed to fetch data');
                }
                const result = await response.json();
                setData(result);
            } catch (error) {
                console.error('Error fetching data:', error.message);
            }
        };

        fetchData();
    }, []); // Rỗng để đảm bảo chỉ gọi API một lần khi component được mount

    const handleButtonClick1 = (id) => {
        navigate(`/page3/${id}`);
    };
    const handleButtonClick2 = (id) => {
        navigate(`/page4/${id}`);
    };

    return (
        <div className='bodyPage2'>
            <Header />
            <div className='mainPage2'>
                <h2 className='h2page2'>CƠ CẤU GIẢI THƯỞNG</h2>
                <div className='listPrize'>
                    <ul >
                        {data.map((item) => (
                            <li key={item.id}>
                                <h4>{item.prizeName}</h4>
                                <p>{item.prizeDescription} (Số lượng: {item.quantity})</p>
                                <button className='button1' onClick={() => handleButtonClick1(item.id)}>Quay Số</button>
                                {/* <button className='button2' onClick={() => handleButtonClick2(item.id)}>Lịch Sử</button> */}
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
};

export default Page2;
