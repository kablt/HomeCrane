using UnityEngine;

public class CarControllerRight : MonoBehaviour
{
    public GameObject cranerightmove;
    CraneRightMove cranerightsc;
    public GameObject Rightcolorchange;
    ColorRightChange colorright;
    public Transform StartPoint;
    public GameObject truck;
    public int carhow = 0;
    // 충돌이 시작될 때 호출됩니다.


    public void MakeCar()
    {
        
        GameObject newObject = Instantiate(truck, StartPoint);
        newObject.transform.parent = null;
        colorright.startcolor();
        carhow++;
    }

    //0~19사이 랜덤
    private void OnTriggerEnter(Collider other)
    {
        // 트리거 영역을 빠져나간 오브젝트가 ExitPoint인지 확인합니다.
        if (other.CompareTag("Truck") && carhow<20)
        {
            MakeCar();
        }
        if(carhow == 20)
        {
            Debug.Log("더이상 오른쪽 차를 생성하지않습니다");
        }

    }

    void Start()
    {
        Rightcolorchange = GameObject.Find("ColorChangeRight");
        colorright = Rightcolorchange.GetComponent<ColorRightChange>();
        cranerightsc = cranerightmove.GetComponent<CraneRightMove>();
        MakeCar();
    }

    void Update()
    {
     
    }
}
