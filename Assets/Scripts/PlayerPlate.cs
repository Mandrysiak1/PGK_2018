using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPlate : MonoBehaviour
{
    /// <summary>
    /// <para>current beer amount</para>
    /// <para>previous beer amount</para>
    /// </summary>
    [Serializable]
    public class BeerCountEvent : UnityEvent<int, int> {}
    public BeerCountEvent OnBeerCountChanged;

    public int StartingBeers = 0;

    public int Beers
    {
        get { return _Beers; }
        set
        {
            int old = _Beers;
            if (value != old)
            {
                _Beers = value;
                OnBeerCountChanged.Invoke(_Beers, old);
            }
        }
    }
    private int _Beers;

    private void Start()
    {
        Beers = StartingBeers;
    }
}
