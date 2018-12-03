using Assets.PGKScripts;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ObstacleGenEventHandlerTutorial : MonoBehaviour
{

    public GameObject prefab;
    [SerializeField]
    public int xMin, xMax, zMin, zMax;
    [SerializeField]
    public float yValue;

    //private Scene currentScene;
    private Vector3 Position;
    

    void Start()
    {
        var x = FindObjectOfType<ObstacleGenerator>();
        x.OnGenerateObstacle += HandleObstacleGenerator;

    }

    private void HandleObstacleGenerator()
    {
        
    }

    public void GenerateObstacle()
    {
        CalculatePosition();
        SpawnOnObject();
    }

    private void CalculatePosition()
    {
        System.Random rand = new System.Random();

        int x = rand.Next(xMin, xMax);

        int z = rand.Next(zMin, zMax);
        Position = new Vector3(x, yValue, z);
        NavMeshHit hit;
        NavMesh.SamplePosition(Position, out hit, 10, -1);
        Position = hit.position;
        Position.y = yValue;
    }

    public void SpawnOnObject()
    {
        Instantiate(prefab, Position, Quaternion.Euler(0, 32.028f, 0));

    }
}
