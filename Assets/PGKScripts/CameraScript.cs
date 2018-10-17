using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform player;

    public float distanceX = 0;
    public float distanceY = 7;
    public float distanceZ = 6;

    private float prevRotX = 45;
    public float rotationX = 45;
    private float prevRotY = 180;
    public float rotationY = 180;
    private float prevRotZ = 0;
    public float rotationZ = 0;

    public float smoothTime = 0f;
    public Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        transform.Rotate(rotationX, rotationY, rotationZ);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (prevRotY != rotationY || prevRotX != rotationX || prevRotZ !=rotationZ)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = rotationX;
            rotationVector.y = rotationY;
            rotationVector.z = rotationZ;
            transform.rotation = Quaternion.Euler(rotationVector);
            prevRotZ = rotationZ;
            prevRotX = rotationX;
            prevRotY = rotationY;
        }
    }
    private void FixedUpdate()
    {
        //transform.position = player.position + new Vector3(distanceX, distanceY, distanceZ);
        transform.position = Vector3.SmoothDamp(player.position, player.position + 
            new Vector3(distanceX, distanceY, distanceZ), ref velocity, smoothTime);

    }
}
