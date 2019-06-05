using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    private Transform myTransform;              // this transform
    private Vector3 destinationPosition;        // The destination Point
    private float destinationDistance;
    // private bool isMovingUnit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Physics.IgnoreLayerCollision(0, 18);
        myTransform = transform;                            // sets myTransform to this GameObject.transform
        destinationPosition = myTransform.position;
    }
    //public void StartUp()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    rb.freezeRotation = true;
    //    Physics.IgnoreLayerCollision(0, 18);
    //    myTransform = transform;                            // sets myTransform to this GameObject.transform
    //    destinationPosition = myTransform.position;
    //}
    void Update()
    {
        // keep track of the distance between this gameObject and destinationPosition
        destinationDistance = Vector3.Distance(destinationPosition, myTransform.position);

        if (destinationDistance < .7f)
        {       // To prevent shakin behavior when near destination
            moveSpeed = 0;
        }
        else if (destinationDistance > .5f)
        {           // To Reset Speed to default
            moveSpeed = 5;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Plane playerPlane = new Plane(Vector3.up, myTransform.position);
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f, LayerMask.GetMask("Environment")))
            {
                //Vector3 targetPoint = ray.GetPoint(hit);
                destinationPosition = hit.point;
                Quaternion targetRotation = Quaternion.LookRotation(destinationPosition - transform.position);
                myTransform.rotation = targetRotation;
            }
        }
        if (destinationDistance > .5f)
        {
            myTransform.position = Vector3.MoveTowards(myTransform.position, destinationPosition, moveSpeed * Time.deltaTime);
        }
    }
}
