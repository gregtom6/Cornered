using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CBeltElement : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnTransform;
    private ObjectPool<CBeltElement> m_Pool;

    private CIngredient m_CreatedItem;

    public void SetObjectPool(ObjectPool<CBeltElement> pool)
    {
        m_Pool = pool;
    }

    private void Update()
    {
        float currentBeltSpeed = CBeltController.instance.GetCurrentMultiplier();
        transform.Translate(-transform.right * Time.deltaTime * currentBeltSpeed);
    }

    private void OnEnable()
    {
        CIngredient prefab = AllConfig.Instance.IngredientGenerationConfig.GetWeightedRandomItemPrefab();
        m_CreatedItem = Instantiate<CIngredient>(prefab, m_SpawnTransform.position, Quaternion.identity, m_SpawnTransform);
    }

    private void OnDisable()
    {
        if (!m_CreatedItem.WasPickedAnytime())
        {
            Destroy(m_CreatedItem.gameObject);
        }
    }
}
