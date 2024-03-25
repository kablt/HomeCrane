using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform R_MainCamera;

    public Transform L_frontView;
    public Transform L_sideView;
    public Transform topView;

    public Transform R_frontView;
    public Transform R_sideView;

    void Start()
    {
        // 초기 상태에서는 메인 카메라만 활성화되도록 설정
        ActivateMainCamera();
    }

    public void ActivateMainCamera()
    {
        // 메인 카메라 활성화
        mainCamera.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        L_frontView.gameObject.SetActive(false);
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }

    public void RightMainCamera()
    {
        // 메인 카메라 활성화
        R_MainCamera.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        mainCamera.gameObject.SetActive(false);
        L_frontView.gameObject.SetActive(false);
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
    }

    public void LeftFrontCamera()
    {
        L_frontView.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }
    public void LeftSideCamera()
    {
        L_sideView.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        L_frontView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }
    public void TopCamera()
    {
        topView.gameObject.SetActive(true);

        // 나머지 카메라 비활성화
        L_frontView.gameObject.SetActive(false);
        L_sideView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }

    public void RightFrontCamera()
    {
        R_frontView.gameObject.SetActive(true);

        //
        R_sideView.gameObject.SetActive(false);
        L_frontView.gameObject.SetActive(false);
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }
    public void RightSideCamera()
    {
        R_sideView.gameObject.SetActive(true);

        //
        R_frontView.gameObject.SetActive(false);
        L_frontView.gameObject.SetActive(false);
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }

}
