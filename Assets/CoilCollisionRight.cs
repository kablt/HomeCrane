using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CoilCollisionRight : MonoBehaviour

{
    public GameObject CraneManager; //���Ϲ��� ��ũ��Ʈ�� �������ִ� ���ӿ�����Ʈ
    CraneRightMove cranerightmove; // ���Ϲ��갡 �������ִ� ��ũ��Ʈ�� �����ϱ����� ����
    private GameObject coil; //�浹�� ���� ������Ʈ�� ������ ������ ���ӿ�����Ʈ
    public GameObject SkidGB;//�浹�� ��Ű���� ������ ������ ��Ű�� ������Ʈ
    CoilrightData coilrighrdata; //�浹�� ������ ��ũ��Ʈ�� �����ϱ����� ����


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
            Debug.Log("�θ�ٲٴ� �����");

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
