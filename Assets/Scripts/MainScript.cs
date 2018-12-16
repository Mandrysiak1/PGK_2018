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
    #region EventClasses
    //event classes
    public class GameStatusEvent : UnityEvent<GameState, GameState> { }
    public class DissatisfactionEvent : UnityEvent<float, float> { }


    //end of event classes
    #endregion
    public GameStatusEvent GameStatusChanged { get; set; }
    public bool tutorial = false;

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
        if (state == GameState.Success)
            Time.timeScale = 0.0f;
        PlayerController.gameObject.SetActive(false);
        CurrentGameState = state;
        Player.ResetPlate();
    }
}


