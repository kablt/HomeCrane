using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;
using UnityEngine.AI;

public class SimpleStaticAgent : MonoBehaviour
{
    [SerializeField]
    Transform target;
    NavMeshAgent nmAgent;
    // Start is called before the first frame update
    void Start()
    {
         nmAgent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAnimatorMove()
    {
        nmAgent.SetDestination(target.position);
    }
    
}
