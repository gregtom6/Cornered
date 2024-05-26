using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HidingRoomElementsCollector : MonoBehaviour
{
    [SerializeField] private List<Collider> m_HidingElementColliders = new();

    public static HidingRoomElementsCollector Instance;

    public bool IsAnyColliderContainsPosition(Vector3 position)
    {
        return m_HidingElementColliders.Any(x => x.bounds.Contains(position));
    }

    private void Start()
    {
        Instance = this;
    }
}
