using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform StartPoint;
    public GameObject truck;
    // �浹�� ���۵� �� ȣ��˴ϴ�.

    private int[] usedValues = new int[20]; // Array to keep track of used values
    private int currentIndex = 0; // Index to keep track of the current value

    public void MakeCar()
    {
        if (currentIndex == 20)
        {
            Debug.Log("��� ���� ���Ǿ����ϴ�.");
            return; // Exit the function if all values are used
        }

        // Find an unused value for i
        int i = FindUnusedValue();

        GameObject newObject = Instantiate(truck, StartPoint);
        // Access the variable from the instantiated object
        truckTrigger component = newObject.GetComponent<truckTrigger>(); // Replace YourComponent with the actual component type
        if (component != null)
        {
            // Initialize your variable with the value of the variable held by the object
            component.truckNum = i;
            newObject.transform.parent = null;// Replace variableToAccess with the actual variable name
                                              // Now you can use variableValue3 as needed
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
            randomIndex = Random.Range(0, 20); // Generate a random index within the range of 0 to 19
        } while (usedValues[randomIndex] != 0); // Keep generating until an unused value is found

        usedValues[randomIndex] = 1; // Mark the value as used
        currentIndex++; // Increment the current index
        return randomIndex; // Return the unused value
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
        MakeCar();
    }
}
