/// <summary>
/// Filename: CCharacterManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterManager : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private List<Transform> m_EnemyPossibleStartingTransforms = new();

    public static CCharacterManager instance;

    public Transform playerTransform => instance.m_Player;

    private GameObject m_EnemyInstanceGameObject;

    public Vector3 GetCharacterPosition(ECharacterType characterType)
    {
        if (characterType == ECharacterType.Player && m_Player != null)
        {
            return m_Player.position;
        }
        else if (characterType == ECharacterType.Enemy && m_EnemyInstanceGameObject != null)
        {
            return m_EnemyInstanceGameObject.transform.position;
        }

        return Vector3.zero;
    }

    public CEquipmentVisualizer GetEquipmentVisualizer(ECharacterType characterType)
    {
        if (characterType == ECharacterType.Player && m_Player != null)
        {
            return m_Player.GetComponentInParent<CEquipmentVisualizer>();
        }
        else if (characterType == ECharacterType.Enemy && m_EnemyInstanceGameObject != null)
        {
            return m_EnemyInstanceGameObject.GetComponentInParent<CEquipmentVisualizer>();
        }

        return null;
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        EventManager.AddListener<NewMatchStartedEvent>(OnNewMatchStarted);
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeatedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStarted);
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeatedEvent);
    }

    private void OnNewMatchStarted(NewMatchStartedEvent ev)
    {
        Transform selectedTransform = m_EnemyPossibleStartingTransforms.GetRandom();
        m_EnemyInstanceGameObject = Instantiate(AllConfig.Instance.CharacterConfig.enemyPrefab, selectedTransform.position, Quaternion.identity, null);
        EventManager.Raise(new EnemyGeneratedEvent { enemy = m_EnemyInstanceGameObject });
    }

    private void OnCharacterDefeatedEvent(CharacterDefeatedEvent ev)
    {
        if (ev.characterType == ECharacterType.Enemy)
        {
            Destroy(m_EnemyInstanceGameObject);
        }
    }
}