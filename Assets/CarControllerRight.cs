using UnityEngine;

public class CarControllerRight : MonoBehaviour
{
    public GameObject cranerightmove;
    CraneRightMove cranerightsc;
    public Transform StartPoint;
    public GameObject truck;
    public int carhow = 0;
    // �浹�� ���۵� �� ȣ��˴ϴ�.


    public void MakeCar()
    {
        
        GameObject newObject = Instantiate(truck, StartPoint);
        newObject.transform.parent = null;
        carhow++;
    }

    //0~19���� ����
    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���� ������ �������� ������Ʈ�� ExitPoint���� Ȯ���մϴ�.
        if (other.CompareTag("Truck") && carhow<20)
        {
            MakeCar();
        }
        if(carhow == 20)
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
     
    }
}
