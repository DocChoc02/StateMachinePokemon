using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    //coma sperated list of identifieers
    public Text stateText;

    public enum State
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking
    }
    public State currentState; 

    public MoveAi aiMovement;

    private void Start()
    {
        aiMovement = GetComponent<MoveAi>();
        NextState();
    }

    private void NextState()
    {
        
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryState());
                break;
            default:
                break;
        }
        stateText.text = "state : " + currentState;
    }
    private IEnumerator AttackState()
    {
        Debug.Log("Attack");
        while (currentState == State.Attack)
        {
            aiMovement.AiMoveTowards(aiMovement.player);
            if (!aiMovement.PlayerInRange())
            {
                aiMovement.FindClosestWaypoint();
                currentState = State.BerryPicking;
                
            }
            yield return null;
        }

        Debug.Log("Stop Attack");
        NextState();
    }
    private IEnumerator DefenceState()
    {
        
        while (currentState == State.Defence)
        {
            yield return new WaitForSeconds(2);
            aiMovement.NewWayPoint();
            aiMovement.NewWayPoint();
            aiMovement.NewWayPoint();
            aiMovement.NewWayPoint();
            currentState = State.BerryPicking;
            yield return null;
            
        }

       
        NextState();
    }
    private IEnumerator RunAwayState()
    {
        Debug.Log("Run");
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently Run");
            yield return null;
        }

        Debug.Log("Stop Run");
        NextState();
    }
    private IEnumerator BerryState()
    {
        Debug.Log("Berry");
        while (currentState == State.BerryPicking)
        {
            aiMovement.AiMoveTowards(aiMovement.position[aiMovement.positionIndex].transform);
            aiMovement.WaypointUpdate();
            if (aiMovement.PlayerInRange())
            {
                currentState = State.Attack;
                Debug.Log("atak");
            }
            if (aiMovement.position.Count < 1)
            {
                currentState = State.Defence;
            }
            yield return null;
        }

        Debug.Log("Stop Berry");
        NextState();
    }
}

