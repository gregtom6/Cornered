using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private List<SceneSetting> m_GameOverScenesToLoad = new List<SceneSetting>();

    private int m_MatchIndex;

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

    void OnCharacterDefeated(CharacterDefeatedEvent e)
    {
        if (e.characterType == ECharacterType.Player)
        {
            InitiateGameOver();
        }
        else if (e.characterType == ECharacterType.Enemy)
        {
            m_MatchIndex += 1;
            InitiateNewMatch();
        }
    }

    private void InitiateGameOver()
    {
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

public struct CharacterDefeatedEvent
{
    public ECharacterType characterType;
}

public struct NewMatchStartedEvent
{
    public int matchIndex;
}