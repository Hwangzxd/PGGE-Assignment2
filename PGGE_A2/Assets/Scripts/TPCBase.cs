using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;

        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        public void RepositionCamera()
        {
            //-------------------------------------------------------------------
            // Implement here.
            //-------------------------------------------------------------------
            //-------------------------------------------------------------------
            // Hints:
            //-------------------------------------------------------------------
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point
            //-------------------------------------------------------------------

            // Get the direction vector from the camera to the player
            Vector3 directionToPlayer = mPlayerTransform.position - mCameraTransform.position;

            // Create a ray from the camera position towards the player
            Ray ray = new Ray(mCameraTransform.position, directionToPlayer);

            // Variable to store raycast hit
            RaycastHit hit;

            // Get the CharacterController component attached to the player
            CharacterController characterController = mPlayerTransform.GetComponent<CharacterController>();

            // Check if the characterController is not null and if there is a collision between the camera and the player
            if (characterController != null && Physics.Raycast(ray, out hit, directionToPlayer.magnitude - 1f))
            {
                // Calculate the new position for the camera based on the collision point and
                // adding the character's height from the character controller to the y-coordinate
                Vector3 newPosition = new Vector3(hit.point.x, mPlayerTransform.position.y + characterController.height, hit.point.z);
                // Shift the camera's position to the new calculated position
                mCameraTransform.position = newPosition;
            }

        }

        public abstract void Update();
    }
}
