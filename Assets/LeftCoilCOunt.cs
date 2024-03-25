using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeftCoilCOunt : MonoBehaviour
{
    public TextMeshProUGUI mine;
    public int count = 0;
    public GameObject mainpanel;
    UiCraneCoildata uiright;

    // Start is called before the first frame update
    void Start()
    {
        mainpanel = GameObject.Find("MainPanelCraneleft");
        uiright = mainpanel.GetComponent<UiCraneCoildata>();
    }

    // Update is called once per frame
    void Update()
    {
        count = uiright.count;
        mine.text = $"Coil : {count}/20";
    }

}
