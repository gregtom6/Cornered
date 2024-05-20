using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDeadDestroyer : MonoBehaviour
{
    [SerializeField] private CHealth m_Health;

    private void Update()
    {
        if (m_Health.currentHealth <= AllConfig.Instance.CharacterConfig.minHealth)
        {
            Destroy(gameObject);
        }
    }
}
