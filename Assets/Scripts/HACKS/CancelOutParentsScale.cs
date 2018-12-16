using UnityEngine;

public class CancelOutParentsScale : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(
            1.0f / transform.lossyScale.x,
            1.0f / transform.lossyScale.y,
            1.0f / transform.lossyScale.z);
    }
}
