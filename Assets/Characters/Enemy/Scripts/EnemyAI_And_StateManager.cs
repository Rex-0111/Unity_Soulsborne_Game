using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    public enum States
    {
        Petrolling_State,
        Chasing_State,
        Attack_State,
        Find_State
    }

    protected GameObject Player;
    public States CurrentState;
    protected NavMeshAgent agent;
    protected float remainingDist;
    protected Enemy_Locomotion Enemy_Locomotion;

    private void Start()
    {
        Enemy_Locomotion = GetComponent<Enemy_Locomotion>();
        Player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        CurrentState = States.Petrolling_State;
    }
}

public class EnemyAI_And_StateManager : StateManager
{
    private Coroutine findCoroutine; // To keep track of the coroutine

    private void Update() // Changed to Update
    {
        remainingDist = Vector3.Distance(transform.position, Player.transform.position);

        //Debug.Log(Vector3.Distance(transform.position, Player.transform.position));

        if (remainingDist > 15f && CurrentState != States.Petrolling_State)
        {
            if (findCoroutine == null) // Start the coroutine if not already running
                findCoroutine = StartCoroutine(FindToPetrol());
        }
        else if (remainingDist <= 15f && remainingDist > 1.5f)
        {
            if (findCoroutine != null) // Stop coroutine if chasing
            {
                StopCoroutine(findCoroutine);
                findCoroutine = null;
            }
            if (!Enemy_Locomotion.isAttacking) 
            { 
            CurrentState = States.Chasing_State;
            }
        }
        else if (remainingDist <= 1.5f && remainingDist > 1f)
        {
            CurrentState = States.Attack_State;
        }
    }

    IEnumerator FindToPetrol()
    {
        CurrentState = States.Find_State;
        yield return new WaitForSeconds(1f);
        CurrentState = States.Petrolling_State;
        findCoroutine = null; // Reset the coroutine tracker
    }
}

