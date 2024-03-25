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
 
    }

    public void opendetailpanel()
    {
        detailpanel.SetActive(true);
    }

    public void setdata()
    {

        textID.text = coilcollsion.coildata.InCoilID.ToString();
        textWeight.text = coilcollsion.coildata.InCoilWeight.ToString();
        textWidth.text = coilcollsion.coildata.InCoilWidth.ToString();
        textIOD.text = coilcollsion.coildata.InCoilIOD.ToString();
        textReceiveOrder.text = coilcollsion.coildata.InCoilReceiveOrder.ToString();
        textSendOrder.text = coilcollsion.coildata.InCoilSendOrder.ToString();

    }

    public void craneleftpaneloff()
    {
        cranerightpanel.SetActive(false);
        craneleftpanel.SetActive(true);
    }
}
