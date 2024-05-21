using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CShieldHint : MonoBehaviour
{
    [SerializeField] private MeshRenderer m_HintMeshRenderer;

    //TODO: osszeolvasztani a CShieldHint es WeaponHint osztalyokat

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
        m_HintMeshRenderer.material = AllConfig.Instance.HintConfig.GetMaterialBasedOnItemType(e.shieldItem.item);
    }
}