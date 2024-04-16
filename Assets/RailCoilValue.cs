using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCoilValue : MonoBehaviour
{
    public GameObject[] RailCoil;
    RailCoildata data;
    public GameObject csv;
    CSVDataLoader csvloader;
    public List<GameObject> Sendcoil = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        csvloader = csv.GetComponent<CSVDataLoader>();
        for(int i = 0; i<20; i++)
        {
            data = RailCoil[i].GetComponent<RailCoildata>();
            data.InCoilID = csvloader.coilDatas[i].CoilID;
            data.InCoilWeight = csvloader.coilDatas[i].CoilWeight;
            data.InCoilWidth = csvloader.coilDatas[i].CoilWidth;
            data.InCoilIOD = csvloader.coilDatas[i].CoilID;
            data.InCoilReceiveOrder = csvloader.coilDatas[i].CoilReceiveOrder;
            data.InCoilSendOrder = csvloader.coilDatas[i].CoilSendOrder;
        }

        foreach (GameObject coilObject in RailCoil)
        {
            RailCoildata data = coilObject.GetComponent<RailCoildata>();
            if (data != null)
            {
                Sendcoil.Add(coilObject);
            }
        }

        // Sendcoil 리스트를 InCoilSendOrder 값에 따라 정렬
        Sendcoil.Sort((a, b) =>
        {
            RailCoildata dataA = a.GetComponent<RailCoildata>();
            RailCoildata dataB = b.GetComponent<RailCoildata>();
            if (dataA != null && dataB != null)
            {
                return dataA.InCoilSendOrder.CompareTo(dataB.InCoilSendOrder);
            }
            return 0;
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
