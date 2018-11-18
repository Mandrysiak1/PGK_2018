using TMPro;
using UnityEngine;

public class ItemChangedAnimation : MonoBehaviour
{
    public float Scale = 1.5f;

    public float AnimationTime = 0.5f;
    public AnimationCurve Curve;

    [SerializeField]
    private TextMeshProUGUI Text;

    [SerializeField]
    private Transform ScaleTransform;

    private float Timer = 0.0f;
    private bool Animate = false;
    private Color TargetColor;
    private Transform TextTransform;
    private Vector3 InitialScale;
    private Color NormalColor = Color.white;

    private void Start()
    {
        TextTransform = Text.transform;
        InitialScale = ScaleTransform.localScale;
        NormalColor = Text.color;
    }

    private void Update()
    {
        if (Animate)
        {
            Timer += Time.deltaTime;

            float t = Curve.Evaluate(Timer / AnimationTime);
            Text.color = Color.Lerp(NormalColor, TargetColor, t);
            ScaleTransform.localScale = Vector3.Lerp(InitialScale, InitialScale * Scale, t);
            if (Timer > AnimationTime)
            {
                Animate = false;
            }

        }
    }

    public void StartAnimation(Color color)
    {
        Animate = true;
        Timer = 0.0f;
        TargetColor = color;
    }
}
