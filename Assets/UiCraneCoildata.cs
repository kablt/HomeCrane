using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiCraneCoildata : MonoBehaviour
{
    public TextMeshProUGUI textID;
    public TextMeshProUGUI textWeight;
    public TextMeshProUGUI textWidth;
    public TextMeshProUGUI textIOD;
    public TextMeshProUGUI textReceiveOrder;
    public TextMeshProUGUI textSendOrder;
    public GameObject LiftRoot;
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
        textID.text = coilcollsion.coildata.InCoilID.ToString();
        textWeight.text = coilcollsion.coildata.InCoilWeight.ToString();
        textWidth.text = coilcollsion.coildata.InCoilWidth.ToString();
        textIOD.text = coilcollsion.coildata.InCoilOD.ToString();
        textReceiveOrder.text = coilcollsion.coildata.InCoilReceiveOrder.ToString();
        textSendOrder.text = coilcollsion.coildata.InCoilSendOrder.ToString();

    }
}
