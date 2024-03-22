using UnityEngine;

public class CraneSkidNumManager : MonoBehaviour
{
    public Transform[] skid;
    public GameObject csvmanager;
    CSVDataLoader csvdataLoder;
    SkidBool skidbool;


    // Start is called before the first frame update
    void Start()
    {
        csvdataLoder = csvmanager.GetComponent<CSVDataLoader>();
        InitializePirorNum();
        SortSkidsByCode();
    }

    void InitializePirorNum()
    {
        int[] availableNumbers = new int[skid.Length];
        for (int i = 0; i < availableNumbers.Length; i++)
        {
            availableNumbers[i] = i; // Fill the array with numbers from 0 to skid.Length - 1
        }

        // Shuffle the array
        for (int i = 0; i < availableNumbers.Length; i++)
        {
            int temp = availableNumbers[i];
            int randomIndex = Random.Range(i, availableNumbers.Length);
            availableNumbers[i] = availableNumbers[randomIndex];
            availableNumbers[randomIndex] = temp;
        }

        // Assign the shuffled numbers to PirorNum
        for (int i = 0; i < skid.Length; i++)
        {
            skid[i].GetComponent<SkidBool>().PirorNum = availableNumbers[i];
        }

    }

    public void SortSkidsByCode()
    {
        // Create a custom sorting algorithm based on the code values
        for (int i = 0; i < skid.Length - 1; i++)
        {
            for (int j = i + 1; j < skid.Length; j++)
            {
                float codeI = skid[i].GetComponent<SkidBool>().PirorNum;
                float codeJ = skid[j].GetComponent<SkidBool>().PirorNum;

                if (codeI > codeJ)
                {
                    // Swap the skids
                    Transform temp = skid[i];
                    skid[i] = skid[j];
                    skid[j] = temp;
                }
            }
        }
    }

    void Update()
    {

    }
}
