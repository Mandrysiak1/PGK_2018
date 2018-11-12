using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public float AnimationTime = 0.33f;

    [SerializeField]
    private LevelLoadListener LevelLoaded;

    [SerializeField]
    private Image Background;

    [SerializeField]
    private TextMeshProUGUI Text;

    private void Awake()
    {
        Enable();
        LevelLoaded.LevelLoaded.AddListener(Disable);
    }

    private void Enable()
    {
        Color color = Background.color;
        color.a = 1.0f;
        Background.color = color;

        color = Text.color;
        color.a = 1.0f;
        Text.color = color;
    }

    private void Disable(GameLevel level, LevelScene levelScene)
    {
        Background.DOFade(0.0f, AnimationTime);
        Text.DOFade(0.0f, AnimationTime);
    }

}
