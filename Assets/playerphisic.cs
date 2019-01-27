using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerphisic : MonoBehaviour {





    GameContext context;
    private bool hasPlayer = false;
    Vector3 oldPosition;
    Vector3 newPosition;
    // Use this for initialization
   void Start () {
        GameContext.FindIfNull(ref context);
        oldPosition = transform.position;
	}

    // Update is called once per frame
    void LateUpdate()
    {
        context.Player.gameObject.transform.position = transform.position;
        // Debug.Log(transform.position + "HLAOSFD");
        newPosition = transform.position;
        if (true)
        {

            Vector3 result = (oldPosition - newPosition);
            //.Log(result);
            if (context == null)
            {
                //  Debug.LogWarning("NULL");
            }
            else
            {

            }
            // Debug.Log(context.Player.transform.position + "gracz");


            oldPosition = newPosition;

        }

    }
 
}
