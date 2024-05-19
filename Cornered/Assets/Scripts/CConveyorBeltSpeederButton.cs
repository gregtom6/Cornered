using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CConveyorBeltSpeederButton : MonoBehaviour, IInteractable
{
    [SerializeField] private CBeltController m_BeltController;
    [SerializeField] private MeshRenderer m_MeshRenderer;

    private void Start()
    {
        m_MeshRenderer.material = AllConfig.Instance.beltConfig.GetMaterialBasedOnSpeed(m_BeltController.currentBeltSpeed);
    }

    public void Interact()
    {
        m_BeltController.ButtonPress();

        m_MeshRenderer.material = AllConfig.Instance.beltConfig.GetMaterialBasedOnSpeed(m_BeltController.currentBeltSpeed);
    }
}
