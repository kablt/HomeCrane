using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoilCount : MonoBehaviour
{
    public GameObject mainpaenl;
    public TextMeshProUGUI mine;
    UiCraneCoildata uicr;
    // Start is called before the first frame update
    void Start()
    {
        uicr = mainpaenl.GetComponent<UiCraneCoildata>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setdata()
    {
        mine.text = $"Coil : {uicr.count}/20";
    }
}
