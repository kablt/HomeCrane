using UnityEngine;

public class CarControllerRight : MonoBehaviour
{
    public GameObject cranerightmove;
    CraneRightMove cranerightsc;
    public bool makecar = true;
    public Transform StartPoint;
    public GameObject truck;
    // �浹�� ���۵� �� ȣ��˴ϴ�.


    public void MakeCar()
    {
        
        GameObject newObject = Instantiate(truck, StartPoint);
        newObject.transform.parent = null;
    }

    //0~19���� ����
    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���� ������ �������� ������Ʈ�� ExitPoint���� Ȯ���մϴ�.
        if (other.CompareTag("Truck") && makecar ==true)
        {
            MakeCar();
        }
        if(makecar == false)
        {
            Debug.Log("���̻� ������ ���� ���������ʽ��ϴ�");
        }

    }

    void Start()
    {
        cranerightsc = cranerightmove.GetComponent<CraneRightMove>();
        MakeCar();
    }

    void Update()
    {
       if(cranerightsc.skidarraryindex==20)
        {
            makecar = false;
        }
    }
}
