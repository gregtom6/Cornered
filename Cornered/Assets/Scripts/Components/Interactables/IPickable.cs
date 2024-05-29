/// <summary>
/// Filename: IPickable.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using UnityEngine;

public interface IPickable
{
    void Pickup(Transform transform);

    void Drop();

    bool IsPicked();

    bool WasPickedAnytime();

    IEquippable GetEquippable();
}
