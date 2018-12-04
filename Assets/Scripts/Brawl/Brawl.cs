using System.Collections.Generic;
using UnityEngine;

public class Brawl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Variants;

    [SerializeField]
    private Transform CameraPosition;

    public string[] Insults;
    public AudioClip StartSound;

    private void Start()
    {
        DisableAll();
    }

    public void Setup(Camera camera)
    {
        camera.transform.position = CameraPosition.position;
        camera.transform.localRotation = CameraPosition.localRotation;

        Variants.Random().gameObject.SetActive(true);
    }

    private void DisableAll()
    {
        Variants.ForEach(g => g.gameObject.SetActive(false));
    }
}
