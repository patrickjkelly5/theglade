using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public OVRInput.Controller controller;

    public float grabRadius;
    public LayerMask grabMask;

    private GameObject grabbedObject;
    private bool grabbing;

    public string buttonName;

    private Quaternion lastRotation, currentRotation;

    public float newYPosition;

    void GrabObject()
    {

        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if (hits.Length > 0)
        {

            int closestHit = 0;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance) closestHit = i;
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

            if (grabbedObject.tag == "Ball") {
                newYPosition = transform.position.y;
            } else {
                newYPosition = transform.position.y + 0.1f;
            }


            grabbedObject.transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            //grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;

        }

    }

    void DropObject()
    {
        grabbing = false;
         
        if (grabbedObject != null)
        {

            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            Vector3 temp = OVRInput.GetLocalControllerVelocity(controller);
            temp.x = (temp.x * -1);
            temp.z = (temp.z * -1);

            Debug.Log(temp);

            grabbedObject.GetComponent<Rigidbody>().velocity = temp;
            //grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

            grabbedObject = null;

        }
    }


    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }


    void Update () {

        if (grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

        if (!grabbing && Input.GetAxis(buttonName) == 1) GrabObject();
        if (grabbing && Input.GetAxis(buttonName) < 1) DropObject();

    }
}
