using System.Collections;
using Assets.PGKScripts.Enums;
using Game.Initialization;
using TMPro;
using UnityEngine;

public class BrawlController : MonoBehaviour
{
    public int Punches = 4;
    public float PunchDelay = 3.0f;
    [SerializeField]
    private LevelLoadEvent OnLevelLoad;

    [SerializeField]
    private UIMain UIMainObject;

    [SerializeField]
    private MainScript Main;

    [SerializeField]
    private Canvas MenuCanvas;

    [SerializeField]
    private Canvas MainCanvas;

    [SerializeField]
    private Canvas LoseCanvas;

    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private AudioSource Punch;

    [SerializeField]
    private ColorAnimation Fade;

    [SerializeField]
    private TextMeshProUGUI Insult;

    private Brawl Brawl;

    private void Start()
    {
        OnLevelLoad.LevelLoaded.AddListener(OnLevelLoaded);
        Main.GameStatusChanged.AddListener(GameStateChanged);
        Brawl = FindObjectOfType<Brawl>();
    }

    private void OnLevelLoaded(GameLevel level, LevelScene levelScene)
    {
        if(Brawl == null)
            Brawl = FindObjectOfType<Brawl>();

    }

    private void GameStateChanged(GameState old, GameState current)
    {
        if (Brawl == null)
        {
            Debug.LogWarning("No brawl!");
            return;
        }

        if (old == GameState.Playing && current == GameState.Failure)
        {
            StartCoroutine(RunBrawl());
        }
    }

    private IEnumerator RunBrawl()
    {
        Debug.Log("BRAWL TIME!");
        Time.timeScale = 1.0f;

        MenuCanvas.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(false);
        LoseCanvas.gameObject.SetActive(false);
        UIMainObject.MenuActivated = false;
        Insult.text = "";
        Insult.gameObject.SetActive(true);

        Fade.ApplySourceColor();

        yield return new WaitForSeconds(1.0f);
        UIMainObject.MenuActivated = false;

        Fade.Animate();
        DisableWanderers();
        SetNegativeMood();

        Brawl.Setup(Camera);
        Punch.PlayOneShot(Brawl.StartSound);

        yield return new WaitForSeconds(1.0f);
        UIMainObject.MenuActivated = false;
        /*for (int i = 0; i < Punches; i++)
        {
            Punch.Play();
            Insult.text = Brawl.Insults.Random();
            if(i != Punches - 1)
                yield return new WaitForSeconds(PunchDelay);
        }*/

        yield return new WaitForSeconds(4.0f);

        Fade.AnimateReverse();

        yield return new WaitForSeconds(Fade.AnimationTime);

        Time.timeScale = 0.0f;
        MenuCanvas.gameObject.SetActive(true);
        LoseCanvas.gameObject.SetActive(true);
        UIMainObject.MenuActivated = true;
    }

    private void SetNegativeMood()
    {
        var sources = FindObjectsOfType<OrderSource>();
        foreach (var source in sources)
        {
            source.Mood = 0.0f;
        }

        var UIS = FindObjectsOfType<OrderSourceUI>();
        foreach (var ui in UIS)
        {
            ui.gameObject.SetActive(false);
        }
    }

    private void DisableWanderers()
    {
        WaypointWandering[] wanderers = FindObjectsOfType<WaypointWandering>();
        foreach (WaypointWandering wanderer in wanderers)
            wanderer.gameObject.SetActive(false);
    }
}
