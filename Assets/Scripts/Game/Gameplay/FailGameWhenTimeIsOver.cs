
using UnityEngine;

public class FailGameWhenTimeIsOver : MonoBehaviour
{
    public float Limit = 124;

    private float Timer;

    [SerializeField]
    private MainScript Main;

    private void Start()
    {
        Timer = 0.0f;
    }

    public void Reset()
    {
        Timer = 0.0f;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > Limit)
        {
            Main.GameOver();
        }
    }
}
