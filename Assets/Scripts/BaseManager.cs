using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseManager : MonoBehaviour
{
    //sterilisefeild means we can modify this in unity editor even if its private/protected
    [SerializeField] protected float _health = 100f;
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected Text _healthText;

    protected virtual void Start()
    {
        UpdateHealthText();
    }
    public abstract void TakeTurn();
    protected abstract void EndTurn();
    private void UpdateHealthText()
    {
        if(_healthText != null)
        {
            _healthText.text = _health.ToString();
        }
    }
    public void Heal(float heal)
    {
        _health = Mathf.Min(_health + heal, _maxHealth);
        UpdateHealthText();
    }

    public void DealDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        UpdateHealthText();
        if (_health <= 0)
        {
            _health = 0;
            Debug.Log("Dead af");
        }
    }
}
