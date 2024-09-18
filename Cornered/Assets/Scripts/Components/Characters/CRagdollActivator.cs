/// <summary>
/// Filename: CRagdollActivator.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CCharacterController))]
public class CRagdollActivator : MonoBehaviour
{
    [SerializeField] protected Animator m_BodyAnimator;
    [SerializeField] protected GameObject m_Head;
    [SerializeField] protected GameObject m_Body;
    [SerializeField] protected GameObject m_LeftUpperArm;
    [SerializeField] protected GameObject m_RightUpperArm;
    [SerializeField] protected GameObject m_LeftLowerArm;
    [SerializeField] protected GameObject m_RightLowerArm;
    [SerializeField] protected GameObject m_LeftUpperLeg;
    [SerializeField] protected GameObject m_RightUpperLeg;
    [SerializeField] protected GameObject m_LeftLowerLeg;
    [SerializeField] protected GameObject m_RightLowerLeg;
    [SerializeField] protected List<GameObject> m_BodyAndLimbs = new();

    private CCharacterController m_Controller;
    private bool m_RagdollActivated;
    private List<GameObject> m_LimbsConnectingBody = new();

    protected void OnEnable()
    {
        EventManager.AddListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    protected void OnDisable()
    {
        EventManager.RemoveListener<CharacterDefeatedEvent>(OnCharacterDefeated);
    }

    protected virtual void Start()
    {
        m_Controller = GetComponent<CCharacterController>();

        m_LimbsConnectingBody.Add(m_LeftUpperLeg);
        m_LimbsConnectingBody.Add(m_RightUpperLeg);
        m_LimbsConnectingBody.Add(m_LeftUpperArm);
        m_LimbsConnectingBody.Add(m_RightUpperArm);
    }

    protected virtual void OnCharacterDefeated(CharacterDefeatedEvent ev)
    {
        if (m_RagdollActivated || 
            (m_Controller is CPlayerController && ev.characterType != ECharacterType.Player) ||
            (m_Controller is CEnemyController && ev.characterType != ECharacterType.Enemy))
        {
            return;
        }

        SetWholeBodyComponents();

        Rigidbody headRigidbody = SetHead();

        SetBodyAndLimbs();

        SetBody(headRigidbody);

        SetLimbs();

        m_RagdollActivated = true;
    }

    private void SetWholeBodyComponents()
    {
        m_BodyAnimator.enabled = false;
        m_Controller.enabled = false;
    }

    private Rigidbody SetHead()
    {
        BoxCollider headBoxCollider = m_Head.GetComponent<BoxCollider>();
        Rigidbody headRigidBody = m_Head.AddComponent<Rigidbody>();
        if (headBoxCollider != null)
        {
            headBoxCollider.enabled = true;
        }
        if (headRigidBody != null)
        {
            headRigidBody.isKinematic = false;
            headRigidBody.useGravity = true;
        }

        return headRigidBody;
    }

    private void SetBody(Rigidbody headRigidbody)
    {
        CharacterJoint bodyCharacterJoint = m_Body.GetComponent<CharacterJoint>();
        if (bodyCharacterJoint != null)
        {
            bodyCharacterJoint.connectedBody = headRigidbody;
        }
    }

    private void SetBodyAndLimbs()
    {
        for (int i = 0; i < m_BodyAndLimbs.Count; i++)
        {
            Rigidbody rigidbody = m_BodyAndLimbs[i].AddComponent<Rigidbody>();
            CharacterJoint characterJoint = m_BodyAndLimbs[i].AddComponent<CharacterJoint>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
    }

    private void SetLimbs()
    {
        Rigidbody bodyRigidbody = m_Body.GetComponent<Rigidbody>();

        for (int i = 0; i < m_LimbsConnectingBody.Count; i++)
        {
            CharacterJoint bodyPartCharacterJoint = m_LimbsConnectingBody[i].GetComponent<CharacterJoint>();
            if (bodyPartCharacterJoint != null)
            {
                bodyPartCharacterJoint.connectedBody = bodyRigidbody;
            }
        }

        CharacterJoint rightUpperArmJoint = m_RightUpperArm.GetComponent<CharacterJoint>();
        CharacterJoint rightLowerArmJoint = m_RightLowerArm.GetComponent<CharacterJoint>();
        CharacterJoint leftLowerArmJoint = m_LeftLowerArm.GetComponent<CharacterJoint>();
        CharacterJoint leftLowerLegJoint = m_LeftLowerLeg.GetComponent<CharacterJoint>();
        CharacterJoint rightLowerLegJoint = m_RightLowerLeg.GetComponent<CharacterJoint>();

        if (rightUpperArmJoint != null)
        {
            Vector3 anchor = rightUpperArmJoint.anchor;
            anchor.y = anchor.y * -1;
            rightUpperArmJoint.anchor = anchor;
        }

        if (rightLowerArmJoint != null)
        {
            Vector3 anchor = rightLowerArmJoint.anchor;
            anchor.y = anchor.y * -1;
            rightLowerArmJoint.anchor = anchor;
        }

        if (leftLowerArmJoint != null)
        {
            leftLowerArmJoint.connectedBody = m_LeftUpperArm.GetComponent<Rigidbody>();
        }

        if (rightLowerArmJoint != null)
        {
            rightLowerArmJoint.connectedBody = m_RightUpperArm.GetComponent<Rigidbody>();
        }

        if (leftLowerLegJoint != null)
        {
            leftLowerLegJoint.connectedBody = m_LeftUpperLeg.GetComponent<Rigidbody>();
        }

        if (rightLowerLegJoint != null)
        {
            rightLowerLegJoint.connectedBody = m_RightUpperLeg.GetComponent<Rigidbody>();
        }

        for (int i = 0; i < m_LimbsConnectingBody.Count; i++)
        {
            BoxCollider bodyPartBoxCollider = m_LimbsConnectingBody[i].GetComponent<BoxCollider>();
            if (bodyPartBoxCollider != null)
            {
                bodyPartBoxCollider.enabled = true;
            }
        }
    }
}
