using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaNotification : Notification {

    public string Format = "chuj w duepe";

    protected new void Start()
    {
        base.Start();
        var x = FindObjectOfType<AddSantaBonus>();
        x.OnSantaInfo += OnActivate;
   
    }

    private void OnActivate()
    {
        Show(string.Format(Format));
    }
}
