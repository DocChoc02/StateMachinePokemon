using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    private AIManager _aiManager;
    [SerializeField] protected CanvasGroup _buttonGroup;
    protected override void Start()
    {
        base.Start();
        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("AIManager no find");
        }
    }

    public override void TakeTurn()
    {
        
        _buttonGroup.interactable = true;
        Debug.Log("buttonGroup enabled");
        //_aiManager.TakeTurn();
    }

    protected override void EndTurn()
    {
        _buttonGroup.interactable = false;
        Debug.Log("buttonGroup disabled");
        _aiManager.TakeTurn();
        
    }
    public void MainAttack()
    {
        _aiManager.DealDamage(20f);
        EndTurn();
    }
    public void SpecAttack()
    {
        _aiManager.DealDamage(40f);
        EndTurn();
    }
    public void Regen()
    {
        Heal(30f);
        EndTurn();
    }
    public void Flee()
    {
        _aiManager.DealDamage(_maxHealth);
        DealDamage(_maxHealth);
        EndTurn();
    }
    
}
