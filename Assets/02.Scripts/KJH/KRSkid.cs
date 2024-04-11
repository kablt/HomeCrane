using UnityEngine;

public class KRSkid : MonoBehaviour
{
    public GameObject[] skid = new GameObject[3]; // 3번쨰 인덱스는 자기자신. 중앙 위치
    public bool SkidBoolLeft = true;
    public bool SkidBoolRight = true;
  

    void Start()
    {
        // midskidtransform 초기화
        // z 축 값 가져오기
        float zValue = (skid[0].transform.position.z + skid[1].transform.position.z) / 2.0f;

        // x, y 값 가져오기 (두 오브젝트의 x, y 중간값)
        float xValue = (skid[0].transform.position.x + skid[1].transform.position.x) / 2.0f;
        float yValue = (skid[0].transform.position.y + skid[1].transform.position.y) / 2.0f;

    }

    void Update()
    {
        // Update 함수는 필요에 따라 추가적으로 구현하시면 됩니다.
    }
}
