using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorRightChange : MonoBehaviour
{
    public TMP_Text textComponentStart; // 자동차 들어오는중 
    public TMP_Text textComponentWait;  // 자동차 대기중
    public TMP_Text textComponentExit;  // 자동차 나가는중
    public Color color1 = Color.white;  // 첫 번째 색상
    public Color color2;                // 두 번째 색상 (from hexadecimal)
    public string hexColor = "3BC047";  // 두 번째 색상의 hexadecimal 값
    private bool isColor1 = true;       // 현재 색상 상태를 추적

    void Start()
    {
        color1 = Color.white;
        color2 = HexToColor(hexColor);
    }

    Color HexToColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
    IEnumerator ChangeColorStart()
    {
        while (true) // 무한 루프
        {
            // 색상 상태에 따라 Text의 색상 변경
            textComponentStart.color = isColor1 ? color1 : color2;
            // 색상 상태 토글
            isColor1 = !isColor1;
            // 1초 대기
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator ChangeColorWait()
    {
        while (true) // 무한 루프
        {
            // 색상 상태에 따라 Text의 색상 변경
            textComponentWait.color = isColor1 ? color1 : color2;
            // 색상 상태 토글
            isColor1 = !isColor1;
            // 1초 대기
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator ChangeColorExit()
    {
        while (true) // 무한 루프
        {
            // 색상 상태에 따라 Text의 색상 변경
            textComponentExit.color = isColor1 ? color1 : color2;
            // 색상 상태 토글
            isColor1 = !isColor1;
            // 1초 대기
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void startcolor()
    {
        stopcolor();
        StartCoroutine(ChangeColorStart());
    }
    public void waitcolor()
    {
        stopcolor();
        StartCoroutine(ChangeColorWait());
    }
    public void exitcolor()
    {
        stopcolor();
        StartCoroutine(ChangeColorExit());
    }

    public void stopcolor()
    {
        StopAllCoroutines();
        textComponentStart.color = Color.white;
        textComponentWait.color = Color.white;
        textComponentExit.color = Color.white;
    }
}
