using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RightCraneStatus : MonoBehaviour
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
        mine.text = "�����";
    }
    public void Movetruck()
    {
        mine.text = "Ʈ������ �̵���";
    }
    public void MoveCoil()
    {
        mine.text = "��Ű��� �̵���";
    }
}
