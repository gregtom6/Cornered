using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CBeltElement : MonoBehaviour
{
    private ObjectPool<CBeltElement> m_Pool;

    public void SetObjectPool(ObjectPool<CBeltElement> pool)
    {
        m_Pool = pool;
    }

    void Update()
    {
        float currentBeltSpeed = CBeltController.instance.GetCurrentMultiplier();
        transform.Translate(-transform.right * Time.deltaTime * currentBeltSpeed);
    }
}
