using DG.Tweening;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public float AnimationTime = 0.33f;
    public float MessageStayTime = 3.0f;

    [SerializeField]
    private TextMeshProUGUI Text;

    protected void Start()
    {
        MakeInvisible();
    }

    public void Show(string text)
    {
        MakeInvisible();
        Text.text = text;
        var sequence = DOTween.Sequence();
        var showUp = Text.DOFade(1.0f, AnimationTime);
        var fadeOut = Text.DOFade(0.0f, AnimationTime);

        sequence
            .Append(showUp)
            .AppendInterval(MessageStayTime)
            .Append(fadeOut);
    }

    private void MakeInvisible()
    {
        Color color = Text.color;
        color.a = 0.0f;
        Text.color = color;
    }
}
