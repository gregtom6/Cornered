using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CRagdollActivator : MonoBehaviour
{
    public bool isPlayer;
    public Animator bodyAnimator;
    public CCharacterController controller;
    public GameObject head;
    public GameObject body;
    public GameObject leftUpperArm;
    public GameObject rightUpperArm;
    public GameObject leftLowerArm;
    public GameObject rightLowerArm;
    public GameObject leftUpperLeg;
    public GameObject rightUpperLeg;
    public GameObject leftLowerLeg;
    public GameObject rightLowerLeg;
    public NavMeshAgent navmeshAgent;
    public List<GameObject> bodyparts = new();

    // Start is called before the first frame update
    void Start()
    {
        bodyAnimator.enabled = false;
        controller.enabled = false;

        head.AddComponent<BoxCollider>();
        body.AddComponent<BoxCollider>();
        Rigidbody headRigidBody = head.AddComponent<Rigidbody>();
        headRigidBody.isKinematic = false;
        headRigidBody.useGravity = true;

        for (int i = 0; i < bodyparts.Count; i++)
        {
            Rigidbody rigidbody = bodyparts[i].AddComponent<Rigidbody>();
            CharacterJoint characterJoint = bodyparts[i].AddComponent<CharacterJoint>();
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }

        body.GetComponent<CharacterJoint>().connectedBody = headRigidBody;
        leftUpperArm.GetComponent<CharacterJoint>().connectedBody = body.GetComponent<Rigidbody>();
        rightUpperArm.GetComponent<CharacterJoint>().connectedBody = body.GetComponent<Rigidbody>();
        leftUpperLeg.GetComponent<CharacterJoint>().connectedBody = body.GetComponent<Rigidbody>();
        rightUpperLeg.GetComponent<CharacterJoint>().connectedBody = body.GetComponent<Rigidbody>();

        leftLowerArm.GetComponent<CharacterJoint>().connectedBody = leftUpperArm.GetComponent<Rigidbody>();
        rightLowerArm.GetComponent<CharacterJoint>().connectedBody = rightUpperArm.GetComponent<Rigidbody>();

        leftLowerLeg.GetComponent<CharacterJoint>().connectedBody = leftUpperLeg.GetComponent<Rigidbody>();
        rightLowerLeg.GetComponent<CharacterJoint>().connectedBody = rightUpperLeg.GetComponent<Rigidbody>();

        Vector3 anchor = rightUpperArm.GetComponent<CharacterJoint>().anchor;
        anchor.y = anchor.y * -1;
        rightUpperArm.GetComponent<CharacterJoint>().anchor = anchor;

        anchor = rightLowerArm.GetComponent<CharacterJoint>().anchor;
        anchor.y = anchor.y * -1;
        rightLowerArm.GetComponent<CharacterJoint>().anchor = anchor;

        if (!isPlayer)
        {
            navmeshAgent.enabled = false;

            leftUpperLeg.AddComponent<BoxCollider>();
            leftLowerLeg.AddComponent<BoxCollider>();

            rightUpperLeg.AddComponent<BoxCollider>();
            rightLowerLeg.AddComponent<BoxCollider>();

            leftUpperArm.AddComponent<BoxCollider>();
            leftLowerArm.AddComponent<BoxCollider>();

            rightUpperArm.AddComponent<BoxCollider>();
            rightLowerArm.AddComponent<BoxCollider>();
        }
    }
}
