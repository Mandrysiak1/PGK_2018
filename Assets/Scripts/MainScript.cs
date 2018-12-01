using Assets;
using Assets.PGKScripts;
using Assets.PGKScripts.Enums;
using Assets.PGKScripts.Interfaces;
using Assets.PGKScripts.Perks.WinStreak;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using QTE;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class MainScript : MonoBehaviour, IWinStreakSource
{
    #region EventClasses
    //event classes
    public class GameStatusEvent : UnityEvent<GameState, GameState> { }
    public class DissatisfactionEvent : UnityEvent<float, float> { }


    //end of event classes
    #endregion
    public GameStatusEvent GameStatusChanged { get; set; }
    public WinStreakEvent WinStreakChanged { get; set; }
    public DissatisfactionEvent DissatisfactionChanged { get; set; }
    public bool tutorial = false;

    private GameState gameState = GameState.Playing;
    public GameState CurrentGameState
    {
        get
        {
            return gameState;
        }
        private set
        {
            var temp = this.gameState;
            this.gameState = value;
            GameStatusChanged.Invoke(temp, value);
            //OnPropertyChanged("CurrentGameState");
        }
    }

    public int BeersHandedOut
    {
        get
        {
            return Player.BeersHandedOut;
        }
    }
    public int Score
    {
        get
        {
            return (int)(100 - DissatisfactionValue) + 2 * BeersHandedOut;
        }
    }
    private int _winStreak;
    public int WinStreak
    {
        get
        {
            return _winStreak;
        }
         set
        {
            var temp = this._winStreak;
            _winStreak = value;
            WinStreakChanged.Invoke(temp, value);
            //OnPropertyChanged("WinStreak");
        }
    }
    private float _dissatisfactionValue;
    public float DissatisfactionValue
    {
        get
        {
            return _dissatisfactionValue;
        }
        private set
        {
            var temp = this._dissatisfactionValue;
            _dissatisfactionValue = value;
            DissatisfactionChanged.Invoke(temp, value);
            //OnPropertyChanged("DissatisfactionValue");
        }
    }

    public float moodDecreaseValue;
    [SerializeField]
    private float _time = 0f;
    public float GameTime
    {
        get
        {
            return _time;
        }
    }
    System.Random randomNum = new System.Random();


    [SerializeField]
    private OrderController Orders;
    [SerializeField]
    private Player Player;
    [SerializeField]
    private ThirdPersonCharacter PlayerController;
    [SerializeField]
    private Camera Camera;

    public MainScript()
    {
        if (DissatisfactionChanged == null)
            DissatisfactionChanged = new DissatisfactionEvent();
        if (GameStatusChanged == null)
            GameStatusChanged = new GameStatusEvent();
        if (WinStreakChanged == null)
            WinStreakChanged = new WinStreakEvent();
    }

    private void Awake()
    {
        PlayerController.gameObject.SetActive(false);
    }

    public void Start()
    {
        UpgradeClass.preGameTip = UpgradeClass.Tip;
        UpgradeClass.exited = false;
        if (PlayerPrefs.HasKey("difficultyKey")) moodDecreaseValue = PlayerPrefs.GetFloat("difficultyKey");
        else moodDecreaseValue = 0.3f;
        DissatisfactionChanged.AddListener(DissatisfactionValueListener);
    }

    internal void ResetScore()
    {
        DissatisfactionValue = 0;
        Player.ResetBeersHandedOut();
    }

    public Player GetPlayer()
    {
        return Player;
    }

    public float GetTime()
    {
        return _time;
    }
    void Update()
    {
        _time += Time.deltaTime;
        ChangeDissatisfactionValue();
        if(tutorial == false)
            GameOver();
        if(Input.GetKeyDown("p"))
        {
            WinStreak += 1;
        }
        if (Input.GetKeyDown("o"))
        {
            _time += 30;
        }
    }
    private void DissatisfactionValueListener(float arg0, float arg1)
    {
        WinStreak = 0;
    }



    private void ChangeDissatisfactionValue()
    {
        int i = 0;
        float threshold = 0.25f;
        foreach (OrderSource source in Orders.AwaitingSources)
        {
            if (source.Mood < threshold) i++;
        }
        if (i != 0)
            DissatisfactionValue += Time.deltaTime * 5 * i;

    }
    private void GameOver()
    {
        if (CurrentGameState != GameState.Success && CurrentGameState != GameState.Failure)
        {

            if (DissatisfactionValue >= 100)
            {
                PlayerController.gameObject.SetActive(false);
                CurrentGameState = GameState.Failure;
                Player.ResetPlate();
                UpgradeClass.Tip = UpgradeClass.preGameTip;
            }

            if (_time >= 124)
            {
                PlayerController.gameObject.SetActive(false);
                CurrentGameState = GameState.Success;
                Player.ResetPlate();
            }
        }


    }
}


