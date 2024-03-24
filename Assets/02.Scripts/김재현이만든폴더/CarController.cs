using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform StartPoint;
    public GameObject truck;
    public GameObject Colorchange;
    ColorChange colorchange;
    // 충돌이 시작될 때 호출됩니다.

    private int[] usedValues = new int[20]; // Array to keep track of used values
    private int currentIndex = 0; // Index to keep track of the current value

    public void MakeCar()
    {
        if (currentIndex == 20)
        {
            Debug.Log("모든 값이 사용되었습니다.");
            colorchange.stopcolor();
            return; // Exit the function if all values are used
        }

        int i = FindUnusedValue();
        GameObject newObject = Instantiate(truck, StartPoint);
        truckTrigger component = newObject.GetComponent<truckTrigger>(); 
        if (component != null)
        {
            component.truckNum = i;
            newObject.transform.parent = null;
            colorchange.startcolor();
        }
        else
        {
            Debug.LogWarning("Prefab does not contain the expected component.");
        }
    }

    //0~19사이 랜덤
    private int FindUnusedValue()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, 20); 
        } 
        while (usedValues[randomIndex] != 0); 
        usedValues[randomIndex] = 1; 
        currentIndex++; 
        return randomIndex; 
    }
    private void OnTriggerEnter(Collider other)
    {
        // 트리거 영역을 빠져나간 오브젝트가 ExitPoint인지 확인합니다.
        if (other.CompareTag("Truck"))
        {
            MakeCar();
        }
    
    }

    void Start()
    {
        colorchange = Colorchange.GetComponent<ColorChange>();
        MakeCar();
    }
}
