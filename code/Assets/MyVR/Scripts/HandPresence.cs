using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    [SerializeField] private bool m_ShowHand;
    private InputDevice m_RightHand;
    [SerializeField] private GameObject m_HandModelPrefab;

    private GameObject m_SpawnedController;
    private GameObject m_SpawnedHandmodel;

    void Start()
    {
        List<InputDevice> t_Devices = new List<InputDevice>();
        InputDeviceCharacteristics t_CharOfRight = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(t_CharOfRight, t_Devices);

        if (t_Devices.Count > 0)
        {
            m_RightHand = t_Devices[0];
            Debug.Log("Got the button");
        }

        m_SpawnedHandmodel = Instantiate(m_HandModelPrefab, transform);
    }

    private void Update()
    {
        /*m_RightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primarybuttonvalue);
        if (primarybuttonvalue)
            Debug.Log("PressingButton");
        if(m_RightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 AxisVal) && AxisVal!= Vector2.zero)
        {
            Debug.Log("Primary 2D>>>" + AxisVal);
        }*/
        if (m_ShowHand)
        {
            m_SpawnedHandmodel.SetActive(true);
        }
        else
        {
            m_SpawnedHandmodel.SetActive(false);
        }
    }
}
