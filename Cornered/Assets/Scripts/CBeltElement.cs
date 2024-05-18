using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CBeltElement : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnTransform;
    private ObjectPool<CBeltElement> m_Pool;

    private GameObject m_CreatedItem;

    public void SetObjectPool(ObjectPool<CBeltElement> pool)
    {
        m_Pool = pool;
    }

    private void Update()
    {
        float currentBeltSpeed = CBeltController.instance.GetCurrentMultiplier();
        transform.Translate(-transform.right * Time.deltaTime * currentBeltSpeed);
    }

    void Start()
    {
        if(m_CreatedItem == null)
        {
            GameObject prefab = AllConfig.Instance.IngredientGenerationConfig.GetWeightedRandomItemPrefab();
            m_CreatedItem = Instantiate(prefab, m_SpawnTransform.position, Quaternion.identity, m_SpawnTransform);
        }
    }
}
