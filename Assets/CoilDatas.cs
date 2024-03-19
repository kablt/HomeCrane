using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilDatas : MonoBehaviour
{
    public GameObject csvmanager;
    CSVDataLoader csvloader;
    public float Number;
    // Start is called before the first frame update
    void Start()
    {
        csvloader = csvmanager.GetComponent<CSVDataLoader>();
        Number = csvloader.coilDatas[6].CoilID;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
