using UnityEngine;

public class ButtonBehaviour : MonoBehaviour {

    public WinStreak winStreakScript;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //SPEED PERK
        if ((Input.GetButtonDown("Perk_1") || Input.GetAxis("QTE_DPAD_H") < -0.5f))
        {
            this.ActivateSpeed();
        }
        //HOLD PERK
        if ((Input.GetButtonDown("Perk_2") || Input.GetAxis("QTE_DPAD_V") > 0.5f))
        {
            this.ActivateHold();
        }

        //INV PERK
        if ((Input.GetButtonDown("Perk_3") || Input.GetAxis("QTE_DPAD_H") > 0.5f))
        {
            this.ActivateInv();
        }
    }

    public void ActivateSpeed()
    {
        winStreakScript.TryActivateSpeed();
    }

    public void ActivateHold()
    {
        winStreakScript.TryActivateHold();
    }

    public void ActivateInv()
    {
        winStreakScript.TryActivateInv();
    }
}
