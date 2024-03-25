using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraneStatusUI : MonoBehaviour
{
    public TextMeshProUGUI mine;
    // Start is called before the first frame update
    void Start()
    {
        mine.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void idle()
    {
        mine.text = "대기중";
    }
    public void Movetruck()
    {
        mine.text = "트럭으로 이동중";
    }
    public void MoveCoil()
    {
        mine.text = "스키드로 이동중";
    }
}
