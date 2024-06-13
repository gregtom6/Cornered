/// <summary>
/// Filename: MatchManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private List<SceneSetting> m_GameOverScenesToLoad = new();

    private int m_MatchIndex;
    private float m_MatchEndedStartTime;
    private bool m_ShouldWaitUntilNewMatch;

    private void OnEnable()
    {
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    private void Start()
    {
        InitiateNewMatch();
    }

    private void OnCharacterDefeated(CharacterDefeatedEvent e)
    {
        if (e.characterType == ECharacterType.Player)
        {
            InitiateGameOver();
        }
        else if (e.characterType == ECharacterType.Enemy)
        {
            m_MatchIndex += 1;
            m_MatchEndedStartTime = Time.time;
            m_ShouldWaitUntilNewMatch = true;
        }
    }

    private void Update()
    {
        if (!m_ShouldWaitUntilNewMatch)
        {
            return;
        }

        float currentTime = Time.time - m_MatchEndedStartTime;
        if (currentTime >= AllConfig.Instance.TimeConfig.waitBetweenPreviousAndNewMatchInSec)
        {
            m_ShouldWaitUntilNewMatch = false;
            InitiateNewMatch();
        }
    }

    private void InitiateGameOver()
    {
        SoundManager.instance.StopAll();

        m_GameOverScenesToLoad.ForEach(x =>
        {
            SceneManager.LoadScene(x.sceneReference, x.loadSceneMode);
        });
    }

    private void InitiateNewMatch()
    {
        EventManager.Raise(new NewMatchStartedEvent { matchIndex = m_MatchIndex });
    }
}