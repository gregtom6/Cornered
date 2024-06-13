using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEventAudioPlayer : MonoBehaviour
{
    [SerializeField] private List<CAudioPlayer> m_AudioPlayers = new();

    public void OnEventHappened()
    {
        m_AudioPlayers.ForEach(x => x.Play());
    }
}
