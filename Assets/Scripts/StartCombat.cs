using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    [SerializeField] GameObject _combatCanvas;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log(collision.gameObject.name);
        MoveAi aiMove = collision.gameObject.GetComponent<MoveAi>();
        if (aiMove == null)
        {
            return;
        }
        Debug.Log("We have hit an Ai");
        _combatCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
