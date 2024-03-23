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
        // 초기 상태에서는 첫 번째 카메라만 활성화되도록 설정
        if (frontView != null && frontView != null && topView !=null)
        {
            frontView.gameObject.SetActive(true);
            sideView.gameObject.SetActive(false);
            topView.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("카메라를 설정하지 않았습니다");
        }
    }

    public void FrontCamera()
    {
        frontView.gameObject.SetActive(true) ;

        // 나머지 카메라 비활성화
        sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
    }
    public void SideCamera()
    {
        sideView.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        frontView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
    }
    public void TopCamera()
    {
        topView.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        frontView.gameObject.SetActive(false);
        sideView.gameObject.SetActive(false);
    }
}
