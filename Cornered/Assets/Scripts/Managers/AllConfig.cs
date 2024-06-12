/// <summary>
/// Filename: AllConfig.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllConfig : MonoBehaviour
{
    [SerializeField] private SOCharacterConfig m_CharacterConfig;
    [SerializeField] private SOBeltConfig m_BeltConfig;
    [SerializeField] private SOIngredientGenerationConfig m_IngredientGenerationConfig;
    [SerializeField] private SORecipeConfig m_RecipeConfig;
    [SerializeField] private SOTimeConfig m_TimeConfig;
    [SerializeField] private SOHintConfig m_HintConfig;
    [SerializeField] private SOEquipmentConfig m_EquipmentConfig;
    [SerializeField] private SOAIConfig m_AIConfig;
    [SerializeField] private SOMixingMachineConfig m_MixingMachineConfig;
    [SerializeField] private SOProgressConfig m_ProgressConfig;
    [SerializeField] private SOExitDoorConfig m_ExitDoorConfig;
    [SerializeField] private SOTutorialConfig m_TutorialConfig;
    [SerializeField] private SOControlsConfig m_ControlsConfig;
    [SerializeField] private SOAudioConfig m_AudioConfig;

    public SOCharacterConfig CharacterConfig => m_CharacterConfig;

    public SOBeltConfig beltConfig => m_BeltConfig;

    public SOIngredientGenerationConfig IngredientGenerationConfig => m_IngredientGenerationConfig;

    public SORecipeConfig RecipeConfig => m_RecipeConfig;

    public SOTimeConfig TimeConfig => m_TimeConfig;

    public SOHintConfig HintConfig => m_HintConfig;

    public SOEquipmentConfig EquipmentConfig => m_EquipmentConfig;

    public SOAIConfig AIConfig => m_AIConfig;

    public SOProgressConfig ProgressConfig => m_ProgressConfig;

    public SOMixingMachineConfig MixingMachineConfig => m_MixingMachineConfig;

    public SOExitDoorConfig ExitDoorConfig => m_ExitDoorConfig;

    public SOTutorialConfig TutorialConfig => m_TutorialConfig;

    public SOControlsConfig ControlsConfig => m_ControlsConfig;

    public SOAudioConfig AudioConfig => m_AudioConfig;

    public static AllConfig Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
