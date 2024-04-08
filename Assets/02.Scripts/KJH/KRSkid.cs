using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KRSkid : MonoBehaviour
{
    public GameObject[] skid = new GameObject[2];
    public bool SkidBoolLeft = true;
    public bool SkidBoolRight = true;

    // ����� ������: midskidtransform
    Transform midskidtransform;

    // Start is called before the first frame update
    void Start()
    {
        // skid �迭�� �� ���� ������Ʈ�� �Ҵ�Ǿ� �ִٸ�
        if (skid.Length >= 2 && skid[0] != null && skid[1] != null)
        {
            // z �� �� ��������
            float zValue = (skid[0].transform.position.z + skid[1].transform.position.z) / 2.0f;

            // x, y �� �������� (�� ������Ʈ�� x, y �߰���)
            float xValue = (skid[0].transform.position.x + skid[1].transform.position.x) / 2.0f;
            float yValue = (skid[0].transform.position.y + skid[1].transform.position.y) / 2.0f;

            // midskidtransform ������ �� �Ҵ��ϱ�
            midskidtransform.position = new Vector3(xValue, yValue, zValue);
        }
        else
        {
            Debug.LogWarning("skid �迭�� �� ���� ������Ʈ�� �ʿ��մϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update �Լ��� �ʿ信 ���� �߰������� �����Ͻø� �˴ϴ�.
    }
}
