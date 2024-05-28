using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    void Pickup(Transform transform);

    void Drop();

    bool IsPicked();

    bool WasPickedAnytime();

    IEquippable GetEquippable();
}
