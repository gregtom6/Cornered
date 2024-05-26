using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterManager : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private List<Transform> m_EnemyPossibleStartingTransforms = new();

    public static CCharacterManager instance;

    private GameObject m_EnemyInstanceGameObject;

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

    public static Vector3 playerPosition => instance.m_Player.position;

    public static Transform playerTransform => instance.m_Player;

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
}

public struct EnemyGeneratedEvent
{
    public GameObject enemy;
}