using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    public int currentUnlockLevel => m_UnlockLevel;

    private int m_UnlockLevel;

    public void ResetProgress()
    {
        m_UnlockLevel = 0;
    }

    private void Start()
    {
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeated);

        m_UnlockLevel = 0;
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }


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

    private void OnCharacterDefeated(CharacterDefeatedEvent ev)
    {
        if (ev.characterType == ECharacterType.Enemy)
        {
            m_UnlockLevel = Mathf.Clamp(m_UnlockLevel + 1, 0, AllConfig.Instance.ProgressConfig.maxUnlockLevel);
        }
        else if (ev.characterType == ECharacterType.Player)
        {
            m_UnlockLevel = 0;
        }
    }
}