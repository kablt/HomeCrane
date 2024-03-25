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
    public GameObject LiftRoot;
    public GameObject detailpanel; // ªÛ»≤∆«
    public int count = 0;
    CoilCollision coilcollsion;
    // Start is called before the first frame update
    void Start()
    {
      
        coilcollsion = LiftRoot.GetComponent<CoilCollision>();
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
        /*
        textID.text = coilcollsion.coildata.InCoilID.ToString();
        textWeight.text = coilcollsion.coildata.InCoilWeight.ToString();
        textWidth.text = coilcollsion.coildata.InCoilWidth.ToString();
        textIOD.text = coilcollsion.coildata.InCoilIOD.ToString();
        textReceiveOrder.text = coilcollsion.coildata.InCoilReceiveOrder.ToString();
        textSendOrder.text = coilcollsion.coildata.InCoilSendOrder.ToString();
        */
    }

    public void opendetailpanel()
    {
        detailpanel.SetActive(true);
    }

    public void setdata()
    {
        if (textID != null)
        {
            textID.text = coilcollsion.coildata.InCoilID.ToString();
        }
        else
        {
            textID.text = "";
        }

        if (textWeight != null)
        {
            textWeight.text = coilcollsion.coildata.InCoilWeight.ToString();
        }
        else
        {
            textWeight.text = "";
        }

        if (textWidth != null)
        {
            textWidth.text = coilcollsion.coildata.InCoilWidth.ToString();
        }
        else
        {
            textWidth.text = "";
        }

        if (textIOD != null)
        {
            textIOD.text = coilcollsion.coildata.InCoilIOD.ToString();
        }
        else
        {
            textIOD.text = "";
        }

        if (textReceiveOrder != null)
        {
            textReceiveOrder.text = coilcollsion.coildata.InCoilReceiveOrder.ToString();
        }
        else
        {
            textReceiveOrder.text = "";
        }

        if (textSendOrder != null)
        {
            textSendOrder.text = coilcollsion.coildata.InCoilSendOrder.ToString();
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
