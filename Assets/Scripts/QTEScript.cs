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
    private PlayerCollisionHandler PlayerCollisionHandler;

    void Start ()
    {
        if (PlayerCollisionHandler == null)
            PlayerCollisionHandler = FindObjectOfType<PlayerCollisionHandler>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            PlayerCollisionHandler.ItemCollision(gameObject);
        }
    }

}
