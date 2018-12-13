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
    public class PerkTuple
    {
        public PerkUI UI { get; set; }
        public Perk Perk { get; set; }
        public bool Availible
        {
            get
            {
                return UI.Availible && Perk.Availible && !Perk.Active;
            }
            set
            {
                if(value == false)
                {
                    UI.Availible = false;
                    Perk.Availible = false;
                }
                else
                {
                    if(!Perk.Active)
                    {
                        UI.Availible = true;
                        Perk.Availible = true;
                        UI.Show("");
                    }
                }
            }
        }
        public bool Active
        {
            get
            {
                return UI.PerkStarted && Perk.Active;
            }
            set
            {
                if(value == true )
                {
                    if(this.Availible)
                    {
                        UI.PerkStarted = true;
                        Perk.Active = true;
                        this.Availible = false;
                    }
                }
                else
                {
                    UI.PerkStarted = false;
                    Perk.Active = false;
                }
            }
        }
        public PerkTuple(PerkUI perkui, Perk perk) { this.Perk = perk; this.UI = perkui; }
        public void Disable()
        {
            UI.Disable();
            Perk.Active = false;
            Perk.Availible = false;
        }
    }


    [SerializeField]
    private MonoWinStreakSource winStreakSource;
    public ThirdPersonCharacter character;
    public PlayerPlate plate;
    //PERKS_LIST
    //Dictionary<IPerk, IPerkUi> perksUiBind = new Dictionary<IPerk, IPerkUi>();
    Dictionary<string, PerkTuple> perksUiBind = new Dictionary<string, PerkTuple>();

    public PerkUI speedPerkUI;
    public PerkUI holdPerkUI;
    public PerkUI noDropPerkUI;

    // SPEED PERK
    public static int speedPerkActivateMinimum = 2;
    public float speedMultiplier = 1.5f;
    public string SpeedPerkName { get { return "SpeedPerk"; } }
    // HOLD PERK
    public int holdMultiplier = 2;
    public int holdPerkActivateMinimum = 5;
    public string HoldPerkName { get { return "HoldPerk"; } }

    //
    // NO LOSE PERK
    public static int noLoseActivateMinimum = 8;
    public string NoDropPerkName { get { return "NoDropPerk"; } }
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
        holdPerk.Name = HoldPerkName;
        holdPerk.Quantity = 30;
        perksUiBind.Add(holdPerk.Name, new PerkTuple(holdPerkUI, holdPerk));

        playerStandardSpeed = character.getm_MoveSpeedMultiplier();
        var speedPerk = new Perk(
            new SpeedModif(character),
            speedPerkActivateMinimum);
        speedPerk.Name = SpeedPerkName;
        speedPerk.Quantity = 15;
        perksUiBind.Add(speedPerk.Name, new PerkTuple(speedPerkUI, speedPerk));

        var noDropPerk = new Perk(
            new NoLoseModif(player),
            noLoseActivateMinimum);
        noDropPerk.Name = NoDropPerkName;
        noDropPerk.Quantity = 20;
        perksUiBind.Add(noDropPerk.Name, new PerkTuple(noDropPerkUI, noDropPerk));
    }

    private void WinStreakChanged(int oldWs, int newWs)
    {
        if(newWs == 0)
        {
            foreach(var kvp in perksUiBind)
            {
                kvp.Value.Availible = false;
            }
        }

        foreach(var kvp in perksUiBind)
        {
            if(!kvp.Value.Active)
            {
                if(newWs > kvp.Value.Perk.MinimumToActivate)
                {
                    kvp.Value.Availible = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryActivateSpeed()
    {
        PerkTuple tuple = perksUiBind[SpeedPerkName];
        StartCoroutine(PerkCouroutine(tuple, playerStandardSpeed * speedMultiplier, playerStandardSpeed));
    }
    public void TryActivateHold()
    {
        PerkTuple tuple = perksUiBind[HoldPerkName];
        StartCoroutine(PerkCouroutine(tuple, playerStandardHold * holdMultiplier, playerStandardHold));
    }
    public void TryActivateInv()
    {
        PerkTuple tuple = perksUiBind[NoDropPerkName];
        StartCoroutine(PerkCouroutine(tuple, false, true));
    }
    public IEnumerator PerkCouroutine(PerkTuple perkTuple, object start, object end)
    {
        if (perkTuple.Availible)
        {
            perkTuple.Active = true;
            winStreakSource.WinStreak -= perkTuple.Perk.MinimumToActivate;
            perkTuple.Perk.Invoke(start);
            StartCoroutine(Countdown(perkTuple));
            while (perkTuple.Active)
            {
                yield return new WaitForSeconds(0.1f);
            }
            perkTuple.Perk.Invoke(end);
            if (winStreakSource.WinStreak >= perkTuple.Perk.MinimumToActivate)
                perkTuple.Availible = true;
        }
    }

    private IEnumerator Countdown(PerkTuple tuple)
    {
        for(int i = tuple.Perk.Quantity; i >= 0; i--)
        {
            tuple.UI.Show(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(0.1f);
        tuple.Active = false;
        tuple.Disable();
    }
}