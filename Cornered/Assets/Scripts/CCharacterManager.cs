using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterManager : MonoBehaviour
{
    [SerializeField] private Transform m_Player;
    [SerializeField] private Transform m_Enemy;

    public static CCharacterManager instance;

    private void Start()
    {
        instance = this;
    }

    public static Vector3 playerPosition => instance.m_Player.position;
}
