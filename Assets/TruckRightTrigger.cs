using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Rendering;

public class TruckRightTrigger : MonoBehaviour
{
    private GameObject ExitPoint;
    private GameObject TruckPointA;
    public bool changetrigger = true;

    // OnTriggerExit 함수는 트리거 영역을 빠져나갈 때 호출됩니다.
    void Start()
    {
        // Scene에서 ExitPoint GameObject를 찾아서 할당합니다.
        ExitPoint = GameObject.Find("TruckRightExit");
        TruckPointA = GameObject.Find("TruckRightPosition");

        StartCoroutine(GoPointA());
    }


    private void OnTriggerEnter(Collider other)
    {
        // 트리거 영역을 빠져나간 오브젝트가 ExitPoint인지 확인합니다.
        if (other.CompareTag("Exit"))
        {
            // 트리거 영역을 빠져나간 자식 오브젝트에 대한 처리를 수행합니다.
            Debug.Log("사라지는거 확인용");
            Destroy(gameObject);
        }
        if (other.CompareTag("RightPoint") && changetrigger ==true)
        {
            Debug.Log("Right 컴페얼태그  확인");
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("RightLift"))
        {
            Debug.Log("트럭트리거불린 폴스 교체");
            changetrigger = false;
            StartCoroutine(GoHome());
        }
    }


    IEnumerator GoHome()
    {
        Debug.Log("집가는 코루틴");    
        float speed = 9f;
        while (Vector3.Distance(transform.position, ExitPoint.transform.position) >= 0f)
        {
            Vector3 direction = ExitPoint.transform.position - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
            yield return null;
        }
        yield return null;

    }

    IEnumerator GoPointA()
    {
        float truckspeed = 9f;

        while (Vector3.Distance(transform.position, TruckPointA.transform.position) >= 0f)
        {
            // Calculate the direction towards the ExitPoint.
            Vector3 direction = TruckPointA.transform.position - transform.position;

            // Normalize the direction vector to maintain constant speed.
            direction.Normalize();

            // Move the object towards the ExitPoint with constant speed.
            transform.position += direction * truckspeed * Time.deltaTime;

            yield return null; // Wait for the next frame.
        }
        yield return null;
    }
   
}
