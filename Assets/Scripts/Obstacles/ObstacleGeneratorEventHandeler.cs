using Assets.PGKScripts;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ObstacleGeneratorEventHandeler : MonoBehaviour
{
    [SerializeField]
    private int xMin, xMax, zMin, zMax;
    [SerializeField]
    private float yValue;
    [SerializeField]
    private List<GameObject> sittingGuests;
    [SerializeField]
    private float ThrowPower = 110.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float ThrowPowerVariation = 0.25f;
    [SerializeField]
    private float ThrowYOffset = 0.5f;
    [SerializeField]
    private float ThrowYPowerMultiplier = 2.0f;

    private Scene currentScene;
    private Vector3 Position;
    private UnityEngine.Object[] prefabs;

    private System.Random rand = new System.Random();

    void Start()
    {
        Scene[] scenes = new Scene[SceneManager.sceneCount];
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i);
            Debug.Log(scenes[i].name + " z obstacle");

            if (scenes[i].name == "level2")
            {
                prefabs = Resources.LoadAll("ObjectGeneratorStuff");
            }
            else if (scenes[i].name == "fortnit")
            {
                prefabs = Resources.LoadAll("ObjectGeneratorStuffFortnit");
            }
            else if(scenes[i].name == "level3")
            {
                prefabs = Resources.LoadAll("ObjectGeneratorStuffChristmas");
            }
            else
            {
                prefabs = Resources.LoadAll("ObjectGeneratorStuffWoods");
            }
        }
        var x = FindObjectOfType<ObstacleGenerator>();
        x.OnGenerateObstacle += HandleObstacleGenerator;

    }

    private void HandleObstacleGenerator()
    {
        CalculatePosition();
        SpawnOnObject();
    }

    private void CalculatePosition()
    {
        

        GameObject sittingGuest = sittingGuests[rand.Next(0, sittingGuests.Count - 1)];

        // TODO: move to other method
        // trigger guest animator
        Animator animator = sittingGuest.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("ThrowTrigger");


        // get position for new obstacle
        Bounds bounds = GetTotalMeshFilterBounds(sittingGuest.transform);
        float height = ThrowYOffset;
        Vector3 sittingGuestTopPosition = sittingGuest.transform.position + new Vector3(0, height, 0);

        int x = rand.Next(xMin, xMax);
        int z = rand.Next(zMin,zMax);

        Position = sittingGuestTopPosition;// new Vector3(x, yValue, z);
        //NavMeshHit hit;
        //NavMesh.SamplePosition(Position, out hit, 10, -1);
        //Position = hit.position;
        //Position.y = yValue;
    }

    private void SpawnOnObject()
    {
        int whatToSpawn = UnityEngine.Random.Range(0, prefabs.Length);

        UnityEngine.Object spawned = Instantiate(prefabs[whatToSpawn], Position, Quaternion.Euler(0, 32.028f, 0));
        GameObject gameObject = spawned as GameObject;
        if(gameObject != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(new Vector3(
                (((float)rand.NextDouble() - 0.5f) * 2 * ThrowPowerVariation) * ThrowPower,
                ((float)rand.NextDouble() * ThrowPowerVariation + 1) * ThrowPower * ThrowYPowerMultiplier,
                (((float)rand.NextDouble() - 0.5f) * 2 * ThrowPowerVariation) * ThrowPower
                ));
        }
    }

    private Bounds GetTotalMeshFilterBounds(Transform objectTransform)
    {
        var meshFilter = objectTransform.GetComponent<MeshFilter>();
        var result = meshFilter != null ? meshFilter.mesh.bounds : new Bounds();

        foreach (Transform transform in objectTransform)
        {
            var bounds = GetTotalMeshFilterBounds(transform);
            result.Encapsulate(bounds.min);
            result.Encapsulate(bounds.max);
        }

        //var scaledMin = result.min;
        //scaledMin.Scale(objectTransform.localScale);
        //result.min = scaledMin;

        //var scaledMax = result.max;
        //scaledMax.Scale(objectTransform.localScale);
        //result.max = scaledMax;

        return result;
    }
}
