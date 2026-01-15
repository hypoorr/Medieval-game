using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
public class AIAgent : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInfo)){
                Vector3 pos = hitInfo.point;
                pos = new Vector3(pos.x,0,pos.z);
                agent.SetDestination(pos);
            }
        }

        Vector3 playerPos = player.transform.position;

        

        
    }
}
