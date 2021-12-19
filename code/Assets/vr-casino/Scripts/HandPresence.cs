using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum EHandType
{
    None=-1,
    RightHand,
    LeftHand
}
public class HandPresence : MonoBehaviour
{
    [SerializeField] private EHandType m_HandType; 
    private InputDevice m_CurrentHand;
    [SerializeField] private GameObject m_HandModelPrefab;
    [SerializeField] private Animator m_HandAnimation;

    private GameObject m_SpawnedController;
    private GameObject m_SpawnedHandmodel;

    

    void Start()
    {
        m_CurrentHand = GetHand(InputDeviceCharacteristics.Controller | (m_HandType == EHandType.RightHand ? InputDeviceCharacteristics.Right : InputDeviceCharacteristics.Left));
        m_SpawnedHandmodel = Instantiate(m_HandModelPrefab, transform);
    }

    private InputDevice GetHand(InputDeviceCharacteristics a_DeviceCharacteristics)
    {
        List<InputDevice> t_Devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(a_DeviceCharacteristics, t_Devices);
        return t_Devices[0];
    }

    private void DoHandAnim()
    {
        m_CurrentHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
            Debug.Log("Pressing Primary Button");
        m_CurrentHand.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (triggerValue > 0.1f)
        {
            Debug.Log("Trigger pressed " + triggerValue);
            m_HandAnimation.SetFloat("Trigger", triggerValue);
        }
        m_CurrentHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        if (primary2DAxisValue != Vector2.zero)
            Debug.Log("Primary Touchpad" + primary2DAxisValue);
        m_CurrentHand.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        if (gripValue > 0.1f)
        {
            Debug.Log("Primary Touchpad" + gripValue);
            m_HandAnimation.SetFloat("Grip", gripValue);
        }
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
        DoHandAnim();
    }

}
