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
                GameObject recipeElement = Instantiate(AllConfig.Instance.RecipeConfig.recipeShowElementPrefab, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);

                m_RecipeVisualElements.Add(recipeElement);

                CRecipeElementVisual meshRenderer = recipeElement.GetComponentInParent<CRecipeElementVisual>();

                meshRenderer.SetElement(materials[i][j],stateMaterials[i][j]);

                positionForGeneration = new Vector3(positionForGeneration.x + m_HorizontalGapSize, positionForGeneration.y, positionForGeneration.z);

                if (j + 2 < materials[i].Count)
                {
                    recipeElement = Instantiate(AllConfig.Instance.RecipeConfig.recipeShowOperatorPrefab, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);

                    m_RecipeVisualElements.Add(recipeElement);

                    meshRenderer = recipeElement.GetComponentInParent<CRecipeElementVisual>();

                    meshRenderer.SetElement(AllConfig.Instance.RecipeConfig.plusSignMaterial,null);

                    positionForGeneration = new Vector3(positionForGeneration.x + m_HorizontalGapSize, positionForGeneration.y, positionForGeneration.z);
                }
                else if (j + 2 == materials[i].Count)
                {
                    recipeElement = Instantiate(AllConfig.Instance.RecipeConfig.recipeShowOperatorPrefab, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);

                    m_RecipeVisualElements.Add(recipeElement);

                    meshRenderer = recipeElement.GetComponentInParent<CRecipeElementVisual>();

                    meshRenderer.SetElement(AllConfig.Instance.RecipeConfig.equalSignMaterial,null);

                    positionForGeneration = new Vector3(positionForGeneration.x + m_HorizontalGapSize, positionForGeneration.y, positionForGeneration.z);
                }

            }

            positionForGeneration = new Vector3(m_StartingTransform.position.x, positionForGeneration.y + m_VerticalGapSize, positionForGeneration.z);
        }
    }
}
