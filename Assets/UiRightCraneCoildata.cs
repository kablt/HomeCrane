using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiRightCraneCoildata : MonoBehaviour
{
    public GameObject craneleftpanel;
    public GameObject cranerightpanel;
    public TextMeshProUGUI textID;
    public TextMeshProUGUI textWeight;
    public TextMeshProUGUI textWidth;
    public TextMeshProUGUI textIOD;
    public TextMeshProUGUI textReceiveOrder;
    public TextMeshProUGUI textSendOrder;
    public GameObject LiftRoot;
    public GameObject detailpanel; // ��Ȳ��
    public int count = 20;
    public GameObject lift; // ����Ʈ ��ũ��Ʈ ������ ���� �Լ�
    CoilCollisionRight collsionright;
    // Start is called before the first frame update
    void Start()
    {
        collsionright = lift.GetComponent<CoilCollisionRight>();
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
            textID.text = collsionright.CoilID2.ToString();
        }
        else
        {
            textID.text = "";
        }

        if (textWeight != null)
        {
            textWeight.text = collsionright.CoilWeight2.ToString();
        }
        else
        {
            textWeight.text = "";
        }

        if (textWidth != null)
        {
            textWidth.text = collsionright.CoilWidth2.ToString();
        }
        else
        {
            textWidth.text = "";
        }

        if (textIOD != null)
        {
            textIOD.text = collsionright.CoilOD2.ToString();
        }
        else
        {
            textIOD.text = "";
        }

        if (textReceiveOrder != null)
        {
            textReceiveOrder.text = collsionright.CoilReceiveOrder2.ToString();
        }
        else
        {
            textReceiveOrder.text = "";
        }

        if (textSendOrder != null)
        {
            textSendOrder.text = collsionright.CoilSendOrder2.ToString();
        }
        else
        {
            textSendOrder.text = "";
        }
    }

    public void craneleftpaneloff()
    {
        cranerightpanel.SetActive(false);
        craneleftpanel.SetActive(true);
    }
}
