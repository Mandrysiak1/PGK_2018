using UnityEngine;
using UnityEngine.UI;

public class Autoscroll : MonoBehaviour
{
    public float Speed = 1.0f;
    public float Delay = 0.0f;
    [SerializeField]
    private ScrollRect Scroll;

    private float StartTime;

    private void OnEnable()
    {
        StartTime = Time.time;
        Scroll.verticalNormalizedPosition = 1.0f;
    }

    private void Update()
    {
        if (Time.time > StartTime + Delay)
        {
            if(Scroll.verticalNormalizedPosition > 0.00001f)
                Scroll.verticalNormalizedPosition -= Speed * Time.deltaTime;
        }
    }
}
