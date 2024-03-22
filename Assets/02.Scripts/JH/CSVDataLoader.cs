using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CoilList // 클래스 명 변경
{
  // CoilID는 여전히 문자열로 받습니다.  (ex,C001)
    public float CoilID,CoilWeight, CoilWidth, CoilOD; // 이제 이 필드들은 float 타입입니다.
    public float CoilReceiveOrder, CoilSendOrder; // 숫자만 포함되어 있다면, int로 받는 것이 더 적합할 수 있습니다.
}

public class CSVDataLoader : MonoBehaviour
{
    public TextAsset csvFile; // 인스펙터 창에서 지정할 CSV 파일
    public List<CoilList> coilDatas = new List<CoilList>(); // CSV에서 읽은 데이터를 저장할 리스트, 타입 변경

    void Awake()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] dataLines = csvFile.text.Split('\n');
        for (int i = 0; i < dataLines.Length; i++) // 첫 번째 행은 헤더라고 가정하고 건너뜁니다.
        {
            string[] data = dataLines[i].Split(',');
            if (data.Length == 6)
            {
                coilDatas.Add(new CoilList() // 타입 변경
                {
                    CoilID = float.Parse(data[0]), // 문자열 그대로 받음
                    CoilWeight = float.Parse(data[1]), // 문자열을 float로 변환
                    CoilWidth = float.Parse(data[2]), // 문자열을 float로 변환
                    CoilOD = float.Parse(data[3]), // 문자열을 float로 변환
                    CoilReceiveOrder = float.Parse(data[4]), // 문자열을 int로 변환
                    CoilSendOrder = float.Parse(data[5]) // 문자열을 int로 변환
                });
            }
        }
    }

}
