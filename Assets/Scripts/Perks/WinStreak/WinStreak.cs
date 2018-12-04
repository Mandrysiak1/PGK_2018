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
    [SerializeField]
    private MonoWinStreakSource winStreakSource;
    public ThirdPersonCharacter character;
    public PlayerPlate plate;
    //PERKS_LIST
    Dictionary<IPerk, IPerkUi> perksUiBind = new Dictionary<IPerk, IPerkUi>();

    public PerkUI speedPerkUI;
    public PerkUI holdPerkUI;
    public PerkUI noDropPerkUI;

    // SPEED PERK
    public static int speedPerkActivateMinimum = 2;
    public float speedMultiplier = 1.5f;
    // HOLD PERK
    public int holdMultiplier = 2;
    public int holdPerkActivateMinimum = 5;
    //
    // NO LOSE PERK
    public static int noLoseActivateMinimum = 8;
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
        OrderSource orderSource = FindObjectOfType<OrderSource>();
        winStreakSource.WinStreakChanged.AddListener(WinStreakChanged);

        var player = mainScript.GetPlayer();
        playerStandardHold = plate.maximumCapacityMultiplier;

        var holdPerk = new Perk(
            new HoldModif(plate),
            holdPerkActivateMinimum);
        holdPerk.Name = "HoldPerk";
        holdPerk.Quantity = 30;
        perksUiBind.Add(holdPerk, holdPerkUI);

        playerStandardSpeed = character.getm_MoveSpeedMultiplier();
        var speedPerk = new Perk(
            new SpeedModif(character),
            speedPerkActivateMinimum);
        speedPerk.Name = "SpeedPerk";
        speedPerk.Quantity = 15;
        perksUiBind.Add(speedPerk, speedPerkUI);

        var noDropPerk = new Perk(
            new NoLoseModif(player),
            noLoseActivateMinimum);
        noDropPerk.Name = "NoDropPerk";
        noDropPerk.Quantity = 20;
        perksUiBind.Add(noDropPerk, noDropPerkUI);
    }
    private void DisablePerk(IPerk perk)
    {
        perk.Availible = false;
        perksUiBind[perk].Disable();
        perk.Active = false;
    }
    private IEnumerator PerkRoutine(IPerk perk, object original_val, object modified)
    {
        perk.Invoke(modified);
        StartCoroutine(CountDown(perk.Quantity, perksUiBind[perk]));
        foreach (var kv in perksUiBind)
        {
            if (perk.Name != kv.Key.Name && kv.Key.Availible)
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
            //if(kv.Key.MinimumToActivate + initialWinStreak <= newWs)
            if(newWs >= kv.Key.MinimumToActivate)
            {
                if (!kv.Key.Availible && !kv.Key.Active)
                {
                    kv.Value.Show("", new Color(0, 255, 0));
                    kv.Key.Availible = true;
                    Debug.Log("###### WINSTREAK ##### " + kv.Key.Name + " availible.");
                }
            }
        }
        if (newWs == 0)
        {
            foreach (var kv in perksUiBind)
            {
                if(!kv.Key.Active)
                    DisablePerk(kv.Key);
                initialWinStreak = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var kv in perksUiBind)
        {
            if (kv.Key.Availible && !kv.Key.Active)
            {
                if (kv.Key.Name == "SpeedPerk" && Input.GetButtonDown("Perk_1"))
                {
                    kv.Key.Availible = false;
                    kv.Key.Active = true;
                    StartCoroutine(PerkRoutine(kv.Key, playerStandardSpeed,
                        playerStandardSpeed * speedMultiplier));
                    // initialWinStreak = winStreakSource.WinStreak;
                    winStreakSource.WinStreak -= kv.Key.MinimumToActivate;
                }
                if (kv.Key.Name == "HoldPerk" && Input.GetButtonDown("Perk_2"))
                {
                    kv.Key.Availible = false;
                    kv.Key.Active = true;
                    StartCoroutine(PerkRoutine(kv.Key, playerStandardHold, holdMultiplier));
                    //  initialWinStreak = winStreakSource.WinStreak;
                    winStreakSource.WinStreak -= kv.Key.MinimumToActivate;
                }
                if (kv.Key.Name == "NoDropPerk" && Input.GetButtonDown("Perk_3"))
                {
                    kv.Key.Availible = false;
                    kv.Key.Active = true;
                    StartCoroutine(PerkRoutine(kv.Key, true, false));
                    //  initialWinStreak = winStreakSource.WinStreak;
                    winStreakSource.WinStreak -= kv.Key.MinimumToActivate;
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