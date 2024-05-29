/// <summary>
/// Filename: GameDefinitions.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>


using System;

public enum ECharacterType
{
    Player,
    Enemy,

    Count,
}

public enum EEnemyState
{
    Waiting,
    ShootPosition,
    DefendPosition,

    Count,
}

public enum EEquipment
{
    Weapon,
    Shield,
    Additional,

    Count,
}

public enum EMovementState
{
    Standing,
    Walking,
    Strafing,

    Count,
}

public enum EItemState
{
    Normal,
    Freezed,
    Burned,

    Count,
}

[Serializable]
public enum EAbility
{
    Default,
    Burn,
    Freeze,

    Count,
}

public enum EItemType
{
    EmptyItem,
    Tube,
    Marbles,
    Board,
    Coffee,
    Boots,
    Petrol,
    Pistol,
    Shotgun,
    FastBoots,
    FlamingShotgun,
    Freezer,
    DefenderPhysical,
    DefenderPhysicalExtra,
    DefenderHeat,
    DefenderCold,
    Rope,

    Count,
}

public enum EBeltSpeed
{
    Normal,
    Fastened,

    Count,
}

public enum EMixingMachineState
{
    Heating,
    Freezing,
    Mixing,
    Waiting,

    Count,
}

public enum EMainMenuState
{
    Main,
    Hint,
    Controls,

    Count,
}