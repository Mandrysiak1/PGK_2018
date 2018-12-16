using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OrderSource : MonoBehaviour
{
    public List<OrderRequest> PossibleRequests = new List<OrderRequest>();
    [SerializeField]
    private float _Mood = 1.0f;

    public float MoodDecreaseRate = 0.02f;

    [SerializeField]
    private OrderSourceActivationCondition ActivationCondition;
    [SerializeField]
    private OrderCondition[] OrderCondition;
    [Tooltip("If false only one condition must be met")]
    public bool AllOrderConditionsMustBeMet = true;
    [SerializeField]
    private GameObject SittingGuests;

    [Serializable]
    public class MoodEvent : UnityEvent<float> {}
    [Serializable]
    public class OrderEvent : UnityEvent<OrderSource, Order> {}

    public UnityEvent OnActivate;
    public MoodEvent OnMoodChanged;
    public OrderEvent OnOrderChanged;

    public Order CurrentOrder;

    public float Mood
    {
        get { return _Mood; }
        set
        {
            _Mood = value;
            OnMoodChanged.Invoke(value);
        }
    }

    public bool IsActive { get;  set; }


    private void Awake()
    {
        IsActive = ActivationCondition == null;
    }

    public bool CanIssueOrder(OrderController orders)
    {
        if (OrderCondition.Length == 0)
            return true;

        if (AllOrderConditionsMustBeMet)
        {
            return OrderCondition.All(condition => condition.CanIssueOrder(this, orders));
        }
        else
        {
            return OrderCondition.Any(condition => condition.CanIssueOrder(this, orders));
        }
    }

    private void OnValidate()
    {
        if (OrderCondition == null)
        {
            Debug.LogWarning("OrderCondition is not set! This source will issue an order instantously the moment it is free!");
        }
    }

    private void Update()
    {
        if (!IsActive)
        {
            if (ActivationCondition.IsMeet())
            {
                IsActive = true;
                if(SittingGuests != null)
                    SittingGuests.SetActive(true);
                OnActivate.Invoke();
            }
        }

        if (OrderCondition != null)
        {
            foreach (OrderCondition condition in OrderCondition)
            {
                condition.Tick(CurrentOrder, Time.deltaTime);
            }
        }
    }

    public void Refresh()
    {
        OnOrderChanged.Invoke(this, CurrentOrder);
    }
}
