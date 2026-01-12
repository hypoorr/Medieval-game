using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    private Rigidbody rb;

    private bool isCaught;

    public UnityEvent OnPlayerCatch;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(FOVRoutine());



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerRef)
        {
            if (!isCaught)
            {
                isCaught = true;
                Debug.Log("Player caught!");
                OnPlayerCatch.Invoke();
            }

        }
    }

    private void FixedUpdate()
    {
        if (canSeePlayer)
        {

            Vector3 dir = playerRef.transform.position - transform.position;
            dir.y = 0;
            dir.Normalize();


            Quaternion targetRot = Quaternion.LookRotation(dir);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime));


            Vector3 move = dir * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}