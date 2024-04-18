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
        Idle, // �ű� ������ ������ ������ ��ġ���� ���
        SearchMove,//���� �˻��� ���Ǿ����� �̵�(������ Ž��, �̵� , ���Ϲ������� ȸ�� ��� ��� , īƮ ��ġ�� �̵�)
        
        
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
            Debug.Log("���̻� �ű� ������ ���������ʽ��ϴ�");
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
