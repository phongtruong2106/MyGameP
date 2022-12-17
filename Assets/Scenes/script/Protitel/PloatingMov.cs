using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloatingMov : MonoBehaviour
{
    private Vector3 locationA;
    private Vector3 locationB;
    private Vector3 nextLocation;


    [SerializeField] private Transform platform;
    [SerializeField] private Transform MovingToLocation;

    [SerializeField] private float speed;



    private void Start()
    {
        locationA = platform.localPosition;
        locationB = MovingToLocation.localPosition;
        nextLocation = locationB;
    }

    private void Update()
    {
        Move();
        
    }

    private void Move()
    {
        platform.localPosition = Vector3.MoveTowards(platform.localPosition, nextLocation, speed * Time.deltaTime);

        if(Vector3.Distance(platform.localPosition, nextLocation) <= 0.1)
        {
             ChangePosition();
        }
    }
    private void ChangePosition()
    {
        nextLocation = nextLocation != locationA ? locationA : locationB;

    }

}
