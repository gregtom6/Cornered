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

        for (int currentRow = 0; currentRow < materials.Count; currentRow++)
        {
            GenerateRecipeRow(materials[currentRow], stateMaterials[currentRow], ref positionForGeneration);
            MoveToNextRow(ref positionForGeneration);
        }
    }

    private void CreateRecipeElement(Material material, Material stateMaterial, ref Vector3 positionForGeneration)
    {
        RecipeElementCreation(AllConfig.Instance.RecipeConfig.recipeShowElementPrefab, ref positionForGeneration, material, m_HorizontalGapSize, stateMaterial);
    }

    private void AddOperatorIfNeeded(int index, int materialCount, ref Vector3 positionForGeneration)
    {
        if (IsIndexBeforeLastElement(index, materialCount))
        {
            CreateOperator(AllConfig.Instance.RecipeConfig.plusSignMaterial, ref positionForGeneration);
        }
        else
        {
            CreateOperator(AllConfig.Instance.RecipeConfig.equalSignMaterial, ref positionForGeneration);
        }
    }

    //TODO: replace into an util class

    private bool IsIndexBeforeLastElement(int index, int count)
    {
        return index + 2 < count;
    }

    private void CreateOperator(Material operatorMaterial, ref Vector3 positionForGeneration)
    {
        RecipeElementCreation(AllConfig.Instance.RecipeConfig.recipeShowOperatorPrefab, ref positionForGeneration, operatorMaterial, m_HorizontalGapSize);
    }

    private void MoveToNextRow(ref Vector3 positionForGeneration)
    {
        positionForGeneration = new Vector3(m_StartingTransform.position.x, positionForGeneration.y + m_VerticalGapSize, positionForGeneration.z);
    }

    private void GenerateRecipeRow(IReadOnlyList<Material> materials, IReadOnlyList<Material> stateMaterials, ref Vector3 positionForGeneration)
    {
        for (int currentColumn = 0; currentColumn < materials.Count; currentColumn++)
        {
            CreateRecipeElement(materials[currentColumn], stateMaterials[currentColumn], ref positionForGeneration);
            AddOperatorIfNeeded(currentColumn, materials.Count, ref positionForGeneration);
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
