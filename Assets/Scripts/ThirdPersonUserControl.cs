using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        [SerializeField]
        private float Acceleration = 2.0f;
        [SerializeField]
        private float SlowWalkMultiplier = 0.4f;

        private ThirdPersonCharacter Character;

        private bool Jump;
        
        private void Start()
        {
            // get the third person character ( this should never be null due to require component )
            Character = GetComponent<ThirdPersonCharacter>();
        }

        private void FixedUpdate()
        {

            float joyX = Input.GetAxis("Horizontal");
            float joyY = Input.GetAxis("Vertical");

            // mapping square position from joystick potentiometers to circle
            Vector2 movementVector = new Vector2(
                joyX * Mathf.Sqrt(1 - joyY * joyY * 0.5f),
                joyY * Mathf.Sqrt(1 - joyX * joyX * 0.5f)
                );
            

            Jump = Input.GetButtonDown("Jump");
            bool crouch = Input.GetButton("Crouch");

            Vector3 move3dVector = movementVector.y * Vector3.forward + movementVector.x * Vector3.right;

	        if (Input.GetButton("SlowWalk"))
                move3dVector *= SlowWalkMultiplier;

            Character.Move(move3dVector, crouch, Jump);

            Jump = false;
        }
    }
}
