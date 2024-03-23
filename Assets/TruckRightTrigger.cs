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

    // OnTriggerExit �Լ��� Ʈ���� ������ �������� �� ȣ��˴ϴ�.
    void Start()
    {
        // Scene���� ExitPoint GameObject�� ã�Ƽ� �Ҵ��մϴ�.
        ExitPoint = GameObject.Find("TruckRightExit");
        TruckPointA = GameObject.Find("TruckRightPosition");

        StartCoroutine(GoPointA());
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
        if (other.CompareTag("RightPoint") && changetrigger ==true)
        {
            Debug.Log("Right ������±�  Ȯ��");
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("RightLift"))
        {
            Debug.Log("Ʈ��Ʈ���źҸ� ���� ��ü");
            changetrigger = false;
            StartCoroutine(GoHome());
        }
    }


    IEnumerator GoHome()
    {
        Debug.Log("������ �ڷ�ƾ");    
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
