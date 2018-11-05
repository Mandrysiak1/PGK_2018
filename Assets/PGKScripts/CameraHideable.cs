using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraHideable : MonoBehaviour
{
    [SerializeField]
    private float Opacity = 0.03f;
    [SerializeField]
    private float FadingTime = 0.10f;

    private List<Material> materials = new List<Material>();
    private List<Renderer> renderers = new List<Renderer>();

    void Start()
    {
        foreach (Transform child in this.transform)
        {
            renderers.AddRange(child.GetComponentsInChildren<Renderer>());
        }
        Renderer thisRenderer = GetComponent<Renderer>();
        if (thisRenderer != null)
            renderers.Add(thisRenderer);
        foreach (Renderer renderer in renderers)
        {
            materials.AddRange(renderer.materials.ToList());
        }
    }

    private void SetMaterialTypeTransparent(Material material, bool transparent = true)
    {
        if (transparent)
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
        else    // opaque
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }
    }
    
    public void SetTransparency(bool transparent = true)
    {
        StartCoroutine(FadingCoroutine(transparent));
    }

    IEnumerator FadingCoroutine(bool fadeTo = true)
    {
        // change type to transparent to make fading available
        if (fadeTo)
        {
            foreach (Material material in materials)
            {
                SetMaterialTypeTransparent(material, true);
            }
        }
        float startAlpha = materials[0].color.a;
        float endAlpha = 1.0f;
        if (fadeTo)
        {
            endAlpha = Opacity;
        }
        for (float i = 0.0f; i <= 1.0f; i += Time.deltaTime / FadingTime)
        {
            foreach (Material material in materials)
                material.color = new Color(
                    material.color.r,
                    material.color.g,
                    material.color.b,
                    Mathf.Lerp(startAlpha, endAlpha, i)
                    );
            yield return null;
        }
        foreach (Material material in materials)
            material.color = new Color(
                material.color.r,
                material.color.g,
                material.color.b,
                endAlpha
                );
        yield return null;
        // change type back if it is no longer needed to be transparent
        if (!fadeTo)
        {
            foreach (Material material in materials)
            {
                SetMaterialTypeTransparent(material, false);
            }
        }
        yield return null;
    }
}