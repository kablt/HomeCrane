using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStatusboolControll : MonoBehaviour
{
    public GameObject cranerightmanager;
    CraneRightMove crm;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트럭감지시 아이들 함수에서 코일스키드로 이동하는조거 달성을 위한 함수 확인 디버그");
        if(other.CompareTag("Truck"))
        {
            crm.moveStatus = false;
            crm.downStatus = false;
        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Truck"))
        {
            crm.moveStatus = true;
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        crm = cranerightmanager.GetComponent<CraneRightMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
