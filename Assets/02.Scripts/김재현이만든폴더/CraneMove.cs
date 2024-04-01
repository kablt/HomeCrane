using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CraneMove : MonoBehaviour
{
    public GameObject cranestatusUI;
    CraneStatusUI cranestatus;
    public Transform PointA; //코일이 있는 위치
    public Transform LiftRollBack; // 리프트가 대기할떄의 y축 위치
    public float moveSpeed = 0.3f; // 크레인이 움직이는 속도
    public float downSpeed = 0.3f; // 크레인 내려가는 속도
    public GameObject CraneBody; // 움직일 크레인 body
    public GameObject CraneHoist; // 움직일 크레인 hosit
    public GameObject CraneLift; // 움직일 크레인 lift
    public GameObject LayShooter; // ray를 쏘는 객체
    public CoilCollision coilcollision;
    public LayerMask CoilLayer; // 충돌할 레이어 변수;
    private float time = 3f;
    public bool LiftStatus = true;
    public bool moveStatus = true;
    public Transform PointB;
    public Transform[] SkidPositions;
    //크레인 코루틴 반복 방지
    private bool isMovingPickUpPoint = false;
    private bool isDetectingCoil = false;
    private bool isMovingToB = false;

    enum CraneStatus
    {
        Idle, // 리프트가 위로 올라와져있는 상태
        MovePickUpPoint,// 코일을 집기위한 위치로 이동
        APoint, // 코일을 줍기위해 트럭이 들어오는 위치로 이동
        Detected, // 리프트를 내려서 코일을 집는 함수
        CoilMove, // Coil을 적절한 위치에 옮기는 함수
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
    //대기상태일떄 리프트 올려두기
    public void IdleMove()
    {
        StartCoroutine(IdleStatusLift());
    }
    public void StopIdleStatus()
    {
        StopCoroutine(IdleStatusLift());
        IdleMove();
    }
    //LayShoot스크립트에서 쓰는 함수.
    public void StatusChangeMovePickUpPopint()
    {
        StopCoroutine(IdleStatusLift());
        cranstatus = CraneStatus.MovePickUpPoint;
    }

    //잡을 코일이 없을떄 대기 상태에 들어가기 위한 코루틴
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
        //해당 객체가 null이 아니라면.(해당 객제가 있다면)
    }
    //--------------------------------------------MovePointA----------------------------------------------------------------

    //대기상태일떄 막대기에서 ray쏴서 코일이 있는지 상시 체크. 있으면 PointA로 이동
    public void MovePickUpPoint()
    {
        StartCoroutine(MovementRoutine());
    }
    //코일을 집기위해 APoint로 옮기는 함수
    IEnumerator MovementRoutine()
    {
        cranestatus.Movetruck();
        moveSpeed = 2f;
        // 리프트 y축으로 올리는거
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
        while (Vector3.Distance(CraneLift.transform.position, targetPositionY) > 0.1f)
        {
            CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return null;
        }
      

        //Hoist z축으로 움직이는거
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointA.position.z);
        while (Vector3.Distance(CraneHoist.transform.position, targetpositionZ) > 0.1f)
        {
            CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
            yield return null;
        }
       

        // Body를 x축으로 움직이기
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
    //ray에 Coil태그가 있을때 해당 코일을 집는 코드
    public void CraneDetectedCoil()
    {
        StopCoroutine(MovementRoutine());
       // Debug.Log("두번쨰 case 함수로 잘넘어왔다.");
        StartCoroutine(DetectCoil());
        //리프트의 특정 지점과 코일이 충돌시 코일의 위치를 리프트의 특정위치로 업데이트하는 함수만들기.
    }
    //충돌스크립트에서 쓰는 함수

    IEnumerator DetectCoil()
    {
        downSpeed = 2f;
        // 목표 위치
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, PointA.position.y, CraneLift.transform.position.z);

        // 목표 위치에 도달할 때까지 이동
        while (Vector3.Distance(CraneLift.transform.position, targetPositionY) > 0.01f && LiftStatus == true)
        {
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, downSpeed * Time.deltaTime);
            yield return null;
        }

        // 대기 시간
       

        // 올라가는 동작
        if (downSpeed == 0f && cranstatus != CraneStatus.CoilMove)
        {
            // 올라가는 위치
            Vector3 targetPositionYT = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);

            // 목표 위치에 도달할 때까지 이동
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

    //스키드로 옮기는 코루틴 실핼하는 함수
    public void MovementPointB()
    {
        LiftStatus = true;
        StopCoroutine(DetectCoil());
        StartCoroutine(MovePoint());
    }
    public void StopMovePoint()
    {
        //Debug.Log("스키드 충돌후 idle 상태로 가는 코드");
        StopCoroutine(MovePoint());
        cranstatus = CraneStatus.Idle;
    }

    //코일 충돌후 위치가 리프트로 업데이트 디고 있을떄 목표지점으로 이동하는 함수
    IEnumerator MovePoint()
    {
        cranestatus.MoveCoil();
        InitializePointB();

        // Body를 x축으로 이동
        Vector3 targetpositionX = new Vector3(PointB.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        while (Vector3.Distance(CraneBody.transform.position, targetpositionX) > 0.01f)
        {
            CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Hoist를 z축으로 이동
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, PointB.position.z);
        while (Vector3.Distance(CraneHoist.transform.position, targetpositionZ) > 0.01f)
        {
            CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Lift를 y축으로 이동
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, PointB.position.y, CraneLift.transform.position.z);
        while (Vector3.Distance(CraneLift.transform.position, targetPositionY) > 0.01f && moveStatus == true)
        {
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMovingToB = false;

        // 해당 위치 도착 후 처리할 내용 추가
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
            //Debug.Log("등록되어있지않은 코일입니다");
        }
    }
}
