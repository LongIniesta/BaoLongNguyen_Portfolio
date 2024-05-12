import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import './css/Page3.css';
import Header from './Header';


const Page3 = () => {
    const [loading, setLoading] = useState(false);
    const [data, setData] = useState([]);
    const [isStopped, setIsStopped] = useState(false);
    const { id } = useParams();
    const [currentIdIndex, setCurrentIdIndex] = useState(0);
    const [list, setList] = useState([]);
    const [concatenatedString, setConcatenatedString] = useState('');
    const [availableIndexes, setAvailableIndexes] = useState([]); // Mảng lưu trữ các index có sẵn
    const [currentAvailableIndex, setCurrentAvailableIndex] = useState(0);
    const [loaded, setLoaded] = useState(false); // Biến đánh dấu đã tải dữ liệu hay chưa
    const [prizeData, setPrizeData] = useState(null);

    const handleConfirmation = async () => {
        const isConfirmed = window.confirm('Bạn có chắc chắn muốn reset không?');

        if (isConfirmed) {
            await resetData(id);
            window.location.reload();
        } else {
        }
    };

    useEffect(() => {
        const fetchPrizeData = async () => {
            try {
                const response = await fetch('https://lotteryapi20240124145714.azurewebsites.net/api/Prize/' + id); // Thay đổi URL của API theo đúng đường dẫn của bạn
                if (!response.ok) {
                    throw new Error('Failed to fetch prize data');
                }

                const prize = await response.json();
                await setPrizeData(prize);
                if (prize.members.length > 0) {
                    setList(prize.members);
                }

            } catch (error) {
                console.error('Error fetching prize data:', error.message);
            }
        };

        fetchPrizeData();
    }, []);

    useEffect(() => {
        // Chỉ tải dữ liệu nếu loaded là false
        if (!loaded) {
            const fetchData = async () => {
                try {
                    const response = await fetch('https://lotteryapi20240124145714.azurewebsites.net/api/Member/TestSheet');
                    if (!response.ok) {
                        throw new Error('Failed to fetch data');
                    }
                    const result = await response.json();
                    setData(result);
                    setAvailableIndexes(Array.from({ length: result.length }, (_, index) => index));
                    setLoaded(true); // Đặt loaded thành true sau khi tải dữ liệu

                } catch (error) {
                    console.error('Error fetching data:', error.message);
                }
            }

            fetchData();
        }
    }, [loaded]);

    useEffect(() => {
        // Tạo interval để chuyển đổi id mỗi giây
        const intervalId = setInterval(() => {
            if (availableIndexes.length > 0) {
                const randomIndex = Math.floor(Math.random() * availableIndexes.length);
                setCurrentIdIndex(availableIndexes[randomIndex]);
                setCurrentAvailableIndex(randomIndex);
            } else {
                setAvailableIndexes(Array.from({ length: data.length }, (_, index) => index));
            }
        }, 100);

        return () => clearInterval(intervalId);
    }, [availableIndexes]);
    if (!prizeData) {
        return <p>Loading...</p>;
    }

    const updateData = async (memberId, prizeId) => {
        try {
            const response = await fetch(`https://lotteryapi20240124145714.azurewebsites.net/api/Prize/${memberId}/${prizeId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (!response.ok) {
                throw new Error('Failed to update data');
            }
            console.log('Data updated successfully');
        } catch (error) {
            console.error('Error updating data:', error.message);
        }
    };

    const resetData = async (prizeId) => {
        try {
            const response = await fetch(`https://lotteryapi20240124145714.azurewebsites.net/api/Prize/${prizeId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if (!response.ok) {
                throw new Error('Failed to update data');
            }
            console.log('Data updated successfully');
        } catch (error) {
            console.error('Error updating data:', error.message);
        }
    };


    const delay = (milliseconds) => new Promise(resolve => setTimeout(resolve, milliseconds));
    const handleConcatenate = async () => {
        setLoading(true);
        if (list.length < prizeData.quantity) {
            await setList(prevList => [...prevList, data[currentIdIndex],]);
            await updateData(data[currentIdIndex].id, id);
            await setAvailableIndexes(prevIndexes => {
                const newIndexes = [...prevIndexes];
                newIndexes.splice(currentAvailableIndex, 1);
                return newIndexes;
            });
            const randomIndex = Math.floor(Math.random() * availableIndexes.length);
            await setCurrentIdIndex(availableIndexes[randomIndex]);
            await setCurrentAvailableIndex(randomIndex);
        } else {
            toast('Đã đủ giải thưởng');
        }
        setLoading(false);
    };
    // if (prizeData.quantity === prizeData.members.length) {
    //     return (
    //         <div className='bodyPage3'>
    //             <Header />
    //             <div className='mainPage3'>
    //                 <h2>Số phần thưởng đã hết</h2>
    //             </div>
    //         </div>
    //     );
    // } else

    return (
        <div className='bodyPage3'>
            <ToastContainer />
            <Header />
            <div className='mainPage3'>
                {prizeData.quantity <= list.length && <div className='divcao' />}
                <h2 className='h21giai'>{prizeData.prizeName}</h2>
                <h3>{prizeData.prizeDescription} (Số lượng: {prizeData.quantity})</h3>
                <img className='imgPrize' src={prizeData.imageLink} />
                {prizeData.quantity > list.length && <p>{data.length > 0 && data[currentIdIndex].memberName} - {data.length > 0 && data[currentIdIndex].phoneNumber}</p>}
                {!loading && prizeData.quantity > list.length && <button onClick={handleConcatenate}>Dừng</button>}
                {loading && prizeData.quantity > list.length && <button>Dừng</button>}
                {prizeData.quantity < 5 && prizeData.quantity > list.length && <div className='listWin'>
                    <ul>
                        {list.map((item, index) => (
                            <li key={index.id}>
                                <div className='listwindiv'>
                                    <h5 className='sttlw'>{index + 1}</h5>
                                    <h5 className='namelw'>{item.memberName}</h5>
                                    <h5>{item.phoneNumber}</h5>
                                </div>
                            </li>
                        ))
                        }
                    </ul>
                </div>}
                {prizeData.quantity < 5 && prizeData.quantity <= list.length && <div className='listWinec'>
                    <ul>
                        {list.map((item, index) => (
                            <li key={index.id}>
                                <div className='listwindiv'>
                                    <h5 className='sttlw'>{index + 1}</h5>
                                    <h5 className='namelw'>{item.memberName}</h5>
                                    <h5>{item.phoneNumber}</h5>
                                </div>
                            </li>
                        ))
                        }
                    </ul>
                </div>}

                {prizeData.quantity > 5 &&
                    <div className='win10'>
                        <div className='win5'>
                            {list[0] &&
                                <div className='win1l'>
                                    <h1 className='win1stt'>1.</h1>
                                    <h1 className='win1ten'>{list[0].memberName}</h1>
                                    <h1 className='win1phongban'>{list[0].phoneNumber}</h1>
                                </div>
                            }
                            {list[1] &&
                                <div className='win1'>
                                    <h1 className='win1stt'>2.</h1>
                                    <h1 className='win1ten'>{list[1].memberName}</h1>
                                    <h1 className='win1phongban'>{list[1].phoneNumber}</h1>
                                </div>
                            }
                            {list[2] &&
                                <div className='win1l'>
                                    <h1 className='win1stt'>3.</h1>
                                    <h1 className='win1ten'>{list[2].memberName}</h1>
                                    <h1 className='win1phongban'>{list[2].phoneNumber}</h1>
                                </div>
                            }
                            {list[3] &&
                                <div className='win1'>
                                    <h1 className='win1stt'>4.</h1>
                                    <h1 className='win1ten'>{list[3].memberName}</h1>
                                    <h1 className='win1phongban'>{list[3].phoneNumber}</h1>
                                </div>
                            }
                            {list[4] &&
                                <div className='win1l'>
                                    <h1 className='win1stt'>5.</h1>
                                    <h1 className='win1ten'>{list[4].memberName}</h1>
                                    <h1 className='win1phongban'>{list[4].phoneNumber}</h1>
                                </div>
                            }
                        </div>
                        <div className='win5'>
                            {list[5] &&
                                <div className='win1'>
                                    <h1 className='win1stt'>6.</h1>
                                    <h1 className='win1ten'>{list[5].memberName}</h1>
                                    <h1 className='win1phongban'>{list[5].phoneNumber}</h1>
                                </div>
                            }
                            {list[6] &&
                                <div className='win1l'>
                                    <h1 className='win1stt'>7.</h1>
                                    <h1 className='win1ten'>{list[6].memberName}</h1>
                                    <h1 className='win1phongban'>{list[6].phoneNumber}</h1>
                                </div>
                            }
                            {list[7] &&
                                <div className='win1'>
                                    <h1 className='win1stt'>8.</h1>
                                    <h1 className='win1ten'>{list[7].memberName}</h1>
                                    <h1 className='win1phongban'>{list[7].phoneNumber}</h1>
                                </div>
                            }
                            {list[8] &&
                                <div className='win1l'>
                                    <h1 className='win1stt'>9.</h1>
                                    <h1 className='win1ten'>{list[8].memberName}</h1>
                                    <h1 className='win1phongban'>{list[8].phoneNumber}</h1>
                                </div>
                            }
                            {list[9] &&
                                <div className='win1'>
                                    <h1 className='win1stt'>10.</h1>
                                    <h1 className='win1ten'>{list[9].memberName}</h1>
                                    <h1 className='win1phongban'>{list[9].phoneNumber}</h1>
                                </div>
                            }
                        </div>
                    </div>
                }
                {prizeData.quantity <= list.length && <button className='btnReset' onClick={handleConfirmation}>Reset</button>}
            </div>

        </div>
    );
};


export default Page3;
