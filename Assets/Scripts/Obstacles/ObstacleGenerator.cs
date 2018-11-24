
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public float MinimalDelayBetweenEvents;
    public float State5ChanceIndex, State4ChanceIndex, State3ChanceIndex, State2ChanceIndex, State1ChanceIndex;
    private float ChanceOfEvent = 0;
    private float NormalizedChanceOfEvent;

    [SerializeField]
    private GameContext Context;

    public delegate void GenerateObstacle();
    public event GenerateObstacle OnGenerateObstacle;


    void Start()
    {
        GameContext.FindIfNull(ref Context);
        InvokeRepeating("CalculateChanceOfEvent", 1f, 1f);
    }


    private void CalculateChanceOfEvent()
    {

        ChanceOfEvent = 0;

        foreach (var x in Context.Orders.AwaitingSources)
        {
            if (x.Mood >= 16)
            {
                ChanceOfEvent += State1ChanceIndex;
            }
            if (x.Mood >= 12 && x.Mood < 16)
            {
                ChanceOfEvent += State2ChanceIndex;
            }
            if (x.Mood >= 8 && x.Mood < 12)
            {
                ChanceOfEvent += State3ChanceIndex;
            }
            if (x.Mood >= 4 && x.Mood < 8)
            {
                ChanceOfEvent += State4ChanceIndex;
            }
            if (x.Mood < 4)
            {
                ChanceOfEvent += State5ChanceIndex;

            }
            else
            {
                ; //DO NOTHING
            }
        }
        NormalizeChanceOfEvent();

        if(NormalizedChanceOfEvent > 0)
        {
            CheckIfEventHappens();
        }

    }

    private void NormalizeChanceOfEvent()
    {
        int tablesCount = Context.Orders.AwaitingSources.Count();
        NormalizedChanceOfEvent = (ChanceOfEvent / (40 * (float)tablesCount)) * 100;

    }

    private void CheckIfEventHappens()
    {

        if (Random.Range(0, 101) <= NormalizedChanceOfEvent)
        {
            if (OnGenerateObstacle != null)
            {

                OnGenerateObstacle();
            }

            CancelInvoke("CalculateChanceOfEvent");

            InvokeRepeating("CalculateChanceOfEvent", MinimalDelayBetweenEvents, 1f);

        }

    }


}