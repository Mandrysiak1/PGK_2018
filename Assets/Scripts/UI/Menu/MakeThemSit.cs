using UnityEngine;

public class MakeThemSit : MonoBehaviour
{
    public float OffsetY = -0.11f;

    private void Awake()
    {
        foreach (var sittingGuest in FindObjectsOfType<SittingGuestAnimation>())
        {
            var animator = sittingGuest.GetComponent<Animator>();
            animator.CrossFade("Blend Tree", 0.0f);

            var pos = sittingGuest.transform.localPosition;
            pos.y += OffsetY;
            sittingGuest.transform.localPosition = pos;
        }
    }
}
