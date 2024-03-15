using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CoilCollision : MonoBehaviour

{
    public GameObject CraneManager;
    CraneMove cranemove;
    private GameObject coil;

    void Awake()
    {
        cranemove = CraneManager.GetComponent<CraneMove>();
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Coil")
        {
            coil = collision.gameObject;
            cranemove.StopLift();
            collision.transform.SetParent(transform);
            Debug.Log("부모바꾸는 디버그");
            cranemove.LiftStatus = false;
        }

        if (collision.gameObject.CompareTag("Skid"))
        {
            Debug.Log("Crash with Skid");
            cranemove.StopMovePoint();
            coil.transform.SetParent(collision.transform);
            coil.transform.position = collision.transform.position;
            coil.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // Function called when a collision with an object with the Skid tag has ended
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}
