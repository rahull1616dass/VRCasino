using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGestures : MonoBehaviour
{
    int collisionCount;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameManager gameManager;
    bool enableTheCollision = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HandCollider"&&enableTheCollision)
        {
            enableTheCollision = false;
            Invoke("EnableCollision", 0.3f);
            StartCoroutine(DoHandGesture());
        }

    }

    private void EnableCollision()
    {
        enableTheCollision = true;
    }

    private IEnumerator DoHandGesture()
    {
        collisionCount++;
        Debug.Log("calling" + collisionCount);
        yield return new WaitForSeconds(2f);
        if (collisionCount == 2)
            HitOrDeal();
        else if (collisionCount == 3)
            Stand();
        else
            collisionCount = 0;
    }

    private void Stand()
    {
        StopAllCoroutines();

        if (gameManager.CurrentState == GameState.ComputerTurn || gameManager.CurrentState == GameState.HumanTurn)
            uiManager.StandButton();
    }

    private void HitOrDeal()
    {
        StopAllCoroutines();
        if (gameManager.CurrentState == GameState.None)
            uiManager.DealButton();
        else if (gameManager.CurrentState == GameState.ComputerTurn || gameManager.CurrentState == GameState.HumanTurn)
            uiManager.HitGameButton();
    }
}
