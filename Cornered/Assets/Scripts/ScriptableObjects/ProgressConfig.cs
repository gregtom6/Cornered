using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Progress Config")]
public class ProgressConfig : ScriptableObject
{
    [SerializeField] private List<UnlockableAbilities> m_AbilitiesToUnlockPerLevel = new();
    public int maxUnlockLevel => m_AbilitiesToUnlockPerLevel.Count;
    public bool IsAbilityAlreadyUnlocked(EAbility ability)
    {
        int currentUnlockLevel = ProgressManager.Instance.currentUnlockLevel;

        IReadOnlyList<EAbility> unlockedAbilities = GetAlreadyUnlockedAbilities(currentUnlockLevel);

        return unlockedAbilities.Contains(ability);
    }

    private IReadOnlyList<EAbility> GetAlreadyUnlockedAbilities(int currentUnlockLevel)
    {
        List<EAbility> abilities = new();

        for (int i = currentUnlockLevel; i >= 0; i--)
        {
            abilities.AddRange(m_AbilitiesToUnlockPerLevel[i].abilities);
        }

        return abilities;
    }
}

[Serializable]
public struct UnlockableAbilities
{
    public List<EAbility> abilities;
}

