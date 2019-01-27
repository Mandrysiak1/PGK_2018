using UnityEngine;

public class EnableInDebug : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(Debug.isDebugBuild);
    }
}
