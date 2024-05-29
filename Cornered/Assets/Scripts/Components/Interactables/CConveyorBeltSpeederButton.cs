/// <summary>
/// Filename: CConveyorBeltSpeederButton.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CConveyorBeltSpeederButton : CButton
{
    [SerializeField] private CBeltController m_BeltController;
    [SerializeField] private MeshRenderer m_MeshRenderer;
    public override void Interact()
    {
        base.Interact();

        m_MeshRenderer.material = AllConfig.Instance.beltConfig.GetMaterialBasedOnSpeed(m_BeltController.currentBeltSpeed);
    }

    private void Start()
    {
        m_MeshRenderer.material = AllConfig.Instance.beltConfig.GetMaterialBasedOnSpeed(m_BeltController.currentBeltSpeed);
    }

}
