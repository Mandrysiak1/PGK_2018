using Assets.PGKScripts.Interfaces;
using Assets.PGKScripts.Perks.UI;
using Assets.PGKScripts.Perks.WinStreak;
using Assets.PGKScripts.Perks.WinStreak.ModifWrappers;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class WinStreak : MonoBehaviour {

    public ThirdPersonCharacter character;
    public IWinStreakSource winStreakSource;
    // SPEED PERK
    private static int speedPerkActivateMinimum = 2;
    IPerk<float> speedPerk;
    public IPerksUi<string, Color> speedPerkUi;
    public float speedMultiplier = 1.5f;
    bool speedPerkAvailible = false;
    // HOLD PERK
    private static int holdPerkActivateMinimum = 5;
    IPerk<int> holdPerk;
    public IPerksUi<string, Color> holdPerkUi;
    public int holdNewValue = 10;
    bool holdPerkAvailible = false;
    //
    public int perkMaxTime = 10;
    private float playerStandardSpeed = 0f;
    private int playerStandardHold = 5;
    private int initialWinStreak = 0;
    private bool duringCountdown = false;
	// Use this for initialization
	void Start () {
        speedPerkUi = this.gameObject.GetComponent<SpeedPerkWinStreakUI>();
        holdPerkUi = this.gameObject.GetComponent<HoldPerkWinStreakUi>();
        winStreakSource = FindObjectOfType<MainScript>();
        winStreakSource.PropertyChanged += WinStreakChanged;

        var player = MainScript.player;
        playerStandardHold = player.MaxBeers;
        holdPerk = new HoldPerk(
            new HoldModif(player));
        playerStandardSpeed = character.getm_MoveSpeedMultiplier();
        speedPerk = new SpeedPerk(
            new SpeedModif(character));
    }
    private void DisablePerk(IPerksUi<string, Color> perkUi, out bool availibility)
    {
        availibility = false;
        perkUi.Disable();
    }
    private IEnumerator SpeedPerkRoutine()
    {
        speedPerk.Invoke(playerStandardSpeed * speedMultiplier);
        StartCoroutine(CountDown(10, speedPerkUi));
        while (duringCountdown)
            yield return new WaitForSeconds(0.1f);
        speedPerk.Invoke(1 / speedMultiplier);
        DisablePerk(holdPerkUi, out holdPerkAvailible);
        DisablePerk(speedPerkUi, out speedPerkAvailible);
    }
    private IEnumerator HoldPerkRoutine()
    {
        holdPerk.Invoke(holdNewValue);
        StartCoroutine(CountDown(30, holdPerkUi));
        while (duringCountdown)
            yield return new WaitForSeconds(0.1f);
        holdPerk.Invoke(playerStandardHold);
        DisablePerk(holdPerkUi, out holdPerkAvailible);
        DisablePerk(speedPerkUi, out speedPerkAvailible);
    }

    private void WinStreakChanged(object sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName.Equals("WinStreak"))
        {
            if (winStreakSource.WinStreak >= speedPerkActivateMinimum + initialWinStreak)
            {
                if(!speedPerkAvailible)
                    speedPerkUi.Show("1", new Color(0, 255, 0));
                speedPerkAvailible = true;
                Debug.Log("###### WINSTREAK ##### Speed Multiplier Active");
            }
            if (winStreakSource.WinStreak >= holdPerkActivateMinimum + initialWinStreak)
            {
                if(!holdPerkAvailible)
                    holdPerkUi.Show("2", new Color(0, 255, 0));
                holdPerkAvailible = true;
                Debug.Log("###### WINSTREAK ##### Max Hold Add Active");

            }
            if (winStreakSource.WinStreak.Equals(0))
            {
                speedPerkAvailible = false;
                holdPerkAvailible = false;
                holdPerkUi.Disable();
                speedPerkUi.Disable();
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if (speedPerkAvailible)
        {
            if (Input.GetKey("1"))
            {
                StartCoroutine(SpeedPerkRoutine());
            }
        }
        if(holdPerkAvailible)
        {
            if (Input.GetKey("2"))
            {
                StartCoroutine(HoldPerkRoutine());
            }
        }
    }
    private IEnumerator CountDown(int value, IPerksUi<string, Color> perkUi)
    {
        duringCountdown = true;
        for (int i = value; i >= 0; i--)
        {
            perkUi.Show(i.ToString(), new Color(255, 0, 0));
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(0.1f);
        duringCountdown = false;
    }
}
