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
    public int skidarraryindex = 0;
    public bool moveStatus = true;// ���� �ø������� ����  A
    public bool downStatus = true;// ��������Ʈ���� ����Ʈ ������ ���� B
    public bool arrayindex = true;

    enum CraneStatus
    {
        Idle,//����Ʈ �ö��ִ»���
        MoveCoil,//������ ��ġ�� �̵��ϴ� �Լ� (x,z)����.
        MoveTruck, //����Ʈ�� �޸� ������ Ʈ������ �ű�°�
        LiftDown// Ʈ���� ��Ű����ġ�� ���������� ����Ʈ�� ������ �Լ�. Ʈ���� ��Ű��� �浹�� ��� ���·� ��ȯ.
    }
    // ��� ----- Ʈ���� ���´�. ������ �޼��Ͽ� �����¿��� ���º�ȯ�� ���� ���������°ɷ� ���� ----------- ������ ���� Ʈ���� ��Ű�� ��ġ�� �̵� ---- Ʈ���ǽ�Ű�� ��ġ���� ����Ʈ ������ �Լ� �۵� ------ ��� ���·� ��ȯ
    public void boolchangeA()
    {
        moveStatus = false;
    }
    public void boolchangeB()
    {
        downStatus = false;
    }
    CraneStatus cranestatus;
    // Start is called before the first frame update
    void Start()
    {
        craneskidnummanager = gameObject.GetComponent<CraneSkidNumManager>();
        cranestatus = CraneStatus.Idle;
        
    }
    void Update()
    {
        PointCoil = craneskidnummanager.skid[skidarraryindex];
        switch (cranestatus)
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
            case CraneStatus.LiftDown:
                StartCoroutine(DownLift());
                break;
            default:
                Debug.Log("������ �߻��߽��ϴ�");
                break;
        }
    }

    public void ChangeIdle()
    {
        cranestatus = CraneStatus.Idle;
    }
    IEnumerator IdleStatusLift()
    {
        float distanceY = Mathf.Abs(CraneLift.transform.position.y - PointCoil.position.y);
       
        if (distanceY > 0.1f && moveStatus)
        {
            Debug.Log("���̵�����");
            Debug.Log(moveStatus);
            Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
            CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(1f);
        }
        if(distanceY < 0.1f && !arrayindex)
        {
            Debug.Log("������ũ���γ�");
            StopAllCoroutines();
        }
        if (!moveStatus && !downStatus && arrayindex)
        {
            Debug.Log("�Ʒ���");
            StopAllCoroutines();
            cranestatus = CraneStatus.MoveCoil;
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

    public void PointToTruck()
    {
        Debug.Log("����Ʈ��Ʈ�����γѾ�Դ���Ȯ��");
        StopAllCoroutines();
        cranestatus = CraneStatus.MoveTruck;
    }
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
        float distanceX = Mathf.Abs(CraneBody.transform.position.x - TruckSkid.position.x);
        if (distanceX < 1)
        {
            cranestatus = CraneStatus.LiftDown;
            StopCoroutine(MoveTruckSkid());
        }

    }
    //------------------------------------------����Ʈ�ٿ�-------------------------------------------------

    IEnumerator DownLift()
    {
        // Debug.Log("����Ʈ ����������");
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, TruckSkid.position.y, CraneLift.transform.position.z);
        float distance = Vector3.Distance(CraneLift.transform.position, targetPositionY);
        if (distance > 0.01f)
        {
            Debug.Log("���⵵ �ݺ��ǰ��ִ°�");
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, downSpeed * Time.deltaTime);
            yield return null;
            distance = Vector3.Distance(CraneLift.transform.position, targetPositionY);
        }
        yield return new WaitForSeconds(1f);
    }
}
