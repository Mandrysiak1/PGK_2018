using Assets.PGKScripts.Interfaces;
using Assets.PGKScripts.Perks.WinStreak;
using Assets.PGKScripts.Perks.WinStreak.ModifWrappers;
using Assets.Scripts.Perks.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class WinStreak : MonoBehaviour
{

    public ThirdPersonCharacter character;
    public IWinStreakSource winStreakSource;
    //PERKS_LIST
    //List<IPerk> perksList = new List<IPerk>();
    Dictionary<IPerk, IPerkUi> perksUiBind = new Dictionary<IPerk, IPerkUi>();

    public PerkUI speedPerkUI;
    public PerkUI holdPerkUI;
    public PerkUI noDropPerkUI;

    // SPEED PERK
    public static int speedPerkActivateMinimum = 2;
    //IPerk<float> speedPerk;
    //public IPerksUi<string, Color> speedPerkUi;
    public float speedMultiplier = 1.5f;
    // HOLD PERK
    public static int holdPerkActivateMinimum = 5;
    //IPerk<int> holdPerk;
    //public IPerksUi<string, Color> holdPerkUi;
    public int holdNewValue = 10;
    //
    // NO LOSE PERK
    public static int noLoseActivateMinimum = 8;
    //IPerk<bool> noLosePerk;
    //public IPerksUi<string, Color> noLosePerkUi;
    //
    public int perkMaxTime = 10;
    private float playerStandardSpeed = 0f;
    private int playerStandardHold = 5;
    private int initialWinStreak = 0;
    private bool duringCountdown = false;
    // Use this for initialization
    void Start()
    {
        MainScript mainScript = FindObjectOfType<MainScript>();
        winStreakSource = mainScript;
        winStreakSource.WinStreakChanged.AddListener(WinStreakChanged);

        var player = mainScript.GetPlayer();
        playerStandardHold = player.MaxBeers;

        var holdPerk = new HoldPerk(
            new HoldModif(player));
        holdPerk.Name = "HoldPerk";
        perksUiBind.Add(holdPerk, holdPerkUI);

        playerStandardSpeed = character.getm_MoveSpeedMultiplier();
        var speedPerk = new SpeedPerk(
            new SpeedModif(character));
        speedPerk.Name = "SpeedPerk";
        perksUiBind.Add(speedPerk, speedPerkUI);

        var noDropPerk = new NoLosePerk(
            new NoLoseModif(player));
        noDropPerk.Name = "NoDropPerk";
        perksUiBind.Add(noDropPerk, noDropPerkUI);
    }
    private void DisablePerk(IPerk perk)
    {
        perk.Availible = false;
        perksUiBind[perk].Disable();
    }
    private IEnumerator PerkRoutine(IPerk perk, object original_val, object modified)
    {
        perk.Invoke(modified);
        StartCoroutine(CountDown(20, perksUiBind[perk]));
        foreach (var kv in perksUiBind)
        {
            if(perk.Name != kv.Key.Name && kv.Key.Availible)
                DisablePerk(kv.Key);
        }
        while (duringCountdown)
            yield return new WaitForSeconds(0.1f);
        perk.Invoke(original_val);
        DisablePerk(perk);
    }
    private void WinStreakChanged(int oldWs, int newWs)
    {
        foreach (var kv in perksUiBind)
        {
            if (newWs >= speedPerkActivateMinimum + initialWinStreak && kv.Key.Name == "SpeedPerk")
            {
                if (!kv.Key.Availible)
                    kv.Value.Show("0", new Color(0, 255, 0));
                kv.Key.Availible = true;
                Debug.Log("###### WINSTREAK ##### Speed Multiplier Active");
            }
            if (newWs >= holdPerkActivateMinimum + initialWinStreak && kv.Key.Name == "HoldPerk")
            {
                if (!kv.Key.Availible)
                    kv.Value.Show("0", new Color(0, 255, 0));
                kv.Key.Availible = true;
                Debug.Log("###### WINSTREAK ##### Max Hold Add Active");

            }
            if (newWs >= noLoseActivateMinimum + initialWinStreak && kv.Key.Name == "NoDropPerk")
            {
                if (!kv.Key.Availible)
                    kv.Value.Show("0", new Color(0, 255, 0));
                kv.Key.Availible = true;
                Debug.Log("###### WINSTREAK ##### No Lose Add Active");
            }
        }
        if (newWs == 0)
        {
            foreach (var kv in perksUiBind)
            {
                DisablePerk(kv.Key);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var kv in perksUiBind)
        {
            if (kv.Key.Availible == true)
            {
                if (kv.Key.Name == "SpeedPerk" && Input.GetButton("Perk_1"))
                {
                    StartCoroutine(PerkRoutine(kv.Key, playerStandardSpeed,
                        playerStandardSpeed * speedMultiplier));
                    initialWinStreak = winStreakSource.WinStreak;
                }
                if (kv.Key.Name == "HoldPerk" && Input.GetButton("Perk_2"))
                {
                    StartCoroutine(PerkRoutine(kv.Key, playerStandardHold, holdNewValue));
                    initialWinStreak = winStreakSource.WinStreak;
                }
                if (kv.Key.Name == "NoDropPerk" && Input.GetButton("Perk_3"))
                {
                    StartCoroutine(PerkRoutine(kv.Key, true, false));
                    initialWinStreak = winStreakSource.WinStreak;
                }
            }
        }
    }
    private IEnumerator CountDown(int value, IPerkUi perkUi)
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
