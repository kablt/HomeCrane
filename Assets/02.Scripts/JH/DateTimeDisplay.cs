using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DateTimeDisplay : MonoBehaviour
{
    public TMP_Text dateTimeText; // Inspector에서 할당할 Text 컴포넌트

    void Update()
    {
        // 현재 날짜와 시간을 "년-월-일 시:분:초" 형식으로 설정
        dateTimeText.text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
