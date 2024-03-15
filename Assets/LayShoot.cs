using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayShoot : MonoBehaviour
{
    public GameObject CraneManager;
    CraneMove cranemove;
    void Awake()
    {
        cranemove = CraneManager.GetComponent<CraneMove>();
    }
    public float rayDis = 10f;
    public void ShootAndCheckForCoil()
    {
        // Shoot a ray from the current GameObject in the forward direction
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit, rayDis))
        {
            // Check if the hit object has the specified tag "coil"
            if (hit.collider.CompareTag("Coil"))
            {
                Debug.Log("코일확인 완료");
                //MovePickUpPoint 상태로 전환
                cranemove.StatusChangeMovePickUpPopint();
            }
            else
            {
                Debug.Log("코일이 아닌 물체가 있습니다");
            }
        }
        else
        {
            if(Mathf.Abs(cranemove.LiftRollBack.position.y - cranemove.CraneLift.transform.position.y) < 0.1f || Mathf.Abs(cranemove.LiftRollBack.position.y - cranemove.CraneLift.transform.position.y) ==0f)
            {
                Debug.Log("거리 조건이 만족되어 이동하는 함수가 실행되지않음");
                return;
            }
            Vector3 targetPositionY = new Vector3(cranemove.CraneLift.transform.position.x, cranemove.LiftRollBack.position.y, cranemove.CraneLift.transform.position.z);
            cranemove.CraneLift.transform.position = Vector3.Lerp(cranemove.CraneLift.transform.position, targetPositionY, cranemove.moveSpeed * Time.deltaTime);
            Debug.Log("Move1");
            Debug.Log("어떠한 물체도 감지되지 않음");
        }
    }
}
