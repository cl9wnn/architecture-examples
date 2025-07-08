import React from 'react';
import {Routes, Route} from 'react-router-dom';
import MainPage from '../pages/MainPage';
import { ToastContainer } from 'react-toastify';

const App: React.FC = () => {
  return (
    <>
    <Routes>
      <Route path="/" element={<MainPage />} />
    </Routes>
  <ToastContainer position="top-right" autoClose={3000} />
    </>);
};

export default App;