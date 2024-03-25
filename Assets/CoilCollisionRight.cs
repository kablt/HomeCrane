using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CoilCollisionRight : MonoBehaviour

{
    public GameObject CraneManager; //코일무브 스크립트를 가지고있는 게임오브젝트
    CraneRightMove cranerightmove; // 코일무브가 가지고있는 스크립트를 접근하기위한 변수
    public GameObject coil; //충돌한 코일 오브젝트의 정보를 저장할 게임오브젝트
    public GameObject SkidGB;//충돌한 스키드의 정보를 저장할 스키드 오브젝트
    CoilrightData coilrighrdata; //충돌한 코일의 스크립트의 접근하기위한 변수
    public GameObject mainpanel;
    UiRightCraneCoildata uiint;
    public float CoilID2, CoilWeight2, CoilWidth2, CoilOD2, CoilReceiveOrder2, CoilSendOrder2;
    CoilCount coilcount;
    public GameObject listpanel;


    void Awake()
    {
        cranerightmove = CraneManager.GetComponent<CraneRightMove>();
        uiint = mainpanel.GetComponent<UiRightCraneCoildata>();
        coilcount = listpanel.GetComponent<CoilCount>();
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Coil")
        {
            coil = collision.gameObject;
            coilrighrdata = coil.GetComponent<CoilrightData>();  
            collision.transform.SetParent(transform);
            CoilID2 = coilrighrdata.CoilID2;
            CoilWeight2 = coilrighrdata.CoilWeight2;
            CoilWidth2 = coilrighrdata.CoilWidth2;
            CoilOD2 = coilrighrdata.CoilOD2;
            CoilReceiveOrder2 = coilrighrdata.CoilReceiveOrder2;
            CoilSendOrder2 = coilrighrdata.CoilSendOrder2;
            coilcount.coilcount--;
            cranerightmove.downStatus = true;
            cranerightmove.StopAllCoroutines();
            cranerightmove.PointToTruck();
            Debug.Log("부모바꾸는 디버그");

        }

        if (collision.gameObject.CompareTag("Skid"))
        {
            Debug.Log("Crash with Skid");
            SkidGB = collision.gameObject;
            coil.transform.SetParent(collision.transform);
            cranerightmove.StopAllCoroutines();
            cranerightmove.moveStatus = true;
            cranerightmove.skidarraryindex++;
            if(cranerightmove.skidarraryindex ==20)
            {
                cranerightmove.arrayindex = false;
                cranerightmove.skidarraryindex = 19;
            }
            cranerightmove.ChangeIdle();
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
