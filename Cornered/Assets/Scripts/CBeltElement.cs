using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CBeltElement : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnTransform;
    private ObjectPool<CBeltElement> m_Pool;

    public void SetObjectPool(ObjectPool<CBeltElement> pool)
    {
        m_Pool = pool;
    }

    private void OnEnable()
    {
        GameObject prefab = AllConfig.Instance.IngredientGenerationConfig.GetWeightedRandomItemPrefab();
        Instantiate(prefab, m_SpawnTransform.position, Quaternion.identity, transform);
    }

    private void Update()
    {
        float currentBeltSpeed = CBeltController.instance.GetCurrentMultiplier();
        transform.Translate(-transform.right * Time.deltaTime * currentBeltSpeed);
    }
}
