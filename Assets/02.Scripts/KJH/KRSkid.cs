using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRSkid : MonoBehaviour
{
    public GameObject[] skid = new GameObject[2];
    public bool SkidBoolLeft = true;
    public bool SkidBoolRight = true;

    // 변경된 변수명: midskidtransform
    Transform midskidtransform;

    // Start is called before the first frame update
    void Start()
    {
        // skid 배열에 두 개의 오브젝트가 할당되어 있다면
        if (skid.Length >= 2 && skid[0] != null && skid[1] != null)
        {
            // z 축 값 가져오기
            float zValue = (skid[0].transform.position.z + skid[1].transform.position.z) / 2.0f;

            // x, y 값 가져오기 (두 오브젝트의 x, y 중간값)
            float xValue = (skid[0].transform.position.x + skid[1].transform.position.x) / 2.0f;
            float yValue = (skid[0].transform.position.y + skid[1].transform.position.y) / 2.0f;

            // midskidtransform 변수에 값 할당하기
            midskidtransform.position = new Vector3(xValue, yValue, zValue);
        }
        else
        {
            Debug.LogWarning("skid 배열에 두 개의 오브젝트가 필요합니다.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update 함수는 필요에 따라 추가적으로 구현하시면 됩니다.
    }
}
