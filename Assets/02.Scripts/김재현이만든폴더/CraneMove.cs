using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CraneMove : MonoBehaviour
{
    public GameObject cranestatusUI;
    CraneStatusUI cranestatus;
    public Transform PointA; //������ �ִ� ��ġ
    public Transform LiftRollBack; // ����Ʈ�� ����ҋ��� y�� ��ġ
    public float moveSpeed = 0.3f; // ũ������ �����̴� �ӵ�
    public float downSpeed = 0.3f; // ũ���� �������� �ӵ�
    public GameObject CraneBody; // ������ ũ���� body
    public GameObject CraneHoist; // ������ ũ���� hosit
    public GameObject CraneLift; // ������ ũ���� lift
    public GameObject LayShooter; // ray�� ��� ��ü
    public CoilCollision coilcollision;
    public LayerMask CoilLayer; // �浹�� ���̾� ����;
    private float time = 3f;
    public bool LiftStatus = true;
    public bool moveStatus = true;
    public Transform PointB;
    public Transform[] SkidPositions;
    //ũ���� �ڷ�ƾ �ݺ� ����
    private bool isMovingPickUpPoint = false;
    private bool isDetectingCoil = false;
    private bool isMovingToB = false;

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
        cranestatusUI = GameObject.Find("CranePanelStatus");
        cranstatus = CraneStatus.Idle;
        cranestatus = cranestatusUI.GetComponent<CraneStatusUI>();
    }
    void Update()
{
    switch (cranstatus)
    {
        case CraneStatus.Idle:
            IdleMove();
            break;
        case CraneStatus.MovePickUpPoint:
            if (!isMovingPickUpPoint)
            {
                MovePickUpPoint();
                isMovingPickUpPoint = true;
            }
            break;
        case CraneStatus.Detected:
            if (!isDetectingCoil)
            {
                CraneDetectedCoil();
                isDetectingCoil = true;
            }
            break;
        case CraneStatus.CoilMove:
            if (!isMovingToB)
            {
                MovementPointB();
                isMovingToB = true;
            }
            break;
        default:
            Debug.Log("An error occurred");
            break;
    }
}

    //-------------------------------------------------------------IDLE------------------------------------------------------------------------
    //�������ϋ� ����Ʈ �÷��α�
    public void IdleMove()
    {
        StartCoroutine(IdleStatusLift());
    }
    public void StopIdleStatus()
    {
        StopCoroutine(IdleStatusLift());
        IdleMove();
    }
    //LayShoot��ũ��Ʈ���� ���� �Լ�.
    public void StatusChangeMovePickUpPopint()
    {
        StopCoroutine(IdleStatusLift());
        cranstatus = CraneStatus.MovePickUpPoint;
    }

    //���� ������ ������ ��� ���¿� ���� ���� �ڷ�ƾ
    IEnumerator IdleStatusLift()
    {
        cranestatus.idle();
        moveSpeed = 1f;
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
        CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
        yield return null;
        float timer = 3;
        LayShoot objectBShooter = LayShooter.GetComponent<LayShoot>();
        if(time == timer)
        {
            objectBShooter.ShootAndCheckForCoil();
            time = 0;
        }
        else
        {
            time++;
        }
        //�ش� ��ü�� null�� �ƴ϶��.(�ش� ������ �ִٸ�)
    }
    //--------------------------------------------MovePointA----------------------------------------------------------------

    //�������ϋ� ����⿡�� ray���� ������ �ִ��� ��� üũ. ������ PointA�� �̵�
    public void MovePickUpPoint()
    {
        StartCoroutine(MovementRoutine());
    }
    //������ �������� APoint�� �ű�� �Լ�
    IEnumerator MovementRoutine()
    {
        cranestatus.Movetruck();
        moveSpeed = 2f;
        // ����Ʈ y������ �ø��°�
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
        while (Vector3.Distance(CraneLift.transform.position, targetPositionY) > 0.1f)
        {
            CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return null;
        }
      

        //Hoist z������ �����̴°�
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointA.position.z);
        while (Vector3.Distance(CraneHoist.transform.position, targetpositionZ) > 0.1f)
        {
            CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
            yield return null;
        }
       

        // Body�� x������ �����̱�
        Vector3 targetpositionX = new Vector3(PointA.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        while (Vector3.Distance(CraneBody.transform.position, targetpositionX) > 0.1f)
        {
            CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
            yield return null;
        }
        

        LiftStatus = true;
        isMovingPickUpPoint = false;
        cranstatus = CraneStatus.Detected;
    }
    //-----------------------------------------LiftDown-------------------------------------------------------------------------
    //ray�� Coil�±װ� ������ �ش� ������ ���� �ڵ�
    public void CraneDetectedCoil()
    {
        StopCoroutine(MovementRoutine());
       // Debug.Log("�ι��� case �Լ��� �߳Ѿ�Դ�.");
        StartCoroutine(DetectCoil());
        //����Ʈ�� Ư�� ������ ������ �浹�� ������ ��ġ�� ����Ʈ�� Ư����ġ�� ������Ʈ�ϴ� �Լ������.
    }
    //�浹��ũ��Ʈ���� ���� �Լ�

    IEnumerator DetectCoil()
    {
        downSpeed = 2f;
        // ��ǥ ��ġ
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, PointA.position.y, CraneLift.transform.position.z);

        // ��ǥ ��ġ�� ������ ������ �̵�
        while (Vector3.Distance(CraneLift.transform.position, targetPositionY) > 0.01f && LiftStatus == true)
        {
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, downSpeed * Time.deltaTime);
            yield return null;
        }

        // ��� �ð�
       

        // �ö󰡴� ����
        if (downSpeed == 0f && cranstatus != CraneStatus.CoilMove)
        {
            // �ö󰡴� ��ġ
            Vector3 targetPositionYT = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);

            // ��ǥ ��ġ�� ������ ������ �̵�
            while (Vector3.Distance(CraneLift.transform.position, targetPositionYT) > 0.01f)
            {
                CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionYT, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Once the lift has reached the target position, change crane status
           
            cranstatus = CraneStatus.CoilMove;
        }
        isDetectingCoil = false;
    }
    //-----------------------------------------(MoveTOSkid)-------------------------------------------------------------------------

    //��Ű��� �ű�� �ڷ�ƾ �����ϴ� �Լ�
    public void MovementPointB()
    {
        LiftStatus = true;
        StopCoroutine(DetectCoil());
        StartCoroutine(MovePoint());
    }
    public void StopMovePoint()
    {
        //Debug.Log("��Ű�� �浹�� idle ���·� ���� �ڵ�");
        StopCoroutine(MovePoint());
        cranstatus = CraneStatus.Idle;
    }

    //���� �浹�� ��ġ�� ����Ʈ�� ������Ʈ ��� ������ ��ǥ�������� �̵��ϴ� �Լ�
    IEnumerator MovePoint()
    {
        cranestatus.MoveCoil();
        InitializePointB();

        // Body�� x������ �̵�
        Vector3 targetpositionX = new Vector3(PointB.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        while (Vector3.Distance(CraneBody.transform.position, targetpositionX) > 0.01f)
        {
            CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Hoist�� z������ �̵�
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointB.position.z);
        while (Vector3.Distance(CraneHoist.transform.position, targetpositionZ) > 0.01f)
        {
            CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Lift�� y������ �̵�
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, PointB.position.y, CraneLift.transform.position.z);
        while (Vector3.Distance(CraneLift.transform.position, targetPositionY) > 0.01f && moveStatus == true)
        {
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMovingToB = false;

        // �ش� ��ġ ���� �� ó���� ���� �߰�
    }

    void InitializePointB()
    {
        float priorNum = coilcollision.CoilNumber;
        if (priorNum >= 0 && priorNum < 5)
        {
            int startIndex = 0; 
            int endIndex = 4;   

            for (int i = startIndex; i <= endIndex && i < SkidPositions.Length; i++)
            {
                Transform skidPosition = SkidPositions[i];
                SkidLeft skidBoolScript = skidPosition.GetComponent<SkidLeft>();

                // Check if the SkidBool script is attached and SkidUse is true
                if (skidBoolScript != null && skidBoolScript.SkidUse)
                {
                    // Set PointB to the current skidPosition
                    PointB = skidPosition;
                    break; // Exit the loop since we found a valid PointB
                }
            }
        }
        else if(priorNum >4 && priorNum <10)
        {
            int startIndex = 5;
            int endIndex = 9;

            for (int i = startIndex; i <= endIndex && i < SkidPositions.Length; i++)
            {
                Transform skidPosition = SkidPositions[i];
                SkidLeft skidBoolScript = skidPosition.GetComponent<SkidLeft>();

                // Check if the SkidBool script is attached and SkidUse is true
                if (skidBoolScript != null && skidBoolScript.SkidUse)
                {
                    // Set PointB to the current skidPosition
                    PointB = skidPosition;
                    break; // Exit the loop since we found a valid PointB
                }
            }
        }
        else if (priorNum > 9 && priorNum < 15)
        {
            int startIndex = 10;
            int endIndex = 14;

            for (int i = startIndex; i <= endIndex && i < SkidPositions.Length; i++)
            {
                Transform skidPosition = SkidPositions[i];
                SkidLeft skidBoolScript = skidPosition.GetComponent<SkidLeft>();

                // Check if the SkidBool script is attached and SkidUse is true
                if (skidBoolScript != null && skidBoolScript.SkidUse)
                {
                    // Set PointB to the current skidPosition
                    PointB = skidPosition;
                    break; // Exit the loop since we found a valid PointB
                }
            }
        }
        else if (priorNum > 14 && priorNum < 20)
        {
            int startIndex = 15;
            int endIndex = 19;

            for (int i = startIndex; i <= endIndex && i < SkidPositions.Length; i++)
            {
                Transform skidPosition = SkidPositions[i];
                SkidLeft skidBoolScript = skidPosition.GetComponent<SkidLeft>();

                // Check if the SkidBool script is attached and SkidUse is true
                if (skidBoolScript != null && skidBoolScript.SkidUse)
                {
                    // Set PointB to the current skidPosition
                    PointB = skidPosition;
                    break; // Exit the loop since we found a valid PointB
                }
            }
        }
        else
        {
            //Debug.Log("��ϵǾ��������� �����Դϴ�");
        }
    }
}
