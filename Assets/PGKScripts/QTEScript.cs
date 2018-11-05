using Assets.PGKScripts;
using Assets.PGKScripts.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using QTE;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class QTEScript : MonoBehaviour
{
    [SerializeField]
    private QTEController QTE;

    void Start ()
    {
        if (QTE == null)
            QTE = FindObjectOfType<QTEController>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.CompareTag("Player") && !QTE.IsRunning)
        {
            QTE.TryRun();
        }
    }

}
