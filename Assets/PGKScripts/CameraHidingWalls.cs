using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraHidingWalls : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float PlayerWidth = 0.3f;

    private List<CameraHideable> lastCollidingObjects = new List<CameraHideable>();

	void Update ()
    {
        GameObject gobj = this.gameObject;
        List<RaycastHit> hits = new List<RaycastHit>();
        Vector3 position = this.transform.position;
        Vector3 direction = Player.transform.position - position;
        
        // raycast position
        hits.AddRange(Physics.RaycastAll(position, direction));
        // raycast left
        hits.AddRange(Physics.RaycastAll(position, new Vector3(direction.x - PlayerWidth / 2f, direction.y, direction.z)));
        // raycast right
        hits.AddRange(Physics.RaycastAll(position, new Vector3(direction.x + PlayerWidth / 2f, direction.y, direction.z)));

        List<CameraHideable> currentCollidingObjects = new List<CameraHideable>();
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider != null)
            {
                Collider collider = hit.collider;

                CameraHideable cameraHideable = collider.gameObject.GetComponent<CameraHideable>();
                if (cameraHideable != null)
                {
                    if (!currentCollidingObjects.Contains(cameraHideable))
                    {
                        if (!lastCollidingObjects.Contains(cameraHideable))
                            SetTransparency(cameraHideable);
                        currentCollidingObjects.Add(cameraHideable);
                    }
                }
            }
        }
        List<CameraHideable> notCollidingObjects = lastCollidingObjects.Except(currentCollidingObjects).ToList();
        foreach(CameraHideable nonCollidingObject in notCollidingObjects)
        {
            SetTransparency(nonCollidingObject, false);
        }
        lastCollidingObjects = currentCollidingObjects;
	}

    void SetTransparency(CameraHideable cameraHideable, bool transparent = true)
    {
        cameraHideable.SetTransparency(transparent);
    }

    
}
