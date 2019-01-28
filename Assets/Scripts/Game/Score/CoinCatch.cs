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
            Vector3 screenPoint = target.transform.position ;
         //   Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
            GameObject img = Instantiate(coinObj, startPosition, rotation);
            //Vector3 posoffset = new Vector3(-5f, 0, 0);
            img.transform.DOMove(target.transform.position, coinUpTime).OnComplete( () => Destroy(img) );
            img.transform.DOShakeRotation(2*coinUpTime);
            img.transform.DOScale(30f, coinUpTime);
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;
    }


    // Update is called once per frame
    void Update ()
    {
            
	}
}
