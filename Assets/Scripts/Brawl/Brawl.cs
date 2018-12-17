using System.Collections.Generic;
using UnityEngine;

public class Brawl : MonoBehaviour
{
    [SerializeField]
    private GameObject Variants;

    [SerializeField]
    private Transform CameraPosition;

    public string[] Insults;
    public AudioClip StartSound;


    public void Setup(Camera camera)
    {
        camera.transform.position = CameraPosition.position;
        camera.transform.localRotation = CameraPosition.localRotation;

        Variants.SetActive(true);
    }
}
