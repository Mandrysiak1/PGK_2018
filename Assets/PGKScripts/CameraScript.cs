using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;

    public float smoothSpeed = 0.3f;

    public Vector3 cameraOffset = new Vector3(0, 8, 8);
	public Vector3 rotationVector = new Vector3(45, 180, 0);
    private Vector3 prevRotation;
    public Vector3 currentVelocity;

    public float maxSpeed = 4.5f;

	// Use this for initialization
	void Start () {
		transform.Rotate(rotationVector.x, rotationVector.y, rotationVector.z);
        prevRotation = new Vector3(rotationVector.x, rotationVector.y, rotationVector.z);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 nextPosition = player.position + cameraOffset;

        if(!rotationVector.Equals(prevRotation))
        {
            transform.rotation = Quaternion.Euler(rotationVector);
            prevRotation.x = rotationVector.x;
            prevRotation.y = rotationVector.y;
            prevRotation.z = rotationVector.z;
        }
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, nextPosition,
            ref currentVelocity, smoothSpeed, maxSpeed);

        transform.position = smoothedPosition;
    }
    private void Update()
    {
    }
}
