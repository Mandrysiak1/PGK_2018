using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerPlateUI : MonoBehaviour
{
    public Color LoseColor = Color.red;
    public Color GainColor = Color.green;
    public Color NormalColor = Color.white;
    public float Scale = 1.5f;
    public string TextFormat = "x {0}";

    public float AnimationTime = 0.5f;
    public AnimationCurve Curve;

    [SerializeField]
    private PlayerPlate Plate;
    [SerializeField]
    private TextMeshProUGUI Text;

    [SerializeField]
    private GameObject SpecialItem;



    private float Timer = 0.0f;
    private bool Animate = false;
    private Color TargetColor;
    private Transform TextTransform;
    private Vector3 InitialTextScale;

    private void Start()
    {
        SpecialItem.GetComponent<RawImage>().enabled = false;

        Plate.OnItemAmountChanged.AddListener(BeerCountChanged);
        TextTransform = Text.transform;
        InitialTextScale = TextTransform.localScale;
        SetBeerCount(Plate.Beers);



    }

    private void Update()
    {
        if (Animate)
        {
            Timer += Time.deltaTime;

            float t = Curve.Evaluate(Timer / AnimationTime);
            Text.color = Color.Lerp(NormalColor, TargetColor, t);
            TextTransform.localScale = Vector3.Lerp(InitialTextScale, InitialTextScale * Scale, t);
            if (Timer > AnimationTime)
                Animate = false;

        }
    }

    private void BeerCountChanged(OrderItem x, int current, int old)
    {
        if (x.name != "Beer")
        {
            if (current == 1)
            {
                SpecialItem.GetComponent<RawImage>().enabled = true;
                SpecialItem.GetComponent<RawImage>().texture = x.Sprite.texture;


            }
            else
            {

                SpecialItem.GetComponent<RawImage>().enabled = false;
            }

        }
        else
        {
            Color color;
            if (current < old)
            {
                color = LoseColor;
            }
            else
            {
                color = GainColor;
            }

            SetBeerCount(current);
            StartAnimation(color);
        }


    }

    private void SetBeerCount(int current)
    {
        Text.text = string.Format(TextFormat, current);
    }

    private void StartAnimation(Color color)
    {
        Animate = true;
        Timer = 0.0f;
        TargetColor = color;
    }
}
