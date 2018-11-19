using Assets.PGKScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ObstacleGeneratorEventHandeler : MonoBehaviour
{
    private Scene currentScene;
    private Vector3 Position;
    private Object[] prefabs;

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
        Position = new Vector3(Random.Range(-6, 8), -0.918f, Random.Range(-45, -35));
        NavMeshHit hit;
        NavMesh.SamplePosition(Position, out hit, 10, 1);
        Position = hit.position;
    }

    private void SpawnOnObject()
    {
        int whatToSpawn = Random.Range(0, prefabs.Length);

        Instantiate(prefabs[whatToSpawn], Position, Quaternion.Euler(0, 32.028f, 0));

    }
}
