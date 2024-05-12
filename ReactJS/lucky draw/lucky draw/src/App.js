// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Home from './components/Home';
import Page1 from './components/Page1';
import Page2 from './components/Page2';
import Page3 from './components/Page3';
import Page4 from './components/Page4';
import firebase from 'firebase/compat/app';
import 'firebase/compat/storage';


// Thay thế bằng cấu hình Firebase của bạn
const firebaseConfig = {
  apiKey: "AIzaSyB-bF9ZGLmXVxrssJWE3biWpXCou4rtmFA",
  authDomain: "lottery-4803d.firebaseapp.com",
  projectId: "lottery-4803d",
  storageBucket: "lottery-4803d.appspot.com",
  messagingSenderId: "1004040332320",
  appId: "1:1004040332320:web:7390c2aef7ad30c6d28845",
  measurementId: "G-BWVC5WLDWC"
};
// Khởi tạo Firebase
firebase.initializeApp(firebaseConfig);

const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/page1" element={<Page1 />} />
        <Route path="/page2" element={<Page2 />} />
        <Route path="/page3/:id" element={<Page3 />} />
        <Route path="/page4/:id" element={<Page4 />} />
        <Route path="/" element={<Home />} />
      </Routes>
    </Router>
  );
};

export default App;
