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
        private set
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
    private float time = 0f;
    private float nextOrderTime = 0f;
    private float orderDeadline = 9999f; //IMHO niepotrzebne do niczego //IMOH też 
    System.Random randomNum = new System.Random();


    List<Table> _awaitingTables = new List<Table>();
    public List<Table> AwaitingTables {
        get
        {
            return _awaitingTables;
        }
        set
        {
            AwaitingTables = _awaitingTables;
        }

    }

    List<Table> freeTables = new List<Table>();
    private Player player;
    [SerializeField]
    private PlayerPlate PlayerPlate;
    [SerializeField]
    private QTEController QTE;

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
        if (QTE == null)
            QTE = FindObjectOfType<QTEController>();
        player = new Player(PlayerPlate);
    }

    public void Start()
    {
        
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
        freeTables.Add((Table)table);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public float GetTime()
    {
        return time;
    }
    void Update()
    {
        time += Time.deltaTime;
        ControlOrders();

        if (time >= nextOrderTime)
        {
            GenerateOrder();
            CalculateNextOrderTime();
        }
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

    void CalculateNextOrderTime()
    {
        int offset = randomNum.Next(3, 7);
        nextOrderTime = time + offset;
        //TODO:Wymyśleć jakiś sposób na zmiane czasu;
    }
    public void GenerateOrder()
    {
        if (freeTables.Count != 0)
        {
            int randomTable = randomNum.Next(0, freeTables.Count);
            int sizeOfOrder = randomNum.Next(1, 4);
            Debug.Log("  #SYSINFO: Wygenerowano zamówienie o rozmiarze: " + sizeOfOrder);
            freeTables[randomTable].CurrOrder = new Order(time, time + orderDeadline, sizeOfOrder);

            _awaitingTables.Add(freeTables[randomTable]);
            freeTables.Remove(freeTables[randomTable]);
        }
        else
        {
            Debug.Log("  #SYSINFO: Nie wygenerowano zamówienia,wszystkie stoliki zajęte!");
        }
    }

    public void ControlOrders()
    {
        for (int i = _awaitingTables.Count - 1; i >= 0; i--)
        {
            Table x = _awaitingTables[i];
            var shouldBeFree = x.ControlOrder(time, moodDecreaseValue);
            if (shouldBeFree == true)
            {
                x.Mood = 20;
                Debug.Log("  #SYSINFO: Zwolniono stolik");
                // +++++++++++++ ADD TO WINSTREAK
                WinStreak += 1;
                // ++++++++++++++++++++++++++++++
                _awaitingTables.RemoveAt(i);
                freeTables.Add(x);
            }
        }
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
        if (DissatisfactionValue >= 100)
        {
            CurrentGameState = GameState.Failure;
            player.SetBeersOnPlateQuantity(0);
        }

        if (time >= 124)
        {
            CurrentGameState = GameState.Success;
            player.SetBeersOnPlateQuantity(0);

            
        }

    }
}


