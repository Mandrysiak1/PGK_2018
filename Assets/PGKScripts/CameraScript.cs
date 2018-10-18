using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;

    public float smoothSpeed = 10f;

    public Vector3 cameraOffset = new Vector3(0, 7, 6);

    public Vector3 currentVelocity;

    public float maxSpeed = 10;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 nextPosition = player.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, nextPosition, 
            ref currentVelocity, smoothSpeed, maxSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player);
    }
    private void Update()
    {
        //transform.position = player.position + new Vector3(distanceX, distanceY, distanceZ);
    }
}
