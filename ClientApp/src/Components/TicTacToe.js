import React, { useState, useEffect } from 'react';
import axios from 'axios';

const TicTacToe = () => {
    const [game, setGame] = useState(null);
    const [message, setMessage] = useState("");

    useEffect(() => {
        startGame();
    }, []);

    const startGame = async () => {
        const response = await axios.post('/api/game/start', { userId: 'testUser' });
        setGame(response.data);
        setMessage("Game started. Player X's turn.");
    };

    const makeMove = async (position) => {
        if (game.isFinished || game.board[position] !== '-') return;

        const response = await axios.post('/api/game/move', {
            gameId: game.id,
            position: position,
            player: game.currentPlayer
        });

        setGame(response.data);

        if (response.data.isFinished) {
            setMessage(response.data.winner === "Draw" ? "It's a draw!" : `Player ${response.data.winner} wins!`);
        } else {
            setMessage(`Player ${response.data.currentPlayer}'s turn.`);
        }
    };

    const renderBoard = () => {
        return game.board.split('').map((cell, index) => (
            <button key={index} onClick={() => makeMove(index)} style={styles.cell}>
                {cell === '-' ? '' : cell}
            </button>
        ));
    };

    return (
        <div style={styles.container}>
            <h1>Tic Tac Toe</h1>
            <div style={styles.board}>
                {game && renderBoard()}
            </div>
            <p>{message}</p>
        </div>
    );
};

const styles = {
    container: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    board: {
        display: 'grid',
        gridTemplateColumns: 'repeat(3, 100px)',
        gridGap: '5px',
    },
    cell: {
        width: '100px',
        height: '100px',
        fontSize: '24px',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        cursor: 'pointer',
    }
};

export default TicTacToe;
