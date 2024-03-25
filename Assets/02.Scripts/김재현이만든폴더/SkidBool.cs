using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SkidBool : MonoBehaviour
{
    public bool SkidUse = true;
    public float num, weight, width, iod, rece, send;
    GameObject coil;
    CoilrightData coildata;
    public int PirorNum;
    public GameObject craneright;
    CSVDataLoader cm;


    // Start is called before the first frame update
    void Awake()
    {
        cm = craneright.GetComponent<CSVDataLoader>();
    }
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coil"))
        {
            coil = other.gameObject;
            coildata = coil.GetComponent<CoilrightData>();
            coildata.coilrightcode = PirorNum;
            num = coildata.CoilID2;
            weight = coildata.CoilWeight2;
            width = coildata.CoilWidth2;
            iod = coildata.CoilOD2;
            rece = coildata.CoilReceiveOrder2;
            send = coildata.CoilSendOrder2;

        }
    }


    void Update()
    {

    }

}