/// <summary>
/// Filename: CTimeZeroerButton.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTimeZeroerButton : CButton
{
    public override void Interact()
    {
        base.Interact();

        TimeManager.instance.ZeroingTimer();
    }
}
