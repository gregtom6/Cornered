using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponHint : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_HintMeshRenderer;

    private void OnEnable()
    {
        EventManager.AddListener<EquipmentDecidedEvent>(OnEnemyEquipmentDecidedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EquipmentDecidedEvent>(OnEnemyEquipmentDecidedEvent);
    }

    void OnEnemyEquipmentDecidedEvent(EquipmentDecidedEvent e)
    {
        m_HintMeshRenderer.material = AllConfig.Instance.HintConfig.GetMaterialBasedOnItemType(e.weaponItem.item);
    }
}

public struct EquipmentDecidedEvent
{
    public ECharacterType characterType;
    public ItemTypes weaponItem;
    public ItemTypes shieldItem;
    public ItemTypes additionalItem;
}