using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CoilCollisionRight : MonoBehaviour

{
    public GameObject CraneManager; //코일무브 스크립트를 가지고있는 게임오브젝트
    CraneRightMove cranerightmove; // 코일무브가 가지고있는 스크립트를 접근하기위한 변수
    private GameObject coil; //충돌한 코일 오브젝트의 정보를 저장할 게임오브젝트
    public GameObject SkidGB;//충돌한 스키드의 정보를 저장할 스키드 오브젝트
    CoilrightData coilrighrdata; //충돌한 코일의 스크립트의 접근하기위한 변수


    void Awake()
    {
        cranerightmove = CraneManager.GetComponent<CraneRightMove>();
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Coil")
        {
            coil = collision.gameObject;
            coilrighrdata = coil.GetComponent<CoilrightData>();  
            collision.transform.SetParent(transform);
            Debug.Log("부모바꾸는 디버그");

        }

        if (collision.gameObject.CompareTag("Skid"))
        {
            Debug.Log("Crash with Skid");
            SkidGB = collision.gameObject;
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
