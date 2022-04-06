using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _gameStarted = false;
    private int _levelGain;

    public bool gameStart => _gameStarted;

    public void Init()
    {
    }

    public void Reset()
    {
        _levelGain = 0;
        _gameStarted = false;
        Root.Level.SetProgression(0.5f);
    }

    public void StartGame()
    {
        _gameStarted = true;
        Root.StartLevel();
    }

    public void GameWin()
    {
        if (_gameStarted)
        {
            _gameStarted = false;
            Root.Level.SetProgression(1);
            Debug.Log("Win Game");
            Root.LevelWon(_levelGain);
        }
    }

    public void LoseGame()
    {
        if (_gameStarted)
        {
            _gameStarted = false;
            Root.Level.SetProgression(0);
            Debug.Log("lose Game");
            Root.LevelLost(_levelGain);
        }
    }

    internal void GainCurrency(int score)
    {
        _levelGain += score;
        Root.Currency.AddValue(score);
    }
}
