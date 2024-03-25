using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Rendering;

public class truckTrigger : MonoBehaviour
{
    private GameObject ExitPoint;
    private GameObject TruckPointA;
    private GameObject GameManager;
    ColorChange colorchange;
    public int truckNum;

    // OnTriggerExit 함수는 트리거 영역을 빠져나갈 때 호출됩니다.
    void Start()
    {
        // Scene에서 ExitPoint GameObject를 찾아서 할당합니다.
        ExitPoint = GameObject.Find("CarExit");
        TruckPointA = GameObject.Find("CarPoint");
        GameManager = GameObject.Find("GameManager");
        colorchange = GameManager.GetComponent<ColorChange>();

        StartCoroutine(GoPointA());
    }

    private void OnTriggerExit(Collider other)
    {
        // 트리거 영역을 빠져나간 오브젝트가 Coil 태그를 가진 자식 오브젝트인지 확인합니다.
        if (other.CompareTag("Coil"))
        {
            // 트리거 영역을 빠져나간 자식 오브젝트에 대한 처리를 수행합니다.
            Debug.Log("Coil 태그를 가진 오브젝트가 트리거 영역을 빠져나갔습니다.");
            GohomeNow();
            colorchange.exitcolor();
        }
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
        if(other.CompareTag("PointA"))
        {
            Debug.Log("트럭멈추기");
            StopAllCoroutines();
            StopTruck();
            colorchange.waitcolor();
        }
    }

    public void GohomeNow()
    {
        StopAllCoroutines();
        Debug.Log("gohomecheck");
        StartCoroutine(GoHome());
    }

    IEnumerator GoHome()
    {
        Debug.Log("GoHome coroutine started");

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
    public void StopTruck()
    {
        Debug.Log("트럭멈추는코루틴확인");
        StopCoroutine(GoPointA());
    }
}
