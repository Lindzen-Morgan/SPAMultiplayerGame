// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './components/Login';
import Register from './components/Register';
import Game from './components/Game';
import Profile from './components/Profile';
import Highscore from './components/Highscore';
import Navigation from './components/Navigation';
import TicTacToe from './components/TicTacToe';


function App() {
    return (
        <Router>
            <Routes>
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/game" element={<Game />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/highscore" element={<Highscore />} />
                <Route path="/tic-tac-toe" element={<TicTacToe />} />

            </Routes>
        </Router>
    );
}

export default App;
