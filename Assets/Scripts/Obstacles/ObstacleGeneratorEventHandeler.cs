using Assets.PGKScripts;
using UnityEngine;


public class ObstacleGeneratorEventHandeler : MonoBehaviour
{

    private Vector3 Position;
    private Object[] prefabs;

    void Start()
    {
        prefabs = Resources.LoadAll("Prefabs");

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
        Position = new Vector3(Random.Range(-6, 8), -0.918f, Random.Range(-45, -35));

    }

    private void SpawnOnObject()
    {
        int whatToSpawn = Random.Range(0, prefabs.Length);

        Instantiate(prefabs[whatToSpawn], Position, Quaternion.Euler(0, 32.028f, 0));

    }
}
