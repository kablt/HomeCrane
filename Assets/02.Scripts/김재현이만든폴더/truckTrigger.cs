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

    // OnTriggerExit �Լ��� Ʈ���� ������ �������� �� ȣ��˴ϴ�.
    void Start()
    {
        // Scene���� ExitPoint GameObject�� ã�Ƽ� �Ҵ��մϴ�.
        ExitPoint = GameObject.Find("CarExit");
        TruckPointA = GameObject.Find("CarPoint");
        GameManager = GameObject.Find("GameManager");
        colorchange = GameManager.GetComponent<ColorChange>();

        StartCoroutine(GoPointA());
    }

    private void OnTriggerExit(Collider other)
    {
        // Ʈ���� ������ �������� ������Ʈ�� Coil �±׸� ���� �ڽ� ������Ʈ���� Ȯ���մϴ�.
        if (other.CompareTag("Coil"))
        {
            // Ʈ���� ������ �������� �ڽ� ������Ʈ�� ���� ó���� �����մϴ�.
            Debug.Log("Coil �±׸� ���� ������Ʈ�� Ʈ���� ������ �����������ϴ�.");
            GohomeNow();
            colorchange.exitcolor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���� ������ �������� ������Ʈ�� ExitPoint���� Ȯ���մϴ�.
        if (other.CompareTag("Exit"))
        {
            // Ʈ���� ������ �������� �ڽ� ������Ʈ�� ���� ó���� �����մϴ�.
            Debug.Log("������°� Ȯ�ο�");
            Destroy(gameObject);
        }
        if(other.CompareTag("PointA"))
        {
            Debug.Log("Ʈ�����߱�");
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
        Debug.Log("Ʈ�����ߴ��ڷ�ƾȮ��");
        StopCoroutine(GoPointA());
    }
}
