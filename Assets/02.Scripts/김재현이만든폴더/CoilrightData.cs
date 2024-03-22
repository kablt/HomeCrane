using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilrightData : MonoBehaviour
{
    public int coilrightcode;
    public float CoilID2, CoilWeight2, CoilWidth2, CoilOD2; // 이제 이 필드들은 float 타입입니다.
    public float CoilReceiveOrder2, CoilSendOrder2;
    public GameObject CsvLoader;
    CSVDataLoader csvDataloader;
    // Start is called before the first frame update
    void Start()
    {
        csvDataloader = CsvLoader.GetComponent<CSVDataLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        CoilID2 = csvDataloader.coilDatas[coilrightcode].CoilID;
        CoilWeight2 = csvDataloader.coilDatas[coilrightcode].CoilWeight;
        CoilWidth2 = csvDataloader.coilDatas[coilrightcode].CoilWidth;
        CoilOD2 = csvDataloader.coilDatas[coilrightcode].CoilOD;
        CoilReceiveOrder2 = csvDataloader.coilDatas[coilrightcode].CoilReceiveOrder;
        CoilSendOrder2 = csvDataloader.coilDatas[coilrightcode].CoilSendOrder;

    }
}
