using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraHidingWalls : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private float Opacity = 0.1f;

    private List<GameObject> lastCollidingObjects = new List<GameObject>();

	void Update ()
    {
        RaycastHit[] hits;
        Vector3 position = this.transform.position;
        Vector3 direction = Player.transform.position - position;
        hits = Physics.RaycastAll(position, direction);

        List<GameObject> currentCollidingObjects = new List<GameObject>();
        foreach(RaycastHit hit in hits)
        {
            if (hit.collider != null)
            {
                Collider collider = hit.collider;
                GameObject collidedObject = collider.gameObject;

                if (!currentCollidingObjects.Contains(collidedObject))
                {
                    if (collidedObject != Player)
                    {
                        SetTransparent(collidedObject);
                        currentCollidingObjects.Add(collidedObject);
                    }
                }
            }
        }
        List<GameObject> notCollidingObjects = lastCollidingObjects.Except(currentCollidingObjects).ToList();
        foreach(GameObject nonCollidingObject in notCollidingObjects)
        {
            SetTransparent(nonCollidingObject, false);
        }
        lastCollidingObjects = currentCollidingObjects;
	}
    void SetTransparent(GameObject gameObject, bool transparent = true)
    {
        if(gameObject != null)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (transparent)
                    foreach(Material material in renderer.materials)
                        material.color = new Color(material.color.r, material.color.g, material.color.b, Opacity);
                else
                    foreach (Material material in renderer.materials)
                        material.color = new Color(material.color.r, material.color.g, material.color.b, 1f);
            }
        }
    }
}
