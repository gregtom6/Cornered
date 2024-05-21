using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEquipmentGenerator : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.AddListener<NewMatchStartedEvent>(OnNewMatchStartedEvent);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<NewMatchStartedEvent>(OnNewMatchStartedEvent);
    }

    private void OnNewMatchStartedEvent(NewMatchStartedEvent ev)
    {
        GenerateEquipment(ECharacterType.Enemy);
    }

    private void GenerateEquipment(ECharacterType characterType)
    {
        ItemTypes weapon = AllConfig.Instance.WeaponConfig.GetRandomWeapon();
        ItemTypes shield = AllConfig.Instance.WeaponConfig.GetRandomShield();
        ItemTypes additional = AllConfig.Instance.WeaponConfig.GetRandomAdditional();

        EventManager.Raise(new EquipmentDecidedEvent { characterType = characterType, weaponItem = weapon, shieldItem = shield, additionalItem = additional });
    }
}

public enum ECharacterType
{
    Player,
    Enemy,

    Count,
}