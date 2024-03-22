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
    public Transform TruckSkid; //Ʈ���� ��Ű�� ��ġ(������ �밡�ٷ� ã�Ƽ� �ֱ�)
    CraneSkidNumManager craneskidnummanager; //pointcoil�� ��ġ ������ ���� �ش罺ũ��Ʈ�� �迭����
    public Transform PointCoil;// ������ ��ġ
    public float moveSpeed = 3f;
    public float downSpeed = 3f;
    public bool moveStatus = true;// ���� �ø������� ����
    public bool downStatus = true;// ��������Ʈ���� ����Ʈ ������ ����
    public bool movetruckstatus = true; // Ʈ������ �ű�°� �����ϱ����� ����
    enum CraneStatus
    {
        Idle,//����Ʈ �ö��ִ»���
        MoveCoil,//������ ��ġ�� �̵��ϴ� �Լ� (x,z)����.
        MoveTruck, //����Ʈ�� �޸� ������ Ʈ������ �ű�°�
        LiftDown// Ʈ���� ��Ű����ġ�� ���������� ����Ʈ�� ������ �Լ�. Ʈ���� ��Ű��� �浹�� ��� ���·� ��ȯ.
    }
    // ��� ----- Ʈ���� ���´�. ������ �޼��Ͽ� �����¿��� ���º�ȯ�� ���� ���������°ɷ� ���� ----------- ������ ���� Ʈ���� ��Ű�� ��ġ�� �̵� ---- Ʈ���ǽ�Ű�� ��ġ���� ����Ʈ ������ �Լ� �۵� ------ ��� ���·� ��ȯ
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
            case CraneStatus.MoveTruck:
                StartCoroutine(MoveTruckSkid());
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
    //------------------------------------------------------����Ʈ ���� �÷��� ����ϴ� ����. (���ǿ� ���� ���,�������ִ½�Ű���̵�,Ʈ���ǽ�Ű���̵����� ������ȯ)------------------------------------
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
    }
    //======================================�켱������ ���� ���� ������ �ִ� ��Ű��� �����̴� ���� �ڷ�ƾ======================================================
    IEnumerator MoveTruckSkid()
    {
        downSpeed = 4f;
        // ����Ʈ y������ �ø��°�
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
        CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
        //Debug.Log("Move1");
        yield return new WaitForSeconds(1f);
        //Hoist z������ �����̴°�
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, TruckSkid.position.z);
        CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
        //Debug.Log("Move2");
        yield return new WaitForSeconds(1f);
        // Body�� x������ �����̱�
        Vector3 targetpositionX = new Vector3(TruckSkid.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
        //Debug.Log("Move3");
        yield return new WaitForSeconds(1f);
  
    }

}
