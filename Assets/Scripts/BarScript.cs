using System;
using Assets.PGKScripts.Enums;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;

    private bool hasPlayer = false;
    private bool isInGame = true;
    public OrderItem orderToPick;
    public MainScript mainScript;

    void Start()
    {
        GameContext.FindIfNull(ref Context);
        if (mainScript == null)
            mainScript = FindObjectOfType<MainScript>();
        mainScript.GameStatusChanged.AddListener(GameStatusChanged);
    }

    private void GameStatusChanged(GameState arg0, GameState arg1)
    {
        if (arg1 != GameState.Playing)
            isInGame = false;
        else
            isInGame = true;
    }

    void Update()
    {
        if(Input.GetButtonDown("ReturnItemOnBar") && hasPlayer && orderToPick != null && isInGame)
        {
            Context.Player.RemoveBeer(orderToPick);
        }
        if (Input.GetButtonDown("Submit") && hasPlayer && orderToPick != null && isInGame)
        {
            Context.Player.AddOrderItemOnPlate(orderToPick);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }
}