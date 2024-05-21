using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CBeltController : MonoBehaviour
{
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private CTriggerContainer m_Spawner;
    [SerializeField] private CTriggerContainer m_Despawner;
    [SerializeField] private CConveyorBeltSpeederButton m_SpeederButton;
    [SerializeField] private Transform m_Parent;
    [SerializeField] private CButton m_Button;

    public EBeltSpeed currentBeltSpeed => m_CurrentBeltSpeed;

    private IObjectPool<CBeltElement> m_BeltElementPool;

    private EBeltSpeed m_CurrentBeltSpeed = EBeltSpeed.Normal;

    public static CBeltController instance;

    public float GetCurrentMultiplier()
    {
        return AllConfig.Instance.beltConfig.GetCurrentMultiplier(m_CurrentBeltSpeed);
    }

    private void OnPressHappened()
    {
        SwitchBeltSpeed();
    }

    private void SwitchBeltSpeed()
    {
        m_CurrentBeltSpeed = m_CurrentBeltSpeed == EBeltSpeed.Normal ? EBeltSpeed.Fastened : EBeltSpeed.Normal;
    }

    private void OnEnable()
    {
        m_Spawner.allObjectLeft += OnAllObjectLeftFromSpawner;
        m_Despawner.objectEntered += OnDespawnerObjectEntered;

        m_Button.pressHappened += OnPressHappened;
    }

    private void OnDisable()
    {
        m_Spawner.allObjectLeft -= OnAllObjectLeftFromSpawner;
        m_Despawner.objectEntered -= OnDespawnerObjectEntered;

        m_Button.pressHappened -= OnPressHappened;
    }

    void OnAllObjectLeftFromSpawner()
    {
        m_BeltElementPool.Get();
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

        m_BeltElementPool.Get();
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


}

public enum EBeltSpeed
{
    Normal,
    Fastened,

    Count,
}