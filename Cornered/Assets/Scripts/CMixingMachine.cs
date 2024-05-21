using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMixingMachine : MonoBehaviour
{
    [SerializeField] private Transform m_ResultTargetTransform;
    [SerializeField] private CMixingItemDetector m_MixingItemDetector;
    [SerializeField] private CButton m_FreezeButton;
    [SerializeField] private CButton m_BurnButton;
    [SerializeField] private CButton m_ConvertButton;

    private void OnEnable()
    {
        m_FreezeButton.pressHappened += FreezeButtonPressed;
        m_BurnButton.pressHappened += BurnButtonPressed;
        m_ConvertButton.pressHappened += ConvertButtonPressed;
    }

    private void OnDisable()
    {
        m_FreezeButton.pressHappened -= FreezeButtonPressed;
        m_BurnButton.pressHappened -= BurnButtonPressed;
        m_ConvertButton.pressHappened -= ConvertButtonPressed;
    }

    private void FreezeButtonPressed()
    {
        m_MixingItemDetector.FreezeAllItems();
    }

    private void BurnButtonPressed()
    {
        m_MixingItemDetector.BurnAllItems();
    }

    private void ConvertButtonPressed()
    {
        IReadOnlyList<ItemTypes> detectedItems = m_MixingItemDetector.GetDetectedItems();

        GameObject resultPrefab = AllConfig.Instance.RecipeConfig.GetResultItem(detectedItems);

        Instantiate(resultPrefab, m_ResultTargetTransform.position, Quaternion.identity, null);

        m_MixingItemDetector.DestroyAllItems();
    }

}
