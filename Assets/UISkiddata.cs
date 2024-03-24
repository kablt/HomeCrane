using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISkiddata : MonoBehaviour
{
    public TextMeshProUGUI mine;
    public TextMeshProUGUI coilno;
    public TextMeshProUGUI coilweight;
    public TextMeshProUGUI coilwidth;
    public TextMeshProUGUI coiliod;
    public TextMeshProUGUI coilrece;
    public TextMeshProUGUI coilsend;
    public GameObject Skid;
    SkidLeft skiddata;
    // Start is called before the first frame update
    void Start()
    {
        skiddata = Skid.GetComponent<SkidLeft>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(skiddata.SkidUse == true)
        {
            mine.color = Color.white;
        }
        if(skiddata.SkidUse ==false)
        {
            mine.color = Color.black;
        }
    }

    public void changedata()
    {
        coilno.text = skiddata.num.ToString();
        coilweight.text = skiddata.weight.ToString();
        coilwidth.text = skiddata.width.ToString();
        coiliod.text = skiddata.iod.ToString();
        coilrece.text = skiddata.rece.ToString();
        coilsend.text = skiddata.send.ToString();

    }
}
