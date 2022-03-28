using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIManager : BaseManager
{
    
    public enum State
    {
        HighHP,
        LowHP,
        Dead,
    }
    public State currentState;
    protected PlayerManager _playerManager;
    [SerializeField] protected Animator _anim;
    protected override void Start()
    {
        base.Start();
        _playerManager = GetComponent<PlayerManager>();
        if (_playerManager == null)
        {
            Debug.Log("no player manager");
        }
    }
    public override void TakeTurn()
    {
        if(_health <= 0f)
        {
            currentState = State.Dead;
        }
        switch (currentState)
        {
            case State.HighHP:
                HighHPState();
                break;
            case State.LowHP:
                LowHPState();
                break;
            case State.Dead:
                DeadState();
                break;
            default:
                throw new ArgumentOutOfRangeException();
               
        }
    }
    protected override void EndTurn()
    {
        
        StartCoroutine(WaitAndEndTurn());
    }
    private IEnumerator WaitAndEndTurn()
    {
        
        yield return new WaitForSecondsRealtime(1f);
        _playerManager.TakeTurn();
    }
    void DeadState()
    {
        Debug.Log("AI dead GG");
    }
    void HighHPState()
    {
        if(_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }
        //20% chance spec attack
        //70% chance main attack
        //10%
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 7:
                MainAttack();
                break;
            case int i when i >= 7 && i <= 8:
                SpecAttack();
                break;
            case int i when i >= 8 && i <= 9:
                Flee();
                break;
        }

    }
    void LowHPState()
    {
        //20% chance Attack
        //70% chance Heal
        //10% chance Flee
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 7:
                Regen();
                break;
            case int i when i >= 7 && i <= 8:
                MainAttack();
                break;
            case int i when i >= 8 && i <= 9:
                Flee();
                break;
        }
        if (_health > 60f)
        {
            currentState = State.HighHP;
        }
    }
    public void MainAttack()
    {
        _playerManager.DealDamage(20f);
        _anim.SetTrigger("Beam");
        EndTurn();
    }
    public void SpecAttack()
    {
        _playerManager.DealDamage(40f);
        EndTurn();
    }
    public void Regen()
    {
        Heal(30f);
        EndTurn();
    }
    public void Flee()
    {
        _playerManager.DealDamage(_maxHealth);
        DealDamage(_maxHealth);
        EndTurn();
    }

}
