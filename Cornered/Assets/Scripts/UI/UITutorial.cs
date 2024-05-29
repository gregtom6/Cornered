/// <summary>
/// Filename: UITutorial.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private Image m_Image;
    [SerializeField] private TextMeshProUGUI m_Text;
    [SerializeField] private Button m_LeftPageButton;
    [SerializeField] private Button m_RightPageButton;

    private int m_CurrentPageIndex;

    private void OnEnable()
    {
        m_CurrentPageIndex = 0;
        ShowCurrentPage();
    }

    public void OnLeftPageButton()
    {
        if (m_CurrentPageIndex - 1 >= 0)
        {
            m_CurrentPageIndex -= 1;
            ShowCurrentPage();
        }
    }

    public void OnRightPageButton()
    {
        if (m_CurrentPageIndex + 1 < AllConfig.Instance.TutorialConfig.tutorialPageCount)
        {
            m_CurrentPageIndex += 1;
            ShowCurrentPage();
        }
    }

    private void ManagePageSteppingButtons()
    {
        m_LeftPageButton.interactable = m_CurrentPageIndex > 0;
        m_RightPageButton.interactable = m_CurrentPageIndex < AllConfig.Instance.TutorialConfig.tutorialPageCount - 1;
    }

    private void ShowCurrentPage()
    {
        ManagePageSteppingButtons();

        TutorialPageDatas tutorialPageDatas = AllConfig.Instance.TutorialConfig.GetTutorialPageDatas(m_CurrentPageIndex);
        m_Image.sprite = tutorialPageDatas.image;
        m_Text.text = tutorialPageDatas.text;
    }
}
