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
    public Vector3 playerPosition => instance.m_Player.position;

    public Transform playerTransform => instance.m_Player;

    private GameObject m_EnemyInstanceGameObject;
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
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStarted);
    }

    private void OnNewMatchStarted(NewMatchStartedEvent ev)
    {
        Transform selectedTransform = m_EnemyPossibleStartingTransforms.GetRandom();
        m_EnemyInstanceGameObject = Instantiate(AllConfig.Instance.CharacterConfig.enemyPrefab, selectedTransform.position, Quaternion.identity, null);
        EventManager.Raise(new EnemyGeneratedEvent { enemy = m_EnemyInstanceGameObject });
    }
}