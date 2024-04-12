using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogPass : MonoBehaviour
{
    public Material fogMaterial;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        fogMaterial.SetTexture("_MainTex", source);
        Graphics.Blit(source, destination, fogMaterial);
    }
}
