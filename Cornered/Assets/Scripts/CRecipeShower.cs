using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRecipeShower : MonoBehaviour
{
    [SerializeField] private Transform m_StartingTransform;
    [SerializeField] private float m_HorizontalGapSize;
    [SerializeField] private float m_VerticalGapSize;

    private void Start()
    {
        GenerateRecipeVisuals();
    }

    private void GenerateRecipeVisuals()
    {
        Vector3 positionForGeneration = m_StartingTransform.position;

        IReadOnlyList<IReadOnlyList<Material>> materials = AllConfig.Instance.RecipeConfig.GetRadiatingMaterialsOfAllRecipes();

        for (int i = 0; i < materials.Count; i++)
        {
            for (int j = 0; j < materials[i].Count; j++)
            {
                GameObject recipeElement = Instantiate(AllConfig.Instance.RecipeConfig.recipeShowElementPrefab, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);

                MeshRenderer meshRenderer = recipeElement.GetComponentInParent<MeshRenderer>();

                meshRenderer.material = materials[i][j];

                positionForGeneration = new Vector3(positionForGeneration.x + m_HorizontalGapSize, positionForGeneration.y, positionForGeneration.z);

                if (j + 2 < materials[i].Count)
                {
                    recipeElement = Instantiate(AllConfig.Instance.RecipeConfig.recipeShowElementPrefab, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);

                    meshRenderer = recipeElement.GetComponentInParent<MeshRenderer>();

                    meshRenderer.material = AllConfig.Instance.RecipeConfig.plusSignMaterial;

                    positionForGeneration = new Vector3(positionForGeneration.x + m_HorizontalGapSize, positionForGeneration.y, positionForGeneration.z);
                }
                else if (j + 2 == materials[i].Count)
                {
                    recipeElement = Instantiate(AllConfig.Instance.RecipeConfig.recipeShowElementPrefab, positionForGeneration, Quaternion.Euler(90, 0, 0), transform);

                    meshRenderer = recipeElement.GetComponentInParent<MeshRenderer>();

                    meshRenderer.material = AllConfig.Instance.RecipeConfig.equalSignMaterial;

                    positionForGeneration = new Vector3(positionForGeneration.x + m_HorizontalGapSize, positionForGeneration.y, positionForGeneration.z);
                }

            }

            positionForGeneration = new Vector3(m_StartingTransform.position.x, positionForGeneration.y + m_VerticalGapSize, positionForGeneration.z);
        }
    }
}
