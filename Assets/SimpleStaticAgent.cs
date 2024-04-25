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
    public Transform originPos; // �⺻�����ġ
    public Transform LeftSkid; // īƮ�� ���� ��Ű�� ��ġ
    public Transform CartLeftPos; // īƮ�� ���� �� ��ġ
    public Transform PickCoil; // ������ ��������� ������ġ
    int index = 0;
    public bool CartPosBool;// īƮ�� ������ ��ġ�� �ִ°�
    bool idle;
    bool movecoil;
    bool movecart;
    bool pickup;
    bool pickdown;

    public GameObject RailSkid;

    enum Status
    {
        Idle, // �ű� ������ ������ ������ ��ġ���� ���
        SearchMove,//���� �˻��� ���Ǿ����� �̵�(������ Ž��, �̵� , ���Ϲ������� ȸ�� ��� ��� , īƮ ��ġ�� �̵�)
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
                { IdlePos(); }              
                break;
            case Status.SearchMove:
                if (movecoil)
                { OnAnimatorMove(); }
                if (target != null && Vector3.Distance(transform.position, target.position) < 1)
                {
                    Debug.Log("pickup���º�ȯ üũ");
                    st = Status.Pickup;
                }
                break;
            case Status.Pickup:
                PickUpCoil();
                break;
            case Status.CartMove:
                if(movecart)
                {MoveCart();}
                if (target != null&& CartPosBool)
                {
                    Debug.Log("�κ����� �������µ����");
                    if (Vector3.Distance(transform.position, CartLeftPos.position) < 1)
                    {
                        Debug.Log("���� �������� ���ǹ� ���� Ȯ��");
                        st = Status.PickDown;
                    }
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
        if(index > 19 && !idle)
        {
            Debug.Log("���̵��������� ó����");
        nmAgent.SetDestination(originPos.position);
        }
        if(!pickup && !pickdown && !movecart)
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
            Debug.Log("�ִϸ����Ϳ��� ó����");
            nmAgent.SetDestination(originPos.position);
        }
        Debug.Log("�ִϸ����͹Ǻ�Ȯ��");
        movecoil = false;
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
        if(index < 19)
        {
        idle = true;
        }
        st = Status.Idle;
    }

    
}
