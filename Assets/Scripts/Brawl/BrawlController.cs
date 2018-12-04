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
    private MainScript Main;

    [SerializeField]
    private Canvas MenuCanvas;

    [SerializeField]
    private Canvas MainCanvas;

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

        if (old != current && current == GameState.Failure)
        {
            StartCoroutine(RunBrawl());
        }
    }

    private IEnumerator RunBrawl()
    {
        Time.timeScale = 1.0f;

        MenuCanvas.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(false);
        Insult.text = "";
        Insult.gameObject.SetActive(true);

        Fade.ApplySourceColor();

        yield return new WaitForSeconds(1.0f);

        Fade.Animate();
        DisableWanderers();
        SetNegativeMood();

        Brawl.Setup(Camera);
        Punch.PlayOneShot(Brawl.StartSound);

        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < Punches; i++)
        {
            Punch.Play();
            Insult.text = Brawl.Insults.Random();
            if(i != Punches - 1)
                yield return new WaitForSeconds(PunchDelay);
        }

        yield return new WaitForSeconds(1.0f);

        Fade.AnimateReverse();

        yield return new WaitForSeconds(Fade.AnimationTime);

        Time.timeScale = 0.0f;
        MenuCanvas.gameObject.SetActive(true);
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
