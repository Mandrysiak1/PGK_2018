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
    [SerializeField]
    private float FadingTime = 0.15f;

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
                        if (!lastCollidingObjects.Contains(collidedObject))
                            SetTransparency(collidedObject);
                        currentCollidingObjects.Add(collidedObject);
                    }
                }
            }
        }
        List<GameObject> notCollidingObjects = lastCollidingObjects.Except(currentCollidingObjects).ToList();
        foreach(GameObject nonCollidingObject in notCollidingObjects)
        {
            SetTransparency(nonCollidingObject, false);
        }
        lastCollidingObjects = currentCollidingObjects;
	}

    void SetTransparency(GameObject gameObject, bool transparent = true)
    {
        if(gameObject != null)
        {
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                StartCoroutine(FadingCoroutine(renderer.materials, transparent));
            }
        }
    }

    IEnumerator FadingCoroutine(Material[] materials, bool fadeTo = true)
    {
        float startAlpha = materials[0].color.a;
        float endAlpha = 1.0f;
        if (fadeTo)
        {
            endAlpha = Opacity;
        }
        for(float i = 0.0f; i < 1.0f; i += Time.deltaTime / FadingTime)
        {
            foreach (Material material in materials)
                material.color = new Color(
                    material.color.r, 
                    material.color.g, 
                    material.color.b, 
                    Mathf.Lerp(startAlpha, endAlpha, i)
                    );
            yield return null;
        }
    }
}
