using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform StartPoint;
    public GameObject truck;
    public GameObject Colorchange;
    ColorChange colorchange;
    // �浹�� ���۵� �� ȣ��˴ϴ�.

    private int[] usedValues = new int[20]; // Array to keep track of used values
    private int currentIndex = 0; // Index to keep track of the current value

    public void MakeCar()
    {
        if (currentIndex == 20)
        {
            Debug.Log("��� ���� ���Ǿ����ϴ�.");
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

    //0~19���� ����
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
        // Ʈ���� ������ �������� ������Ʈ�� ExitPoint���� Ȯ���մϴ�.
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
