using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaNotification : Notification {

    public string onSuccess = "";
    public string onFail = "";

    protected new void Start()
    {
        base.Start();
        var x = FindObjectOfType<AddSantaBonus>();
        x.OnSantaInfo.AddListener(OnActivate);
        x.SantaFailure.AddListener(onSantaFailuer);

    }

    private void onSantaFailuer()
    {
        Show(string.Format(onFail));
    }

    private void OnActivate()
    {
        Show(string.Format(onSuccess));
    }
}
