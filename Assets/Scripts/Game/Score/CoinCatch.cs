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
            Vector3 screenPoint = target.transform.position + new Vector3(0, 0, 5);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
            GameObject img = Instantiate(coinObj, startPosition, rotation);
          //  img.transform.SetParent(parent.transform);
            img.transform.DOMove(worldPos, coinUpTime).OnComplete( () => Destroy(img) );
            //img.transform.DORotate(Vector3.right * 50 * Time.deltaTime, 50);
            img.transform.DOShakeRotation(2*coinUpTime);
            img.transform.DOShakeScale(2 * coinUpTime);
            yield return new WaitForSeconds(0.4f);
        }
        yield return null;
    }


    // Update is called once per frame
    void Update ()
    {
            
	}
}
