using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProduct : CIngredient, IEquippable
{
    public override IEquippable GetEquippable()
    {
        return this;
    }

    public virtual void Equip()
    {
        Destroy(gameObject);
    }
}
