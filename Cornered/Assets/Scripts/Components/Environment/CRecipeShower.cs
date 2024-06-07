/// <summary>
/// Filename: CRecipeShower.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRecipeShower : MonoBehaviour
{
    [SerializeField] private Transform m_StartingTransform;
    [SerializeField] private float m_HorizontalGapSize;
    [SerializeField] private float m_VerticalGapSize;

    private List<GameObject> m_RecipeVisualElements = new();

    private void OnEnable()
    {
        EventManager.AddListener<NewMatchStartedEvent>(OnNewMatchStartedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStartedEvent);
    }

    private void OnNewMatchStartedEvent(NewMatchStartedEvent ev)
    {
        DestroyElements();
        GenerateRecipeVisuals();
    }

    private void DestroyElements()
    {
        m_RecipeVisualElements.ForEach(x => Destroy(x));
        m_RecipeVisualElements.Clear();
    }

    private void GenerateRecipeVisuals()
    {
        Vector3 positionForGeneration = m_StartingTransform.position;

        IReadOnlyList<IReadOnlyList<Material>> materials = AllConfig.Instance.RecipeConfig.GetRadiatingMaterialsOfAllRecipes(out IReadOnlyList<IReadOnlyList<Material>> stateMaterials);

        for (int i = 0; i < materials.Count; i++)
        {
            for (int j = 0; j < materials[i].Count; j++)
            {
                RecipeElementCreation(AllConfig.Instance.RecipeConfig.recipeShowElementPrefab, ref positionForGeneration, materials[i][j], m_HorizontalGapSize, stateMaterials[i][j]);

                if (j + 2 < materials[i].Count)
                {
                    RecipeElementCreation(AllConfig.Instance.RecipeConfig.recipeShowOperatorPrefab, ref positionForGeneration, AllConfig.Instance.RecipeConfig.plusSignMaterial, m_HorizontalGapSize);
                }
                else if (j + 2 == materials[i].Count)
                {
                    RecipeElementCreation(AllConfig.Instance.RecipeConfig.recipeShowOperatorPrefab, ref positionForGeneration, AllConfig.Instance.RecipeConfig.equalSignMaterial, m_HorizontalGapSize);
                }
            }

            positionForGeneration = new Vector3(m_StartingTransform.position.x, positionForGeneration.y + m_VerticalGapSize, positionForGeneration.z);
        }
    }

    private void RecipeElementCreation(GameObject prefabGameObject, ref Vector3 positionForGeneration, Material elementMaterial, float xDelta, Material effectMaterial = null)
    {
        GameObject recipeElement = Instantiate(prefabGameObject, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);
        m_RecipeVisualElements.Add(recipeElement);
        CRecipeElementVisual meshRenderer = recipeElement.GetComponentInParent<CRecipeElementVisual>();
        meshRenderer.SetElement(elementMaterial, effectMaterial);
        positionForGeneration = new Vector3(positionForGeneration.x + xDelta, positionForGeneration.y, positionForGeneration.z);
    }
}
