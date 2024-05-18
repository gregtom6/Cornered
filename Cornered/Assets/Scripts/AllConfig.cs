using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllConfig : MonoBehaviour
{
    [SerializeField] private CharacterConfig m_CharacterConfig;
    [SerializeField] private BeltConfig m_BeltConfig;
    [SerializeField] private IngredientGenerationConfig m_IngredientGenerationConfig;
    [SerializeField] private RecipeConfig m_RecipeConfig;

    public CharacterConfig CharacterConfig => m_CharacterConfig;

    public BeltConfig beltConfig => m_BeltConfig;

    public IngredientGenerationConfig IngredientGenerationConfig => m_IngredientGenerationConfig;

    public RecipeConfig RecipeConfig => m_RecipeConfig;

    public static AllConfig Instance;

    private void Awake()
    {
        Instance = this; 
    }
}
