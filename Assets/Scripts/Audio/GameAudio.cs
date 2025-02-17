﻿using System;
using UnityEngine;

public class GameAudio : MonoBehaviour {
    public PlayerController playerController;
    public PlayerCollisionHandler collisionHandler;
    public OrderController orderController;
    public AudioSource collideAudio;
    public AudioSource beerDropAudio;
    public PlayerPlate playerPlate;
    public AudioSource beerOpenSound;
    public AudioSource putDownSound;
    public AudioSource shooshSound;
    public AudioSource dingWinStreakSound;
    public AudioSource nopeSound;
    public WinStreak winStreakScript;
    // private MainScript mainScript;
    // Use this for initialization
    void Start () {
        playerController.OnPlayerCollide.AddListener(PlayerCollided);
        if (playerPlate == null)
            playerPlate = FindObjectOfType<PlayerPlate>();
        playerPlate.OnItemAmountChanged.AddListener(BeerAmountChanged);
        collisionHandler = FindObjectOfType<PlayerCollisionHandler>();
        orderController = FindObjectOfType<OrderController>();
        orderController.OrderUpdated.AddListener(OrderDecreased);
        orderController.UpdateNotPossible.AddListener(Unable);
        collisionHandler.OnCollisionC += OnCustomerCollision;
        collisionHandler.OnCollisionI += OnItemCollision;
        winStreakScript.OnPerkActivated.AddListener(OnPerkActivated);
        winStreakScript.OnPerkAvailible.AddListener(OnPerkAvailible);
    }

    private void OnPerkAvailible(string arg0)
    {
        dingWinStreakSound.Play();
    }

    private void OnPerkActivated(string arg0)
    {
        shooshSound.Play();
    }

    private void Unable()
    {
        nopeSound.Play();
    }

    private void OrderDecreased(OrderSource source, Order order, float previousProgress)
    {
        putDownSound.Play();
    }

    private void OnItemCollision()
    {
        beerDropAudio.Play();
    }

    private void OnCustomerCollision()
    {
        beerDropAudio.Play();
    }

    private void BeerAmountChanged(OrderItem arg0, int arg1, int arg2)
    {
        if(arg2 < arg1)
        {
            beerOpenSound.Play();
        }
    }

    private void PlayerCollided()
    {
        collideAudio.Play();

    }

    // Update is called once per frame
    void Update () {
	}
}
