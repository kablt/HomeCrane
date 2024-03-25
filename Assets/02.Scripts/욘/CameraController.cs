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
        // �ʱ� ���¿����� ���� ī�޶� Ȱ��ȭ�ǵ��� ����
        ActivateMainCamera();
    }

    public void ActivateMainCamera()
    {
        // ���� ī�޶� Ȱ��ȭ
        mainCamera.gameObject.SetActive(true);

        // ������ ī�޶� ��Ȱ��ȭ
        L_frontView.gameObject.SetActive(false);
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }

    public void RightMainCamera()
    {
        // ���� ī�޶� Ȱ��ȭ
        R_MainCamera.gameObject.SetActive(true);

        // ������ ī�޶� ��Ȱ��ȭ
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

        // ������ ī�޶� ��Ȱ��ȭ
        L_sideView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }
    public void LeftSideCamera()
    {
        L_sideView.gameObject.SetActive(true);

        // ������ ī�޶� ��Ȱ��ȭ
        L_frontView.gameObject.SetActive(false);
        topView.gameObject.SetActive(false);
        R_sideView.gameObject.SetActive(false);
        R_frontView.gameObject.SetActive(false);
        R_MainCamera.gameObject.SetActive(false);
    }
    public void TopCamera()
    {
        topView.gameObject.SetActive(true);

        // ������ ī�޶� ��Ȱ��ȭ
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
