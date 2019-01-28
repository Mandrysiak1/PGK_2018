using System;
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
        Brawl = FindObjectOfType<Brawl>();
    }

    private void OnLevelLoaded(GameLevel level, LevelScene levelScene)
    {
        if(Brawl == null)
            Brawl = FindObjectOfType<Brawl>();

    }

    public void RunBrawl(Action onFinish)
    {
        StartCoroutine(_RunBrawl(onFinish));
    }

    private IEnumerator _RunBrawl(Action onFinish)
    {
        Debug.Log("BRAWL TIME!");
        Time.timeScale = 1.0f;

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

        yield return new WaitForSeconds(4.0f);

        Fade.SourceColor.a = 0.5f;
        Fade.AnimateReverse();

        yield return new WaitForSeconds(Fade.AnimationTime);

        Time.timeScale = 0.0f;
        onFinish();
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
