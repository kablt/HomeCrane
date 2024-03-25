using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiCraneCoildata : MonoBehaviour
{
    public GameObject craneleftpanel;
    public GameObject cranerightpanel;
    public TextMeshProUGUI textID;
    public TextMeshProUGUI textWeight;
    public TextMeshProUGUI textWidth;
    public TextMeshProUGUI textIOD;
    public TextMeshProUGUI textReceiveOrder;
    public TextMeshProUGUI textSendOrder;
    public GameObject detailpanel; // 상황판
    public int count = 0;
    public GameObject Leftlift; // 리프트 스크립트 접근을 위한 함수
    CoilCollision collsionleft;
    // Start is called before the first frame update
    void Start()
    {
        Leftlift = GameObject.Find("LiftRootLeft");
        collsionleft = Leftlift.GetComponent<CoilCollision>();
        textID.text = "";
        textWeight.text = "";
        textWidth.text = "";
        textIOD.text = "";
        textReceiveOrder.text = "";
        textSendOrder.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void opendetailpanel()
    {
        detailpanel.SetActive(true);
    }

    public void setdata()
    {
        if (textID != null)
        {
            textID.text = collsionleft.CoilID2.ToString();
        }
        else
        {
            textID.text = "";
        }

        if (textWeight != null)
        {
            textWeight.text = collsionleft.CoilWeight2.ToString();
        }
        else
        {
            textWeight.text = "";
        }

        if (textWidth != null)
        {
            textWidth.text = collsionleft.CoilWidth2.ToString();
        }
        else
        {
            textWidth.text = "";
        }

        if (textIOD != null)
        {
            textIOD.text = collsionleft.CoilOD2.ToString();
        }
        else
        {
            textIOD.text = "";
        }

        if (textReceiveOrder != null)
        {
            textReceiveOrder.text = collsionleft.CoilReceiveOrder2.ToString();
        }
        else
        {
            textReceiveOrder.text = "";
        }

        if (textSendOrder != null)
        {
            textSendOrder.text = collsionleft.CoilSendOrder2.ToString();
        }
        else
        {
            textSendOrder.text = "";
        }
    }

    public void craneleftpaneloff()
    {
        craneleftpanel.SetActive(false);
        cranerightpanel.SetActive(true);
    }
}
