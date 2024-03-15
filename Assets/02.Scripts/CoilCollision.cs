using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CoilCollision : MonoBehaviour

{
    public GameObject coil;
    public GameObject liftRoot;
    public GameObject liftPoint;
    public GameObject CraneManager;
    CraneMove cranemove;

    void Awake()
    {
        cranemove = CraneManager.GetComponent<CraneMove>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Coil")
        {
            cranemove.StopLift();
            cranemove.LiftStatus = false;
            Debug.Log("�浹");
        }

        if (collision.gameObject.CompareTag("Skid"))
        {

            Debug.Log("��Ű��� �浹");
            cranemove.StopMovePoint();
            GetComponent<BoxCollider>().enabled = false;
            coil.GetComponent<CapsuleCollider>().enabled = true;
            coil.transform.position = collision.transform.position + Vector3.up * 2.5f;
            coil.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // Skid �±׸� ���� ������Ʈ���� �浹�� ������ �� ȣ��Ǵ� �Լ�
            DisconnectLiftPoint();

        }
    }

    IEnumerator MoveCoilAfterDelay()
    {
        coil.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(0.3f);


        // �����̰��� �ϴ� ���� �߰�
        while (true)
        {
           MoveCoilWithLiftRoot();
        }
         
    }

    void MoveCoilWithLiftRoot()
    {
        coil.transform.position = liftPoint.transform.position;

    }


    void DisconnectLiftPoint()
    {
        // liftPoint���� ������ �����ϴ� ���� �߰�
        // ���� ���, liftPoint ������ null�� �����ϰų� �ٸ� �ʱ�ȭ �۾��� ������ �� ����
        //GameObject skid = GameObject.Find("skid");
        //coil.transform.position = skid.transform.position;
        //liftPoint = null;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }
}
