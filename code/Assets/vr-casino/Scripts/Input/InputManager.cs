using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<InputActionAsset> m_ActionAssets;
    private void Start()
    {
        if (m_ActionAssets == null)
            return;
        foreach (var actionAsset in m_ActionAssets)
            if (actionAsset != null)
                actionAsset.Enable();
    }
}
