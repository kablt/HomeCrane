using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoilCount : MonoBehaviour
{
    public GameObject mainpaenl;
    public TextMeshProUGUI mine;
    public int coilcount;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        mine.text = $"Coil : {coilcount}/20";
    }

    public void setdata()
    {
       
    }
}
