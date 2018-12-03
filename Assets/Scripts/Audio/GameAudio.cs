using UnityEngine;

public class GameAudio : MonoBehaviour {
    public PlayerController playerController;
    public AudioSource collideAudio;
    public AudioSource beerDropAudio;
    public PlayerPlate playerPlate;
    public AudioSource beerOpenSound;
   // private MainScript mainScript;
	// Use this for initialization
	void Start () {
        playerController.OnPlayerCollide.AddListener(PlayerCollided);
        if (playerPlate == null)
            playerPlate = FindObjectOfType<PlayerPlate>();
        playerPlate.OnItemAmountChanged.AddListener(BeerAmountChanged);

    }

    private void BeerAmountChanged(OrderItem arg0, int arg1, int arg2)
    {
        if(arg2 > arg1)
        {
            beerDropAudio.Play();
        }
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
