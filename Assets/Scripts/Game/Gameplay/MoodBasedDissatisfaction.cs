using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MoodBasedDissatisfaction : MonoBehaviour
{
    public float Treshold = 0.25f;
    public float IncreaseRate = 5.0f;

    /// <summary>
    /// <para>current</para>
    /// <para>old</para>
    /// </summary>
    [Serializable]
    public class DissatisfactionEvent : UnityEvent<float, float> { }
    public DissatisfactionEvent DissatisfactionChanged;

    public float Dissatisfaction
    {
        get { return _Dissatisfaction; }
        set
        {
            var old = _Dissatisfaction;
            if (old != value)
            {
                _Dissatisfaction = value;
                DissatisfactionChanged.Invoke(value, old);
                Slider.value = value;
            }
        }
    }

    [SerializeField]
    private GameContext Context;
    [SerializeField]
    private Slider Slider;

    private float _Dissatisfaction;

    private void Awake()
    {
        Dissatisfaction = 0.0f;
    }

    private void Update()
    {
        int i = 0;
        foreach (OrderSource source in Context.Orders.AwaitingSources)
        {
            if (source.Mood < Treshold)
                i++;
        }

        if (i != 0)
        {
            Dissatisfaction += Time.deltaTime * IncreaseRate * i;
        }
    }
}
