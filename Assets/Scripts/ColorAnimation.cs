using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorAnimation : MonoBehaviour
{
    [SerializeField]
    private Graphic[] Objects;

    public bool AlphaOnly = false;
    public Color TargetColor, SourceColor;
    public float AnimationTime = 0;
    public bool AutoPlay = false;
    public bool Loop = false;
    public bool IsAnimating
    {
        get
        {
            return Animating;
        }
    }

    private bool SwapAtEnd = false;
    private bool Animating = false;
    private float Timer = 0;

    void Start()
    {
        if(AutoPlay)
            Animate();
    }

    public void Animate(Color fromColor, Color toColor, float time)
    {
        SourceColor = fromColor;
        TargetColor = toColor;

        ApplySourceColor();
        Animate(time);
    }

    public void ApplySourceColor()
    {
        foreach (Graphic obj in Objects)
        {
            if (!AlphaOnly)
                obj.color = SourceColor;
            else
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, SourceColor.a);
        }
    }

    public void SwapColors()
    {
        var old = TargetColor;
        TargetColor = SourceColor;
        SourceColor = old;
    }

    public void Animate(float time)
    {
        AnimationTime = time;
        Animate();
    }

    public void AnimateReverse()
    {
        SwapColors();
        SwapAtEnd = true;
        Animate();
    }

    public void Animate()
    {
        SwapAtEnd = false;
        ApplySourceColor();
        Animating = true;
        Timer = 0;
        Update();
    }

    private void SetAnimationState(float ratio)
    {
        foreach (Graphic obj in Objects)
        {
            if (!AlphaOnly)
                obj.color = Color.Lerp(SourceColor, TargetColor, ratio);
            else
                obj.color = new Color(obj.color.r, obj.color.g, obj.color.b, Mathf.Lerp(SourceColor.a, TargetColor.a, ratio));
        }
    }

    private void Update()
    {
        if(Animating)
        {
            SetAnimationState(Timer / AnimationTime);

            Timer += Time.deltaTime;
            if (Timer >= AnimationTime)
            {
                SetAnimationState(1.0f);
                Animating = false;
                if(Loop || SwapAtEnd)
                {
                    SwapColors();
                    if(SwapAtEnd)
                        SwapAtEnd = false;
                }
            }
        }
    }
}
