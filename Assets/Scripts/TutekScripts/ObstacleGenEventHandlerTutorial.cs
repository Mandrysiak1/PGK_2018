using Assets.PGKScripts;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ObstacleGenEventHandlerTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject sittingGuest;
    [SerializeField]
    private float ThrowPower = 110.0f;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float ThrowPowerVariation = 0.25f;
    [SerializeField]
    private float ThrowYOffset = 0.5f;
    [SerializeField]
    private float ThrowYPowerMultiplier = 2.0f;
    [SerializeField]
    private GameObject prefab;

    //private Scene currentScene;
    private Vector3 Position;

    private System.Random rand = new System.Random();

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



    public void CalculatePosition()
    {

        // TODO: move to other method
        // trigger guest animator
        Animator animator = sittingGuest.GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("ThrowTrigger");


        // get position for new obstacle
        Bounds bounds = GetTotalMeshFilterBounds(sittingGuest.transform);
        float height = ThrowYOffset;
        Vector3 sittingGuestTopPosition = sittingGuest.transform.position + new Vector3(0, height, 0);

        Position = sittingGuestTopPosition;// new Vector3(x, yValue, z);
        //NavMeshHit hit;
        //NavMesh.SamplePosition(Position, out hit, 10, -1);
        //Position = hit.position;
        //Position.y = yValue;
    }

    public void SpawnOnObject()
    {

        UnityEngine.Object spawned = Instantiate(prefab, Position, Quaternion.Euler(0, 32.028f, 0));
        GameObject gameObject = spawned as GameObject;
        if (gameObject != null)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.AddForce(new Vector3(
                (((float)rand.NextDouble() - 0.5f) * 2 * (ThrowPowerVariation + 1)) * ThrowPower,
                ((float)rand.NextDouble() * ThrowPowerVariation + 1) * ThrowPower * ThrowYPowerMultiplier,
                (((float)rand.NextDouble() - 0.5f) * 2 * (ThrowPowerVariation + 1)) * ThrowPower
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
