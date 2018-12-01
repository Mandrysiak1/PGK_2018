using UnityEngine;

public class FailGameWhenDissatisfactionExceeded : MonoBehaviour
{
    public float Treshold = 100.0f;

    [SerializeField]
    private MoodBasedDissatisfaction Dissatisfaction;

    [SerializeField]
    private MainScript Main;

    private void Start()
    {
        Dissatisfaction.DissatisfactionChanged.AddListener(DissatisfactionChanged);
    }

    private void DissatisfactionChanged(float current, float old)
    {
        if (current > Treshold)
        {
            Main.GameOver();
        }
    }
}
