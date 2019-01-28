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

public class MainScript : MonoBehaviour
{
    public class GameStatusEvent : UnityEvent<GameState, GameState> { }
    public GameStatusEvent GameStatusChanged { get; set; }
    public bool tutorial = false;

    [SerializeField]
    private GameObject Gui;

    [SerializeField]
    private EndMenu EndMenu;

    [SerializeField]
    private BrawlController Brawl;

    [SerializeField]
    private ScoreSystem Score;

    [SerializeField]
    private PauseController PauseController;

    private GameState gameState = GameState.Playing;
    public GameState CurrentGameState
    {
        get
        {
            return gameState;
        }
        set
        {
            var temp = this.gameState;
            this.gameState = value;
            GameStatusChanged.Invoke(temp, value);
        }
    }

    public int BeersHandedOut
    {
        get
        {
            return Player.BeersHandedOut;
        }
    }

    [SerializeField]
    private Player Player;
    [SerializeField]
    private ThirdPersonCharacter PlayerController;

    public MainScript()
    {
        if (GameStatusChanged == null)
            GameStatusChanged = new GameStatusEvent();
    }

    private void Awake()
    {
    }

    private void OnDestroy()
    {
        Time.timeScale = 1.0f;
    }

    public void Start()
    {
        UpgradeClass.preGameTip = UpgradeClass.Tip;
        UpgradeClass.exited = false;
    }

    internal void ResetBeersHandedOut()
    {
        Player.ResetBeersHandedOut();
    }

    public Player GetPlayer()
    {
        return Player;
    }

    public void GameOver(GameState state = GameState.Failure)
    {
        if (state == CurrentGameState)
            return;

        if (state == GameState.Success)
            Time.timeScale = 0.0f;
        PlayerController.gameObject.SetActive(false);
        CurrentGameState = state;
        Gui.SetActive(false);
        Player.ResetPlate();

        if (state == GameState.Failure)
        {
            PauseController.enabled = false;
            Brawl.RunBrawl(DefeatMenu);
        }
        else if(state == GameState.Success)
        {
            PauseController.enabled = false;
            WinMenu();
        }
    }

    private void DefeatMenu()
    {
        EndMenu.Show(false, Score.Score);
    }

    private void WinMenu()
    {
        EndMenu.Show(true, Score.Score);
    }
}


