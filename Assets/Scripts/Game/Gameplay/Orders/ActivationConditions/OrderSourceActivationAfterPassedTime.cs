using UnityEngine;

public class OrderSourceActivationAfterPassedTime : OrderSourceActivationCondition
{
    [Tooltip("Time in seconds until order source can issue orders")]
    public float Seconds = 10.0f;

    private float Timer;

    private void Start()
    {
        Timer = 0.0f;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public override bool IsMeet()
    {
        return Timer > Seconds;
    }


}
