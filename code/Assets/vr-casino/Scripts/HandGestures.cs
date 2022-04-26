using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum ETypeOfAction
{
    None = -1,
    HitOrDeal,
    Stand
}

public class HandGestures : MonoBehaviour
{
    int collisionCount;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameManager gameManager;
    bool enableTheCollision = true;


    public SteamVR_Input_Sources m_RightTargetSource;
    public SteamVR_Action_Boolean m_RightGrabAction, m_RightPinchAction;

    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        Debug.Log("GrabAction" + m_RightGrabAction.GetState(m_RightTargetSource));
        Debug.Log("PinchAction" + m_RightGrabAction.GetState(m_RightTargetSource));
        if (collision.gameObject.tag == "HandCollider"&&enableTheCollision && (m_RightGrabAction.GetState(m_RightTargetSource)|| m_RightPinchAction.GetState(m_RightTargetSource)))
        {
            Debug.Log("ConditionFulfiled");
            enableTheCollision = false;
            Invoke("EnableCollision", 0.1f);
            StartCoroutine(DoHandGesture(m_RightGrabAction.GetState(m_RightTargetSource) ? ETypeOfAction.HitOrDeal : m_RightPinchAction.GetState(m_RightTargetSource) ? ETypeOfAction.Stand : ETypeOfAction.None));
        }

    }

    private void EnableCollision()
    {
        enableTheCollision = true;
    }

    private IEnumerator DoHandGesture(ETypeOfAction eTypeOfAction)
    {
        collisionCount++;
        Debug.Log("calling" + collisionCount);
        yield return new WaitForSeconds(2f);
        if (eTypeOfAction == ETypeOfAction.HitOrDeal)
            HitOrDeal();
        else if (eTypeOfAction == ETypeOfAction.Stand)
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
