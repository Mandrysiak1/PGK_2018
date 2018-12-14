using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CoinCatch : MonoBehaviour {
    
    public GameObject target;
    public GameObject startPoint;
    public GameObject parent;
    public Quaternion rotation;
    public float coinUpTime = 50f;
    public GameObject coinObj;

	// Use this for initialization
	void Start()
    {
        rotation = Quaternion.Euler(0, 0, 0);
        UpgradeClass.OnTipChanged.AddListener(TipChanged);
    }

    private void TipChanged(int newTip, int oldTip)
    {
        if(newTip > oldTip)
        {
            StartCoroutine(CoinUp(newTip - oldTip));
        }
    }

    public IEnumerator CoinUp(int times)
    {
        while(times-- > 0)
        {
            Vector3 startPosition = startPoint.transform.position;
            Vector3 targetPos = target.transform.position;
            GameObject img = Instantiate(coinObj, startPosition, rotation);
            img.transform.SetParent(parent.transform);
            img.transform.DOMove(targetPos, coinUpTime).OnComplete( () => Destroy(img) );
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;
    }


    // Update is called once per frame
    void Update ()
    {
            
	}
}
