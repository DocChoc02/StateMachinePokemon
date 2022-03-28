using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAi : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 3;
    public GameObject wayPointPrefab;
    public List <GameObject> position;
    public int positionIndex = 0;

    public float speed = 1.5f;
    public float minGoalDistance = 0.1f;
    // Start is called before the first frame update


    // Update is called once per frame
    /* void Update()
     {
         if (Vector2.Distance(transform.position, player.position) < chaseDistance)
         {
             AiMoveTowards(player);
         }
         else
         {
             AiMoveTowards(position[positionIndex].transform);
             WaypointUpdate();
         }

     }*/

    private void Start()
    {
        NewWayPoint();
        NewWayPoint();
        NewWayPoint();
        NewWayPoint();

    }

    public void NewWayPoint()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);
        GameObject newPoint = Instantiate(wayPointPrefab,new Vector2(x, y), Quaternion.identity);
        position.Add(newPoint);
    }

    public void RemoveCurrentWayPoint()
    {
        GameObject current = position[positionIndex];
        position.Remove(current);
        Destroy(current);
    }

    public void FindClosestWaypoint()
    {
        float nearest = float.PositiveInfinity;
        int nearestIndex = 0;

        for(int i = 0; i < position.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, position[1].transform.position);
            if (distance < nearest)
            {
                nearest = distance;
                nearestIndex = 1;
            }
        }

        positionIndex = nearestIndex;
    }

    public bool PlayerInRange()
    {
        return Vector2.Distance(transform.position, player.position) < chaseDistance;
    }

    public void WaypointUpdate()
    {
        if (Vector2.Distance(transform.position, position[positionIndex].transform.position) < minGoalDistance)
        {
            RemoveCurrentWayPoint();
            positionIndex++;

            if (positionIndex >= position.Count)
            {
                positionIndex = 0;
            }
        }
    }


    public void AiMoveTowards(Transform goal)
    {

        if( Vector2.Distance(transform.position, goal.position) > minGoalDistance)
        {
            Vector2 directionToGoal = (goal.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }
        


    }
   
}
