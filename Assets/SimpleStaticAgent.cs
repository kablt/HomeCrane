using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;


public class SimpleStaticAgent : MonoBehaviour
{
    Transform target;
    NavMeshAgent nmAgent;
    RailCoilValue TotalCoil;
    RailCoildata coil;
    public Transform originPos;
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
    public void OnAnimatorMove()
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
