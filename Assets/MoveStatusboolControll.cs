using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStatusboolControll : MonoBehaviour
{
    public GameObject cranerightmanager;
    CraneRightMove crm;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ʈ�������� ���̵� �Լ����� ���Ͻ�Ű��� �̵��ϴ����� �޼��� ���� �Լ� Ȯ�� �����");
        if(other.CompareTag("Truck"))
        {
            crm.moveStatus = false;
            crm.downStatus = false;
        }
    }


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
