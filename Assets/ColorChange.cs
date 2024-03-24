using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorChange : MonoBehaviour
{
    public TMP_Text textComponentStart; // �ڵ��� �������� 
    public TMP_Text textComponentWait;// �ڵ��� �����
    public TMP_Text textComponentExit;// �ڵ��� ��������
    public Color color1 = Color.red; // ù ��° ����
    public Color color2 = Color.blue; // �� ��° ����
    private bool isColor1 = true; // ���� ���� ���¸� ����

    void Start()
    {
        
    }

    IEnumerator ChangeColorStart()
    {
        while (true) // ���� ����
        {
            // ���� ���¿� ���� Text�� ���� ����
            textComponentStart.color = isColor1 ? color1 : color2;
            // ���� ���� ���
            isColor1 = !isColor1;
            // 1�� ���
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator ChangeColorWait()
    {
        while (true) // ���� ����
        {
            // ���� ���¿� ���� Text�� ���� ����
            textComponentWait.color = isColor1 ? color1 : color2;
            // ���� ���� ���
            isColor1 = !isColor1;
            // 1�� ���
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator ChangeColorExit()
    {
        while (true) // ���� ����
        {
            // ���� ���¿� ���� Text�� ���� ����
            textComponentExit.color = isColor1 ? color1 : color2;
            // ���� ���� ���
            isColor1 = !isColor1;
            // 1�� ���
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