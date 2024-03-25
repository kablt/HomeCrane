using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelActive : MonoBehaviour
{
    public GameObject firstCrane;
    public GameObject secondCrane;

    void Start()
    {
        // 시작할 때는 1번 Panel만 활성화
        firstCrane.SetActive(true);
        secondCrane.SetActive(false);
    }

    public void FirstPanel()
    {
        firstCrane.SetActive(true);
        secondCrane.SetActive(false);
    }

    public void SecondPanel()
    {
        firstCrane.SetActive(false);
        secondCrane.SetActive(true);
    }
}
