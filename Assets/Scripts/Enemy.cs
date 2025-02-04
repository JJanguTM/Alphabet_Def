using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private ObjectMover movement2D;

    public void Setup(Transform[] wayPoints)
    {
        movement2D = GetComponent<ObjectMover>();

        wayPointCount = wayPoints.Length; 
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while(true)
        {

            
            if( Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            
            yield return null;
        }

    }

    private void NextMoveTo()
    {
        if ( currentIndex < wayPointCount -1 )
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex ++;
            Vector3 direction = (wayPoints[currentIndex].position-transform.position).normalized;
            movement2D.MoveTo(direction);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
