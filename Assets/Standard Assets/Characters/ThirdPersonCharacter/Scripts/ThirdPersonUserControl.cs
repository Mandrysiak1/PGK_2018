using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        [SerializeField]
        private float Acceleration = 2.0f;
        [SerializeField]
        private float SlowWalkMultiplier = 0.4f;
        [SerializeField]
        private Camera Camera;

        private ThirdPersonCharacter Character;

        private Vector3 CamForward;             // The current forward direction of the camera
        private Vector3 MoveVector;
        private bool Jump;
        
        private void Start()
        {
            if (Camera == null)
            {
                Debug.LogWarning(
                    "Warning: no camera assigned.", gameObject);
            }

            // get the third person character ( this should never be null due to require component )
            Character = GetComponent<ThirdPersonCharacter>();
        }

        private void FixedUpdate()
        {
            // read inputs
            float horizontalAcceleration = CrossPlatformInputManager.GetAxis("Horizontal") * Acceleration;
            float verticalAcceleration = CrossPlatformInputManager.GetAxis("Vertical") * Acceleration;
            Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            bool crouch = CrossPlatformInputManager.GetButton("Crouch");

            if (Camera != null)
            {
                // calculate camera relative direction to move:
                CamForward = Vector3.Scale(Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
                MoveVector = verticalAcceleration * CamForward + horizontalAcceleration * Camera.transform.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                MoveVector = verticalAcceleration*Vector3.forward + horizontalAcceleration*Vector3.right;
            }

	        if (CrossPlatformInputManager.GetButton("SlowWalk"))
                MoveVector *= SlowWalkMultiplier;

            Character.Move(MoveVector, crouch, Jump);

            Jump = false;
        }
    }
}
