using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    bool isGoingLeft = false;
    public Vector3 _originalPosition;
    public Vector3 velocity;
    public float distance;

    private Transform _transform;

    void Start()
    {
        _transform = this.gameObject.transform;
    }

    void Update()
    {
        float distFromStart = transform.position.x - _originalPosition.x;

        if (isGoingLeft)
        {
            // If gone too far, switch direction
            if (distFromStart < -distance)
                SwitchDirection();

            _transform.Translate(velocity.x * Time.deltaTime, 0, 0);
        }
        else
        {
            // If gone too far, switch direction
            if (distFromStart > distance)
                SwitchDirection();

            _transform.Translate(-velocity.x * Time.deltaTime, 0, 0);
        }
    }

    void SwitchDirection()
    {
        isGoingLeft = !isGoingLeft;
        //TODO: Change facing direction, animation, etc
    }
}


