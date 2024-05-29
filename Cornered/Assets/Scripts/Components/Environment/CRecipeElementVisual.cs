/// <summary>
/// Filename: CRecipeElementVisual.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRecipeElementVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_ElementMeshRenderer;
    [SerializeField] private MeshRenderer m_EffectMeshRenderer;

    public void SetElement(Material elementMaterial, Material effectMaterial)
    {
        m_ElementMeshRenderer.material = elementMaterial;
        m_EffectMeshRenderer.material = effectMaterial;

        m_EffectMeshRenderer.gameObject.SetActive(effectMaterial != null);
    }
}
