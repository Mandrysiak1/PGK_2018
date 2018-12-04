using System;
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
   // private MainScript mainScript;
	// Use this for initialization
	void Start () {
        playerController.OnPlayerCollide.AddListener(PlayerCollided);
        if (playerPlate == null)
            playerPlate = FindObjectOfType<PlayerPlate>();
        playerPlate.OnItemAmountChanged.AddListener(BeerAmountChanged);
        collisionHandler = FindObjectOfType<PlayerCollisionHandler>();
        orderController = FindObjectOfType<OrderController>();
        orderController.DecreasedEvent.AddListener(OrderDecreased);
        collisionHandler.OnCollisionC += OnCustomerCollision;
        collisionHandler.OnCollisionI += OnItemCollision;
    }

    private void OrderDecreased()
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
