using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneRightMove : MonoBehaviour
{
    public GameObject CraneBody; // 움직일 크레인 body
    public GameObject CraneHoist; // 움직일 크레인 hosit
    public GameObject CraneLift; // 움직일 크레인 lift
    public Transform LiftRollBack; // 리프트가 대기할떄의 y축 위치
    public Transform TruckSkid; //트럭의 스키드 위치(씬에서 노가다로 찾아서 넣기)
    CraneSkidNumManager craneskidnummanager; //pointcoil의 위치 지정을 위해 해당스크립트의 배열접근
    public Transform PointCoil;// 코일의 위치
    public float moveSpeed = 3f;
    public float downSpeed = 3f;
    public bool moveStatus = true;// 위로 올리기위한 조건
    public bool downStatus = true;// 무브포인트에서 리프트 내리는 조건
    public bool movetruckstatus = true; // 트럭으로 옮기는걸 관리하기위한 조건
    enum CraneStatus
    {
        Idle,//리프트 올라가있는상태
        MoveCoil,//코일을 위치로 이동하는 함수 (x,z)값만.
        MoveTruck, //리프트에 달린 코일을 트록으로 옮기는것
        LiftDown// 트럭의 스키드위치에 도달했을떄 리프트를 내리는 함수. 트럭의 스키드와 충돌시 대기 상태로 전환.
    }
    // 대기 ----- 트럭이 들어온다. 조건이 달성하여 대기상태에서 상태변환중 코일 가지러가는걸로 변경 ----------- 코일을 집고 트럭의 스키드 위치로 이동 ---- 트럭의스키드 위치에서 리프트 내리는 함수 작동 ------ 대기 상태로 전환
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
                Debug.Log("오류가 발생했습니다");
                break;
        }
    }
    IEnumerator IdleStatusLift()
    {
        float distanceY = Mathf.Abs(CraneLift.transform.position.y - PointCoil.position.y);
       
        if (distanceY > 0.1f && moveStatus)
        {
            Debug.Log("아이들위쪽");
            Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
            CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(1f);
        }
        else
        {
            Debug.Log("아래쪽");
            moveStatus = false;
            downStatus = false;
            StopAllCoroutines();
            cranstatus = CraneStatus.MoveCoil;

        }              
    }
    //------------------------------------------------------리프트 위로 올려서 대기하는 상태. (조건에 따라 대기,코일이있는스키드이동,트럭의스키드이동으로 상태전환)------------------------------------
    IEnumerator MovePoint()
    {
        Debug.Log("무브포인트 코루틴 확인");
        downSpeed = 4f;
        // Debug.Log("MovePoint로 넘어왔다.");
        yield return new WaitForSeconds(1f);
        // Debug.Log("포인트지점으로 옮기는 함수가 시작되는 부분이다.");
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
            Debug.Log("내려가는거 확인용");
            CraneLift.transform.position = Vector3.MoveTowards(CraneLift.transform.position, targetPositionY, downSpeed * Time.deltaTime);
            yield return null;
            distance = Vector3.Distance(CraneLift.transform.position, targetPositionY);
        }
        yield return new WaitForSeconds(2f);
    }
    //======================================우선순위가 제일 높은 코일이 있는 스키드로 움직이는 동작 코루틴======================================================
    IEnumerator MoveTruckSkid()
    {
        downSpeed = 4f;
        // 리프트 y축으로 올리는거
        Vector3 targetPositionY = new Vector3(CraneLift.transform.position.x, LiftRollBack.position.y, CraneLift.transform.position.z);
        CraneLift.transform.position = Vector3.Lerp(CraneLift.transform.position, targetPositionY, moveSpeed * Time.deltaTime);
        //Debug.Log("Move1");
        yield return new WaitForSeconds(1f);
        //Hoist z축으로 움직이는거
        Vector3 targetpositionZ = new Vector3(CraneHoist.transform.position.x, CraneHoist.transform.position.y, TruckSkid.position.z);
        CraneHoist.transform.position = Vector3.Lerp(CraneHoist.transform.position, targetpositionZ, moveSpeed * Time.deltaTime);
        //Debug.Log("Move2");
        yield return new WaitForSeconds(1f);
        // Body를 x축으로 움직이기
        Vector3 targetpositionX = new Vector3(TruckSkid.position.x, CraneBody.transform.position.y, CraneBody.transform.position.z);
        CraneBody.transform.position = Vector3.Lerp(CraneBody.transform.position, targetpositionX, moveSpeed * Time.deltaTime);
        //Debug.Log("Move3");
        yield return new WaitForSeconds(1f);
  
    }

}
