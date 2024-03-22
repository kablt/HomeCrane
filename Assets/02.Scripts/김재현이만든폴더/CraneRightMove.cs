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
    CraneSkidNumManager craneskidnummanager; //pointcoil의 위치 지정을 위해 해당스크립트의 배열접근
    public Transform PointCoil;// 코일의 위치
    public float moveSpeed = 3f;
    public float downSpeed = 3f;
    public bool moveStatus = true;
    public bool downStatus = true;
    enum CraneStatus
    {
        Idle,//리프트 올라가있는상태
        MoveCoil,//코일을 위치로 이동하는 함수 (x,z)값만.
        LiftDown,//리프트 내리는 상태
        MoveTruck, //리프트에 달린 코일을 트록으로 옮기는것
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
        //해당위치 도착한 후 , 코일의 위치가 리프트의 특정위치로 업데이트되고있는 상황에서 리프트가 내려가는 매서드 실행. 내려가는동안 스키드의 특정부분과 충돌시 코일의 위치가 리프트의 특정 위치로 업데이트 되는 함수 종료. 
        //코일의 위치를 스키드의 특정위치로 옮기는 함수 만들어서 코일이 해당 위치에 놓여지는 것처럼 보이게 만들기
    }
    // Update is called once per frame
    
}
