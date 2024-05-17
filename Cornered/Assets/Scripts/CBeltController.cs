using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CBeltController : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private CTriggerContainer m_Spawner;
    [SerializeField] private CTriggerContainer m_Despawner;
    [SerializeField] private Transform m_Parent;

    private IObjectPool<CBeltElement> m_BeltElementPool;

    public static CBeltController instance;

    private void OnEnable()
    {
        m_Despawner.objectEntered += OnDespawnerObjectEntered;
    }

    private void OnDisable()
    {
        m_Despawner.objectEntered -= OnDespawnerObjectEntered;
    }

    void OnDespawnerObjectEntered(Transform transform)
    {
        CBeltElement beltElement = transform.GetComponentInParent<CBeltElement>();
        if (beltElement != null)
        {
            m_BeltElementPool.Release(beltElement);
        }
    }

    private void Start()
    {
        instance = this;

        m_BeltElementPool = new ObjectPool<CBeltElement>(CreateBeltElement, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
            AllConfig.Instance.beltConfig.collectionCheck, AllConfig.Instance.beltConfig.defaultCapacity, AllConfig.Instance.beltConfig.maxSize);

    }

    private void Update()
    {
        if (!m_Spawner.isInside)
        {
            m_BeltElementPool.Get();
        }
    }

    private CBeltElement CreateBeltElement()
    {
        CBeltElement element = Instantiate<CBeltElement>(AllConfig.Instance.beltConfig.beltElementPrefab, m_SpawnPoint.position, Quaternion.identity, m_Parent);
        element.SetObjectPool((ObjectPool<CBeltElement>)m_BeltElementPool);
        return element;
    }

    private void OnReleaseToPool(CBeltElement element)
    {
        element.gameObject.SetActive(false);
    }

    private void OnGetFromPool(CBeltElement element)
    {
        element.transform.position = m_SpawnPoint.position;
        element.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(CBeltElement beltElement)
    {
        Destroy(beltElement.gameObject);
    }

    public EBeltSpeed beltSpeed => m_BeltSpeed;

    private EBeltSpeed m_BeltSpeed = EBeltSpeed.Normal;

    public float GetCurrentMultiplier()
    {
        return AllConfig.Instance.beltConfig.GetCurrentMultiplier(m_BeltSpeed);
    }
}

public enum EBeltSpeed
{
    Normal,
    Fastened,

    Count,
}