using UnityEngine;

public class SaveSyncOnStart : MonoBehaviour
{
    [SerializeField]
    private SaveSystem Save;

    private void Start()
    {
        Save.Sync();
    }
}
