using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRagdollActivator : MonoBehaviour
{
    public Animator bodyAnimator;
    public CPlayerController controller;
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
    public List<GameObject> bodyparts = new();

    // Start is called before the first frame update
    void Start()
    {
        bodyAnimator.enabled = false;
        controller.enabled = false;

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
        rightLowerArm.GetComponent<CharacterJoint>().connectedBody= rightUpperArm.GetComponent<Rigidbody>();

        leftLowerLeg.GetComponent<CharacterJoint>().connectedBody=leftUpperArm.GetComponent<Rigidbody>();
        rightLowerLeg.GetComponent<CharacterJoint>().connectedBody=rightUpperArm.GetComponent<Rigidbody>();

        Vector3 anchor = rightUpperArm.GetComponent<CharacterJoint>().anchor;
        anchor.y = anchor.y * -1;
        rightUpperArm.GetComponent<CharacterJoint>().anchor = anchor;

        anchor=rightLowerArm.GetComponent<CharacterJoint>().anchor;
        anchor.y = anchor.y * -1;
        rightLowerArm.GetComponent<CharacterJoint>().anchor = anchor;

        anchor=rightUpperLeg.GetComponent<CharacterJoint>().anchor;
        anchor.y= anchor.y * -1;
        rightUpperLeg.GetComponent <CharacterJoint>().anchor = anchor;

        anchor=rightLowerLeg.GetComponent<CharacterJoint>().anchor;
        anchor.y=anchor.y * -1;
        rightLowerLeg.GetComponent <CharacterJoint>().anchor = anchor;
    }
}
