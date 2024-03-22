using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SkidBool : MonoBehaviour
{
    public bool SkidUse = true;
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

        }
    }


    void Update()
    {

    }

}