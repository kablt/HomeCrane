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
    public Transform originPos; // �⺻�����ġ
    public Transform LeftSkid; // īƮ�� ���� ��Ű�� ��ġ
    public Transform CartLeftPos; // īƮ�� ���� �� ��ġ
    public Transform PickCoil; // ������ ��������� ������ġ
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
    public void OnAnimatorMove()// 1
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
           nmAgent.Resume(); // �⺻ SetDestination�Լ��� �ٽ� ������ѵ� �۵���������. resume�� ����ؼ� �������־����
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
