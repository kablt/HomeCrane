using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraneMove : MonoBehaviour
{
    public Transform PointA; //������ �ִ� ��ġ
    public Transform PointB; //������ ���� ���� ��ġ
    public Transform LiftRollBack; // ����Ʈ�� ����ҋ��� y�� ��ġ
    public float moveSpeed = 3f; // ũ������ �����̴� �ӵ�
    public float downSpeed = 3f; // ũ���� �������� �ӵ�
    public GameObject CraneBody; // ������ ũ���� body
    public GameObject CraneHoist; // ������ ũ���� hosit
    public GameObject CraneLift; // ������ ũ���� lift
    public GameObject LayShooter; // ray�� ��� ��ü
    public LayerMask CoilLayer; // �浹�� ���̾� ����;
    public bool LiftStatus = true;

    enum CraneStatus
    {
        Idle, // ����Ʈ�� ���� �ö�����ִ� ����
        MovePickUpPoint,// ������ �������� ��ġ�� �̵�
        APoint, // ������ �ݱ����� Ʈ���� ������ ��ġ�� �̵�
        Detected, // ����Ʈ�� ������ ������ ���� �Լ�
        CoilMove, // Coil�� ������ ��ġ�� �ű�� �Լ�
    }
    CraneStatus cranstatus;
    void Start()
    {
        cranstatus = CraneStatus.Idle;
    }
    void Update()
    {
        switch (cranstatus)
        {
            case CraneStatus.Idle:
                IdleMove();
                break;
            case CraneStatus.MovePickUpPoint:
                MovePickUpPoint();
                break;
            case CraneStatus.Detected:
                CraneDetectedCoil();
                break;
            case CraneStatus.CoilMove:
                MovementPointB();
                break;
            default:
                Debug.Log("������ �߻��߽��ϴ�");
                break;
        }
    }

    //�������ϋ� ����⿡�� ray���� ������ �ִ��� ��� üũ. ������ PointA�� �̵�
    void MovePickUpPoint()
    {
        StartCoroutine(MovementRoutine());
    }

    //ray�� Coil�±װ� ������ �ش� ������ ���� �ڵ�
    void CraneDetectedCoil()
    {
        StopCoroutine(MovementRoutine());
        Debug.Log("�ι��� case �Լ��� �߳Ѿ�Դ�.");
        StartCoroutine(DetectCoil());
        //����Ʈ�� Ư�� ������ ������ �浹�� ������ ��ġ�� ����Ʈ�� Ư����ġ�� ������Ʈ�ϴ� �Լ������.
    }

    void MovementPointB()
    {
        StartCoroutine(MovePoint());
    }

    void IdleMove()
    {
        StartCoroutine(IdleStatus());
    }

    IEnumerator IdleStatus()
    {
        Debug.Log("�������ڷ�ƾ");
        // LayShooter�� �����ʺz�� �� �ִ� ��ũ��Ʈ LayShoot�� �����Ѵ�.
        LayShoot objectBShooter = LayShooter.GetComponent<LayShoot>();
        //�ش� ��ü�� null�� �ƴ϶��.(�ش� ������ �ִٸ�)
        if (objectBShooter != null)
        {
            // ��ü���� ShootAndCheckForCoil�Լ��� �����Ѵ�.(�ش� ��ü���� ���̸� ���  Tag�� Coil�ΰ��� �����ϴ� ����׸� ����Լ�)
            objectBShooter.ShootAndCheckForCoil();
            //ũ������ ��ġ�� ������ �������� ��ġ�� �ű�� �Լ�
        }
        else
        {
            Debug.LogError("���� �߸��Ǿ��ٴ� ����");
        }
        yield return null;
    }

    //������ �������� APoint�� �ű�� �Լ�
    IEnumerator MovementRoutine()
    {
        // ����Ʈ y������ �ø��°�
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
        CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
        Debug.Log("Move1");
        yield return new WaitForSeconds(1f);
        //Hoist z������ �����̴°�
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointA.position.z);
        CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
        Debug.Log("Move2");
        yield return new WaitForSeconds(1f);
        // Body�� x������ �����̱�
        Vector3 targetpositionX = new Vector3(PointA.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
        Debug.Log("Move3");
        yield return new WaitForSeconds(1f);
        cranstatus = CraneStatus.Detected;

    }


    IEnumerator DetectCoil()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("�ι��� case �Լ����� �ڷ�ƾ�� �ߵ� ���״�..");
        StopCoroutine(MovementRoutine());
        yield return new WaitForSeconds(1f);
        StartCoroutine(DownLift());
        if (LiftStatus == false)
        {
            Debug.Log(LiftStatus);
            //if ���ϰ� �浹�� ������ ����Ʈ�� ��ġ�� ������Ʈ�Ǵ� ���� ����.     
            Debug.Log("���� ��ġ�� ���� ����Ʈ�� ������������ ������Ʈ��");
            yield return new WaitForSeconds(1f);
            Debug.Log("����Ʈ �ٿ��� ���߰� �ٽ� �ö󰡾ߵ�");
            Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
            CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            //�浹�� �ϰ��� ���߰� �ٽ� �ö�
            yield return new WaitForSeconds(1f);
            cranstatus = CraneStatus.CoilMove;
        }
    }


    IEnumerator DownLift()
    {

        Debug.Log("�ٿ��Ʈ�� �ݺ��� ����");
        downSpeed = 2f;
        Vector3 dir = new Vector3(0, -1f, 0); // Direction (downward)

        CraneLift.transform.position += dir * downSpeed * Time.deltaTime;
        yield return new WaitForSeconds(1f);

    }

    public void StopLift()
    {
        StopCoroutine(DownLift());
    }

    public void StatusChangeMovePickUpPopint()
    {
        StopCoroutine(IdleStatus());
        cranstatus = CraneStatus.MovePickUpPoint;
    }
    public void StopMovePoint()
    {
        Debug.Log("��Ű�� �浹�� idle ���·� ���� �ڵ�");
        StopCoroutine(MovePoint());
        cranstatus = CraneStatus.Idle;
    }

    //���� �浹�� ��ġ�� ����Ʈ�� ������Ʈ ��� ������ ��ǥ�������� �̵��ϴ� �Լ�
    IEnumerator MovePoint()
    {
        Debug.Log("MovePoint�� �Ѿ�Դ�.");
        yield return new WaitForSeconds(2f);
        StopCoroutine(DetectCoil());
        LiftStatus = true;
        yield return new WaitForSeconds(2f);
        Debug.Log("����Ʈ�������� �ű�� �Լ��� ���۵Ǵ� �κ��̴�.");
        Vector3 targetpositionX = new Vector3(PointB.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointB.position.z);
        CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, PointB.position.y, CraneLift.transform.position.z);
        CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        //�ش���ġ ������ �� , ������ ��ġ�� ����Ʈ�� Ư����ġ�� ������Ʈ�ǰ��ִ� ��Ȳ���� ����Ʈ�� �������� �ż��� ����. �������µ��� ��Ű���� Ư���κа� �浹�� ������ ��ġ�� ����Ʈ�� Ư�� ��ġ�� ������Ʈ �Ǵ� �Լ� ����. 
        //������ ��ġ�� ��Ű���� Ư����ġ�� �ű�� �Լ� ���� ������ �ش� ��ġ�� �������� ��ó�� ���̰� �����
    }
}
