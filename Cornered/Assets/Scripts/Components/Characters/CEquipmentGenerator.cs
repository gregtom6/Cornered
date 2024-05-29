/// <summary>
/// Filename: CEquipmentGenerator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquipmentGenerator : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener<EnemyGeneratedEvent>(OnEnemyGeneratedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EnemyGeneratedEvent>(OnEnemyGeneratedEvent);
    }

    private void OnEnemyGeneratedEvent(EnemyGeneratedEvent ev)
    {
        GenerateEquipment(ECharacterType.Enemy);
    }

    private void GenerateEquipment(ECharacterType characterType)
    {
        ItemDatas weapon = AllConfig.Instance.EquipmentConfig.GetRandomWeapon();
        ItemDatas shield = AllConfig.Instance.EquipmentConfig.GetRandomShield();
        ItemDatas additional = AllConfig.Instance.EquipmentConfig.GetRandomAdditional();

        EventManager.Raise(new EquipmentDecidedEvent { characterType = characterType, weaponItem = weapon, shieldItem = shield, additionalItem = additional });
    }
}