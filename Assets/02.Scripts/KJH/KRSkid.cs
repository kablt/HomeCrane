using UnityEngine;

public class KRSkid : MonoBehaviour
{
    public GameObject[] skid = new GameObject[3]; // 3���� �ε����� �ڱ��ڽ�. �߾� ��ġ
    public bool SkidBoolLeft = true;
    public bool SkidBoolRight = true;
  

    void Start()
    {
        // midskidtransform �ʱ�ȭ
        // z �� �� ��������
        float zValue = (skid[0].transform.position.z + skid[1].transform.position.z) / 2.0f;

        // x, y �� �������� (�� ������Ʈ�� x, y �߰���)
        float xValue = (skid[0].transform.position.x + skid[1].transform.position.x) / 2.0f;
        float yValue = (skid[0].transform.position.y + skid[1].transform.position.y) / 2.0f;

    }

    void Update()
    {
        // Update �Լ��� �ʿ信 ���� �߰������� �����Ͻø� �˴ϴ�.
    }
}
