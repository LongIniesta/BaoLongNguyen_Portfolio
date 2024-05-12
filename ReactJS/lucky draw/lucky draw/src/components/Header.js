// src/components/Header.js
import React from 'react';
import { Link } from 'react-router-dom';
import './css/Header.css';

const Header = () => {
    return (
        <header>
            <Link className='trangchu' to='/'>
                <h1 >Trang chủ</h1>
                {/* <img className='logo' src="https://firebasestorage.googleapis.com/v0/b/lottery-4803d.appspot.com/o/Logo%2020%20n%C4%83m.png?alt=media&token=5fcf18a0-fa13-4053-91fa-ba355271a395" alt="Logo" id="logo" /> */}
            </Link>
            {/* <h1>Hợp Trí</h1> */}
            <nav>
                <ul>
                    <li>
                        <Link to="/page1">Tạo giải thưởng</Link>
                    </li>
                    <li>
                        <Link to="/page2">Quay số may mắn</Link>
                    </li>
                </ul>
            </nav>
        </header>
    );
};

export default Header;
