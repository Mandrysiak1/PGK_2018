using Assets;
using Assets.PGKScripts;
using Assets.PGKScripts.Enums;
using Assets.PGKScripts.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MainScript : MonoBehaviour, INotifyPropertyChanged, IWinStreakSource
{
    public static readonly Player player = new Player();
    private GameState gameState = GameState.Playing;
    public GameState CurrentGameState
    {
        get
        {
            return gameState;
        }
        private set
        {
            this.gameState = value;
            OnPropertyChanged("CurrentGameState");
        }
    }

    private string _beerCount;
    public string BeerCount
    {
        get
        {
            return _beerCount;
        }
        private set
        {
            _beerCount = value;
            OnPropertyChanged("BeerCount");
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
            _winStreak = value;
            OnPropertyChanged("WinStreak");
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
            _dissatisfactionValue = value;
            OnPropertyChanged("DissatisfactionValue");
        }
    }

    public float moodDecreaseValue;
    private float time = 0f;
    private float nextOrderTime = 0f;
    private float orderDeadline = 9999f; //IMHO niepotrzebne do niczego //IMOH też 
    System.Random randomNum = new System.Random();

    List<Table> awaitingTables = new List<Table>();
    List<Table> freeTables = new List<Table>();

    public event PropertyChangedEventHandler PropertyChanged;

    /*public MainScript()
    {
        
    }*/
    public void Start()
    {
        if (PlayerPrefs.HasKey("difficultyKey")) moodDecreaseValue = PlayerPrefs.GetFloat("difficultyKey");
        else moodDecreaseValue = 0.3f;
        PropertyChanged += DissatisfactionValueListener;
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
        ChangeDissatisfactionValue();
        GameOver();
        BeerCountChange();
    }
    private void DissatisfactionValueListener(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("DissatisfactionValue"))
            this.WinStreak = 0;
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

            awaitingTables.Add(freeTables[randomTable]);
            freeTables.Remove(freeTables[randomTable]);
        }
        else
        {
            Debug.Log("  #SYSINFO: Nie wygenerowano zamówienia,wszystkie stoliki zajęte!");
        }
    }

    public void ControlOrders()
    {
        for (int i = awaitingTables.Count - 1; i >= 0; i--)
        {
            Table x = awaitingTables[i];
            var shouldBeFree = x.ControlOrder(time, moodDecreaseValue);
            if (shouldBeFree == true)
            {
                x.Mood = 20;
                Debug.Log("  #SYSINFO: Zwolniono stolik");
                // +++++++++++++ ADD TO WINSTREAK
                WinStreak += 1;
                // ++++++++++++++++++++++++++++++
                awaitingTables.RemoveAt(i);
                freeTables.Add(x);
            }
        }
    }

    private void ChangeDissatisfactionValue()
    {
        int i = 0;
        int threshold = 4;
        foreach (Table t in awaitingTables)
        {
            if (t.Mood < threshold) i++;
        }
        if (i != 0)
            DissatisfactionValue += Time.deltaTime * 5 * i;

    }
    private void BeerCountChange()
    {
        BeerCount = player.GetBeersOnPlateQuantity() + "";
    }
    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
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


