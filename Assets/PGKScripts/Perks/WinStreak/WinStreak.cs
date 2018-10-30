using Assets.PGKScripts.Interfaces;
using Assets.PGKScripts.Perks.UI;
using System.ComponentModel;
using UnityEngine;

public class WinStreak : MonoBehaviour {

    public IWinStreakSource winStreakSource;
    // SPEED PERK
    private static int speedPerkMinimum = 2;
    IPerk<float> speedPerk;
    public IPerksUi<string> speedPerkUi;
    public float speedMultiplier = 1.5f;
    // HOLD PERK
    private static int holdPerkMinimum = 5;
    IPerk<int> holdPerk;
    public IPerksUi<string> holdPerkUi;
    public int holdNewValue = 10;

	// Use this for initialization
	void Start () {
        speedPerkUi = this.gameObject.GetComponent<SpeedPerkWinStreakUI>();
        holdPerkUi = this.gameObject.GetComponent<HoldPerkWinStreakUi>();
        winStreakSource = FindObjectOfType<MainScript>();
        winStreakSource.PropertyChanged += WinStreakChanged;
	}

    private void WinStreakChanged(object sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName.Equals("WinStreak"))
        {
            if (winStreakSource.WinStreak >= speedPerkMinimum)
            {
                speedPerkUi.Show(speedMultiplier.ToString());
                Debug.Log("###### WINSTREAK ##### Speed Multiplier Active");
            }
            if (winStreakSource.WinStreak >= holdPerkMinimum)
            {
                holdPerkUi.Show(holdNewValue.ToString());
                Debug.Log("###### WINSTREAK ##### Max Hold Add Active");

            }
            if (winStreakSource.Equals(0))
            {
                holdPerkUi.Disable();
                speedPerkUi.Disable();
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
