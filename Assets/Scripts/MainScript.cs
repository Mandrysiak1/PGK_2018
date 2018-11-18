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
            return player.BeersHandedOut;
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
    private float _time = 0f;
    public float GameTime
    {
        get
        {
            return _time;
        }
    }
    System.Random randomNum = new System.Random();


    List<Table> _awaitingTables = new List<Table>();
    public List<Table> AwaitingTables {
        get
        {
            return _awaitingTables;
        }
        set
        {
            _awaitingTables = value;
        }

    }

    List<Table> _freeTables = new List<Table>();
    public List<Table> FreeTables
    {
        get
        {
            return _freeTables;
        }
        set
        {
             _freeTables = value;
        }

    }

    private Player player;
    [SerializeField]
    private PlayerPlate PlayerPlate;
    [SerializeField]
    private QTEController QTE;
    [SerializeField]
    private AudioSource Music;
    [SerializeField]
    private GameObject Player;
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

    public void Setup(GameLevel level, LevelScene scene)
    {
        Music.clip = level.Music;
        Music.time = 0;
        Music.Play();

        scene.Player = player;
        scene.Main = this;

        Camera.transform.position = scene.CameraStartingPosition.position;
        Camera.transform.rotation = scene.CameraStartingPosition.rotation;

        Player.gameObject.SetActive(true);
        Player.transform.position = scene.PlayerStartingPosition.transform.position;
        Player.GetComponent<ThirdPersonCharacter>().setm_AnimSpeedMultiplier(0.85f + 0.25f * UpgradeClass.SpeedModif);
        Player.GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.85f + 0.25f * UpgradeClass.SpeedModif);
        foreach (var listener in FindObjectsOfType<LevelLoadListener>())
        {
            listener.LevelLoaded.Invoke(level, scene);
        }
    }

    private void Awake()
    {
        if (QTE == null)
            QTE = FindObjectOfType<QTEController>();
        player = new Player(PlayerPlate);
        Player.gameObject.SetActive(false);
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
        player.ResetBeersHandedOut();
    }
    public void AddFreeTable(object table)
    {
        Debug.Log("Added table " + ((Table)table).ID);
        _freeTables.Add((Table)table);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public float GetTime()
    {
        return _time;
    }
    void Update()
    {
        _time += Time.deltaTime;
        if(!QTE.IsRunning)
            ChangeDissatisfactionValue();
        GameOver();
        if(Input.GetKeyDown("p"))
        {
            WinStreak += 1;
        }
    }
    private void DissatisfactionValueListener(float arg0, float arg1)
    {
        WinStreak = 0;
    }



    private void ChangeDissatisfactionValue()
    {
        int i = 0;
        int threshold = 4;
        foreach (Table t in _awaitingTables)
        {
            if (t.Mood < threshold) i++;
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
                Player.SetActive(false);
                CurrentGameState = GameState.Failure;
                player.ResetPlate();
                UpgradeClass.Tip = UpgradeClass.preGameTip;
            }

            if (_time >= 124)
            {
                Player.SetActive(false);
                CurrentGameState = GameState.Success;
                player.ResetPlate();
            }
        }


    }
}


