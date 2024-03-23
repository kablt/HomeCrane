using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Transform frontView; 
    public Transform sideView;
    public Transform topView;

    void Start()
    {
        // �ʱ� ���¿����� ù ��° ī�޶� Ȱ��ȭ�ǵ��� ����
        if (frontView != null && frontView != null && topView !=null)
        {
            frontView.gameObject.SetActive(true);
            sideView.gameObject.SetActive(false);
            topView.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("ī�޶� �������� �ʾҽ��ϴ�");
        }
    }

    public void FrontCamera()
    {
        frontView.gameObject.SetActive(true) ;

        // ������ ī�޶� ��Ȱ��ȭ
        sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
    }
    public void SideCamera()
    {
        sideView.gameObject.SetActive(true);

        // ������ ī�޶� ��Ȱ��ȭ
        frontView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
    }
    public void TopCamera()
    {
        topView.gameObject.SetActive(true);

        // ������ ī�޶� ��Ȱ��ȭ
        frontView.gameObject.SetActive(false);
        sideView.gameObject.SetActive(false);
    }
}
