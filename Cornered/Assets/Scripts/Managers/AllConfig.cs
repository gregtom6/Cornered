using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllConfig : MonoBehaviour
{
    [SerializeField] private CharacterConfig m_CharacterConfig;
    [SerializeField] private BeltConfig m_BeltConfig;
    [SerializeField] private IngredientGenerationConfig m_IngredientGenerationConfig;
    [SerializeField] private RecipeConfig m_RecipeConfig;
    [SerializeField] private TimeConfig m_TimeConfig;
    [SerializeField] private HintConfig m_HintConfig;
    [SerializeField] private WeaponConfig m_WeaponConfig;
    [SerializeField] private AIConfig m_AIConfig;
    [SerializeField] private MixingMachineConfig m_MixingMachineConfig;
    [SerializeField] private ProgressConfig m_ProgressConfig;
    [SerializeField] private ExitDoorConfig m_ExitDoorConfig;

    public CharacterConfig CharacterConfig => m_CharacterConfig;

    public BeltConfig beltConfig => m_BeltConfig;

    public IngredientGenerationConfig IngredientGenerationConfig => m_IngredientGenerationConfig;

    public RecipeConfig RecipeConfig => m_RecipeConfig;

    public TimeConfig TimeConfig => m_TimeConfig;

    public HintConfig HintConfig => m_HintConfig;

    public WeaponConfig WeaponConfig => m_WeaponConfig;

    public AIConfig AIConfig => m_AIConfig;

    public ProgressConfig ProgressConfig => m_ProgressConfig;

    public MixingMachineConfig MixingMachineConfig => m_MixingMachineConfig;

    public ExitDoorConfig ExitDoorConfig => m_ExitDoorConfig;

    public static AllConfig Instance;

    private void Awake()
    {
        Instance = this;
    }
}
