using Assets.PGKScripts;
using System;
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

    private Scene currentScene;
    private Vector3 Position;
    private UnityEngine.Object[] prefabs;

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
            else
            {
                ;
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
        System.Random rand = new System.Random();

        int x = rand.Next(xMin, xMax);
        
        int z = rand.Next(zMin,zMax);
        Position = new Vector3(x, yValue, z);
        NavMeshHit hit;
        NavMesh.SamplePosition(Position, out hit, 1, -1);
        Position = hit.position;
        Position.y = yValue;
    }

    private void SpawnOnObject()
    {
        int whatToSpawn = UnityEngine.Random.Range(0, prefabs.Length);

        Instantiate(prefabs[whatToSpawn], Position, Quaternion.Euler(0, 32.028f, 0));

    }
}
