using System.Collections;
using System.Collections.Generic;
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

    public GameObject RailSkid;

    enum Status
    {
        Idle, // 옮길 물건이 없으면 지정된 위치에서 대기
        SearchMove,//물건 검색후 물건앞으로 이동(목적지 탐색, 이동 , 코일방향으로 회전 드는 모션 , 카트 위치로 이동)
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
         nmAgent = GetComponent<NavMeshAgent>();
         TotalCoil = RailSkid.GetComponent<RailCoilValue>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAnimatorMove()// 1
    {
        if(index > 19)
        {
            target = originPos;
            nmAgent.SetDestination(target.position);
            Debug.Log("더이상 옮길 코일이 남아있지않습니다");
            return;
        }
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

    public void PickUpCoil()//2
    {
        nmAgent.Stop();
        GameObject moveCoil;
        moveCoil = TotalCoil.Sendcoil[index];
        moveCoil.transform.position = CartLeftPos.transform.position;
        moveCoil.transform.SetParent(PickCoil);
    }
    public void MoveCart()//3
    {
        nmAgent.SetDestination(CartLeftPos.position);
        nmAgent.Resume();
    }

    public void DownCoil()//4
    {
        nmAgent.Stop();
        GameObject moveCoil;
        moveCoil = TotalCoil.Sendcoil[index];
        moveCoil.transform.position = LeftSkid.transform.position;
        moveCoil.transform.SetParent(LeftSkid);
    }

    
}
