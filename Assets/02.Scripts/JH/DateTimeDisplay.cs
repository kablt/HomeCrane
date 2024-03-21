using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DateTimeDisplay : MonoBehaviour
{
    public TMP_Text dateTimeText; // Inspector���� �Ҵ��� Text ������Ʈ

    void Update()
    {
        // ���� ��¥�� �ð��� "��-��-�� ��:��:��" �������� ����
        dateTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
