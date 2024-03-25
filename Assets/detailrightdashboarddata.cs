using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class detailrightdashboarddata : MonoBehaviour
{
    public TextMeshProUGUI mine;
    public int num = 0;
    public GameObject Rightleft;
    CoilCollisionRight coilright;
    // Start is called before the first frame update
    void Start()
    {
        Rightleft = GameObject.Find("LiftRoot");
        coilright = Rightleft.GetComponent<CoilCollisionRight>();
    }

    // Update is called once per frame
    void Update()
    {
        if(num == 0)
        {
            mine.text = coilright.CoilID2.ToString();
        }
        if(num ==1)
        {
            mine.text =coilright.CoilWeight2.ToString();
        }
        if(num ==2)
        {
            mine.text = coilright.CoilWidth2.ToString();
        }
        if(num ==3)
        {
            mine.text = coilright.CoilOD2.ToString();
        }
        if(num ==4)
        {
            mine.text = coilright.CoilReceiveOrder2.ToString();
        }
        if(num ==5)
        {
            mine.text = coilright.CoilSendOrder2.ToString();
        }
    }
}
