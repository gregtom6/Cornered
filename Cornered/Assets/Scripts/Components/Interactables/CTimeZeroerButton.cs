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
