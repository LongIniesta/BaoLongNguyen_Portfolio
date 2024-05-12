import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import Header from './Header';
import './css/Page4.css';
const Page4 = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [data, setData] = useState(null);

    useEffect(() => {
        // Gọi API ở đây và cập nhật state "data"
        // Ví dụ:
        const fetchData = async () => {
            try {
                const response = await fetch('https://lotteryapi20240124145714.azurewebsites.net/api/Prize/' + id);
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
    }, null); // Rỗng để đảm bảo chỉ gọi API một lần khi component được mount
    if (!data) {
        return <p>Loading...</p>;
    }

    if (data.quantity === 1)
        return (
            <div className='bodyPage4'>
                <Header />
                <div className='mainPage4'>
                    <h2 className='h2Prize1'>{data.prizeName}</h2>
                    <h3>{data.prizeDescription}</h3>
                    <img className='imgPrize' src={data.imageLink} />
                    <div className='divPrize1'>
                        <h1 className='prize1'>{data.members[0].memberName}</h1>
                        <h1 className='prize1Phongban'>- {data.members[0].phoneNumber}</h1>
                    </div>
                </div>
            </div>
        );
    if (data.quantity === 2)
        return (
            <div className='bodyPage4'>
                <Header />
                <div className='mainPage4'>
                    <h2 className='h2Prize2'>{data.prizeName}</h2>
                    <h3>{data.prizeDescription}</h3>
                    <img className='imgPrize2' src={data.imageLink} />
                    {data.members[0] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[0].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[0].phoneNumber}</h1>
                        </div>
                    }
                    {data.members[1] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[1].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[1].phoneNumber}</h1>
                        </div>
                    }
                </div>
            </div>
        );
    if (data.quantity === 3)
        return (
            <div className='bodyPage4'>
                <Header />
                <div className='mainPage4'>
                    <h2 className='h2Prize2'>{data.prizeName}</h2>
                    <h3>{data.prizeDescription}</h3>
                    <img className='imgPrize2' src={data.imageLink} />
                    {data.members[0] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[0].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[0].phoneNumber}</h1>
                        </div>
                    }
                    {data.members[1] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[1].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[1].phoneNumber}</h1>
                        </div>
                    }
                    {data.members[2] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[2].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[2].phoneNumber}</h1>
                        </div>
                    }
                </div>
            </div>
        );
    if (data.quantity === 4)
        return (
            <div className='bodyPage4'>
                <Header />
                <div className='mainPage4'>
                    <h2 className='h2Prize2'>{data.prizeName}</h2>
                    <h3>{data.prizeDescription}</h3>
                    <img className='imgPrize2' src={data.imageLink} />
                    {data.members[0] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[0].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[0].phoneNumber}</h1>
                        </div>
                    }
                    {data.members[1] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[1].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[1].phoneNumber}</h1>
                        </div>
                    }
                    {data.members[2] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[2].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[2].phoneNumber}</h1>
                        </div>
                    }
                    {data.members[3] &&
                        <div className='divPrize1'>
                            <h1 className='prize2'>{data.members[3].memberName}</h1>
                            <h1 className='prize2Phongban'>- {data.members[3].phoneNumber}</h1>
                        </div>
                    }
                </div>
            </div>
        );
    if (data.quantity === 30)
        return (
            <div className='bodyPage4'>
                <Header />
                <div className='mainPage4'>
                    <h2 className='h2Prizekk'>{data.prizeName}</h2>
                    <h3>{data.prizeDescription}</h3>
                    <img className='imgPrize2' src={data.imageLink} />
                    <div className='div30'>
                        <div className='div10'>
                            {data.members[0] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[0].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[0].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[1] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[1].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[1].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[2] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[2].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[2].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[3] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[3].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[3].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[4] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[4].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[4].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[5] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[5].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[5].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[6] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[6].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[6].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[7] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[7].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[7].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[8] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[8].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[8].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[9] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[9].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[9].phoneNumber}</h1>
                                </div>
                            }
                        </div>
                        <div className='div10'>
                            {data.members[10] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[10].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[10].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[11] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[11].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[11].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[12] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[12].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[12].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[13] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[13].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[13].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[14] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[14].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[14].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[15] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[15].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[15].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[16] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[16].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[16].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[17] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[17].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[17].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[18] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[18].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[18].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[19] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[19].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[19].phoneNumber}</h1>
                                </div>
                            }
                        </div>
                        <div className='div10'>
                            {data.members[20] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[20].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[20].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[21] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[21].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[21].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[22] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[22].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[22].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[23] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[23].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[23].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[24] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[24].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[24].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[25] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[25].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[25].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[26] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[26].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[26].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[27] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[27].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[27].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[28] &&
                                <div className='divPrize30'>
                                    <h1 className='prize30'>{data.members[28].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[28].phoneNumber}</h1>
                                </div>
                            }
                            {data.members[29] &&
                                <div className='divPrize30l'>
                                    <h1 className='prize30'>{data.members[29].memberName}</h1>
                                    <h1 className='prize30Phongban'>{data.members[29].phoneNumber}</h1>
                                </div>
                            }
                        </div>
                    </div>
                </div>
            </div>
        );
};

export default Page4;
