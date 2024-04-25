using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;


public class SimpleStaticAgent : MonoBehaviour
{
    Transform target;
    NavMeshAgent nmAgent;
    RailCoilValue TotalCoil;
    RailCoildata coil;
    public Transform originPos; // 기본대기위치
    public Transform LeftSkid; // 카트의 왼쪽 스키드 위치
    public Transform CartLeftPos; // 카트의 왼쪽 앞 위치
    public Transform PickCoil; // 코일을 들었을떄의 코일위치
    int index = 0;
    public bool CartPosBool;// 카트가 지정된 위치에 있는가
    bool idle;
    bool movecoil;
    bool movecart;
    bool pickup;
    bool pickdown;

    public GameObject RailSkid;

    enum Status
    {
        Idle, // 옮길 물건이 없으면 지정된 위치에서 대기
        SearchMove,//물건 검색후 물건앞으로 이동(목적지 탐색, 이동 , 코일방향으로 회전 드는 모션 , 카트 위치로 이동)
        Pickup,
        CartMove,
        PickDown
        
    }
    Status st;
    // Start is called before the first frame update
    void Start()
    {

        idle = true;
        CartPosBool = true;
        pickup= false;
        pickdown= false;
        movecart= false;
         nmAgent = GetComponent<NavMeshAgent>();
         TotalCoil = RailSkid.GetComponent<RailCoilValue>();
         st = Status.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch(st)
        {
            case Status.Idle:
                if (idle)
                {IdlePos(); }              
                break;
            case Status.SearchMove:
                if (movecoil)
                { OnAnimatorMove(); }
                if (target != null && Vector3.Distance(transform.position, target.position) < 1)
                {
                    Debug.Log("pickup상태변환 체크");
                    st = Status.Pickup;
                }
                break;
            case Status.Pickup:
                PickUpCoil();
                break;
            case Status.CartMove:
                if(movecart)
                {MoveCart();}
                if (target != null&& CartPosBool && Vector3.Distance(transform.position, CartLeftPos.position) < 1)
                {                
                    Debug.Log("코일 내려놓는 조건문 입장 확인");
                    st = Status.PickDown;
                }
                break;
            case Status.PickDown: 
                if(pickdown)
                {DownCoil();}
                break;
        }
    }

    void IdlePos()
    {
        if(index ==19)
        {
            idle = false;
        }
        if (index > 18 && !idle)
        {
            Debug.Log("아이들포지에서 처리함");
            nmAgent.Stop();
            nmAgent.SetDestination(originPos.position);
            nmAgent.Resume();
        }
        if(!pickup && !pickdown && !movecart && idle)
        {
            idle = false;
            pickup = true;
            pickdown = true;
            movecart = true;
            movecoil = true;
            st = Status.SearchMove;
        }
    }
    void OnAnimatorMove()// 1
    {
        if(index >19)
        {
            Debug.Log("애니메이터에서 처리함");
            nmAgent.SetDestination(originPos.position);
        }
        else
        {
            Debug.Log("애니메이터므브확인");
            movecoil = false;
            coil = TotalCoil.Sendcoil[index].GetComponent<RailCoildata>();
            if(coil.pickup == true)
            {
               nmAgent.Resume(); // 기본 SetDestination함수를 다시 실행시켜도 작동하지않음. resume를 사용해서 재사용해주어야함
               target= coil.targetPosition;
               nmAgent.SetDestination(target.position);
               coil.pickup = false;
            }
            else
            {
                index++;
                OnAnimatorMove();
            }

        }
    }

    void PickUpCoil()//2
    {
        if (pickup) 
        {
        nmAgent.Stop();
        GameObject moveCoil;
        moveCoil = TotalCoil.Sendcoil[index];
        moveCoil.transform.position = PickCoil.transform.position;
        moveCoil.transform.SetParent(PickCoil);
        pickup = false;
        }
        if(!pickup)
        {
        st = Status.CartMove;
        }
    }
    void MoveCart()//3
    {
        movecart = false;
        nmAgent.SetDestination(CartLeftPos.position);
        nmAgent.Resume();
    }

    void DownCoil()//4
    {
        pickdown = false;
        Vector3 CoilRotation = new Vector3(0f, -90f, 90f);
        nmAgent.Stop();
        GameObject moveCoil;
        moveCoil = TotalCoil.Sendcoil[index];
        moveCoil.transform.position = LeftSkid.transform.position;
        moveCoil.transform.eulerAngles = CoilRotation;
        moveCoil.transform.SetParent(LeftSkid);    
        idle = true;
        st = Status.Idle;
    }

    
}
