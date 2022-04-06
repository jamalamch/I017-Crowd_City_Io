using System;
using System.Collections;
using System.Collections.Generic;
using UIParty;
using UnityEngine;

public class Root : MonoBehaviour
{
    private static Root _instance;
    private static bool _initDone = false;


    public static GameManager GameManager => _instance._gameManager;
    public static UIManager UIManager => _instance._uIManager;
    public static DataManager DataManager => _instance._dataManager;

    public static DataLevelValue Level => DataManager.levelData;
    public static DataValue Currency => DataManager.coinData;


    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private DataManager _dataManager;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _dataManager.Init();
        _uIManager.Init();
        _initDone = true;
    }

    private void Reset()
    {
        GameManager.Reset();
    }

    internal static void StartLevel()
    {
        UIManager.GameStarted();
    }

    public static void EndWindowStartClose()
    {
    }

    internal static void LevelWon(int levelGain)
    {
        UIManager.OpenWinWindow();
    }

    public static void LevelWonClosed()
    {
        Level.LevelUp();
    }

    internal static void LevelLost(int levelGain)
    {
        UIManager.OpenLoseWindow();
    }

    public static void LevelLoseClosed()
    {
        Level.SetProgression(0);
    }

    public static void ChangeLevel()
    {
    }

    public static void TransitionScreenFull()
    {
        _instance.Reset();
    }

    public static void TransitionScreenClosed()
    {
        UIManager.OpenMenu();
    }
}
