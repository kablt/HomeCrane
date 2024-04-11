using System.Collections;
using UnityEngine;

public class RailCart : MonoBehaviour
{
    public Transform StartWaitPoint;
    public Transform EndWaitPoint;
    private float moveSpeed = 0.5f;
    private bool UpdateControll = true;
    public bool leftSkidCoil = true;
    public bool rightSkidCoil = true;

    public IEnumerator StartPointMove()
    {
        UpdateControll = false;
        yield return new WaitForSeconds(1f);
        while (Vector3.Distance(StartWaitPoint.position, transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, StartWaitPoint.position, moveSpeed * Time.deltaTime);
            yield return null; 
        }
        UpdateControll = true;
        
    }

    public IEnumerator EndPointMove()
    {
        UpdateControll = false;
        yield return new WaitForSeconds(1f);
        while(Vector3.Distance(EndWaitPoint.position, transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp( transform.position ,EndWaitPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        UpdateControll = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPointMove()); 
    }

    // Update is called once per frame
    void Update()
    {
        if(leftSkidCoil == true && rightSkidCoil == true && UpdateControll == true)
        {
            StartCoroutine(StartPointMove());
        }
        if(leftSkidCoil==false && rightSkidCoil == false && UpdateControll == true)
        {
            StartCoroutine(EndPointMove());
        }
    }
}
