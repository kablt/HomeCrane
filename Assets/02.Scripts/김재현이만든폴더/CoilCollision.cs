using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CoilCollision : MonoBehaviour

{
    public GameObject CraneManager;
    CraneMove cranemove;
    public GameObject coil;
    public GameObject SkidGB;
    public CoilDatas coildata;
    public float CoilNumber;
    SkidLeft skidLeft;
    public GameObject mainpanel;
    UiCraneCoildata uiint;

    void Awake()
    {
        cranemove = CraneManager.GetComponent<CraneMove>();
        uiint = mainpanel.GetComponent<UiCraneCoildata>();
        
    }

  

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Coil")
        {
            coil = collision.gameObject;
            coildata = coil.GetComponent<CoilDatas>();
            CoilNumber = coildata.Number;
            cranemove.LiftStatus = false;
            cranemove.moveStatus = true;
            cranemove.downSpeed = 0f;
            Debug.Log($"downspeed : {cranemove.downSpeed}");
            collision.transform.SetParent(transform);
            Debug.Log("부모바꾸는 디버그");
           
        }

        if (collision.gameObject.CompareTag("Skid"))
        {
            //코일을 스키드에 내려놓을때 값 0으로 초기화
            /* 
            coildata.InCoilID = 0;
            coildata.InCoilWeight = 0;
            coildata.InCoilWidth = 0;
            coildata.InCoilOD = 0;
            coildata.InCoilReceiveOrder = 0;
            coildata.InCoilSendOrder = 0;
            */
            Debug.Log("Crash with Skid");
            SkidGB = collision.gameObject;
            skidLeft = SkidGB.GetComponent<SkidLeft>();
            skidLeft.SkidUse = false;
            skidLeft.num = coildata.InCoilID;
            skidLeft.weight = coildata.InCoilWeight;
            skidLeft.width = coildata.InCoilWidth;
            skidLeft.iod = coildata.InCoilIOD;
            skidLeft.rece = coildata.InCoilReceiveOrder;
            skidLeft.send = coildata.InCoilSendOrder;
            cranemove.moveStatus = false;
            uiint.count++;
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
