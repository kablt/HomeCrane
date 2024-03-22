using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneRightMove : MonoBehaviour
{
    public GameObject CraneBody; // ������ ũ���� body
    public GameObject CraneHoist; // ������ ũ���� hosit
    public GameObject CraneLift; // ������ ũ���� lift
    public Transform LiftRollBack; // ����Ʈ�� ����ҋ��� y�� ��ġ
    CraneSkidNumManager craneskidnummanager; //pointcoil�� ��ġ ������ ���� �ش罺ũ��Ʈ�� �迭����
    public Transform PointCoil;// ������ ��ġ
    public float moveSpeed = 3f;
    public float downSpeed = 3f;
    public bool moveStatus = true;
    public bool downStatus = true;
    enum CraneStatus
    {
        Idle,//����Ʈ �ö��ִ»���
        MoveCoil,//������ ��ġ�� �̵��ϴ� �Լ� (x,z)����.
        LiftDown,//����Ʈ ������ ����
        MoveTruck, //����Ʈ�� �޸� ������ Ʈ������ �ű�°�
    }
    public void boolchange()
    {
        moveStatus = false;
    }
    CraneStatus cranstatus;
    // Start is called before the first frame update
    void Start()
    {
        craneskidnummanager = gameObject.GetComponent<CraneSkidNumManager>();
        cranstatus = CraneStatus.Idle;
        PointCoil = craneskidnummanager.skid[0];
    }
    void Update()
    {
        switch (cranstatus)
        {
            case CraneStatus.Idle:
                StartCoroutine(IdleStatusLift());
                break;
            case CraneStatus.MoveCoil:
                StartCoroutine(MovePoint());
                break;

            default:
                Debug.Log("������ �߻��߽��ϴ�");
                break;
        }
    }
    IEnumerator IdleStatusLift()
    {
        float distanceY = Mathf.Abs(CraneLift.transform.position.y - PointCoil.position.y);
       
        if (distanceY > 0.1f && moveStatus)
        {
            Debug.Log("���̵�����");
            Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
            CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            Debug.Log("�Ʒ���");
            moveStatus = false;
            downStatus = false;
            StopAllCoroutines();
            cranstatus = CraneStatus.MoveCoil;

        }
           
      
    }

    IEnumerator MovePoint()
    {
        Debug.Log("��������Ʈ �ڷ�ƾ Ȯ��");
        downSpeed = 4f;
        // Debug.Log("MovePoint�� �Ѿ�Դ�.");
        yield return new WaitForSeconds(1f);
        // Debug.Log("����Ʈ�������� �ű�� �Լ��� ���۵Ǵ� �κ��̴�.");
        Vector3 targetpositionX = new Vector3(PointCoil.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, downSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointCoil.position.z);
        CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, downSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);



        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, PointCoil.position.y, CraneLift.transform.position.z);
        float distance = Vector3.Distance(CraneLift.transform.position, targetPositionY);
        if (distance > 0.01f && downStatus == false)
        {
            Debug.Log("�������°� Ȯ�ο�");
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, downSpeed * Time.deltaTime);
            yield return null;
            distance = Vector3.Distance(CraneLift.transform.position, targetPositionY);
        }
        yield return new WaitForSeconds(2f);
        //�ش���ġ ������ �� , ������ ��ġ�� ����Ʈ�� Ư����ġ�� ������Ʈ�ǰ��ִ� ��Ȳ���� ����Ʈ�� �������� �ż��� ����. �������µ��� ��Ű���� Ư���κа� �浹�� ������ ��ġ�� ����Ʈ�� Ư�� ��ġ�� ������Ʈ �Ǵ� �Լ� ����. 
        //������ ��ġ�� ��Ű���� Ư����ġ�� �ű�� �Լ� ���� ������ �ش� ��ġ�� �������� ��ó�� ���̰� �����
    }
    // Update is called once per frame
    
}
