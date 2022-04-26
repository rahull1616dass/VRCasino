using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;


public class VRInputModule : BaseInputModule
{
    public Camera m_RightCamera;
    public SteamVR_Input_Sources m_RightTargetSource;
    public SteamVR_Action_Boolean m_RightClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    protected override void Awake()
    {
        base.Awake();
        m_Data = new PointerEventData(eventSystem);
    }
    public override void Process()
    {
        m_Data.Reset();
        m_Data.position = new Vector2(m_RightCamera.pixelWidth / 2, m_RightCamera.pixelHeight / 2);//m_RightCamera.WorldToScreenPoint(m_RightCamera.transform.position);

        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        m_RaycastResultCache.Clear();

        HandlePointerExitAndEnter(m_Data, m_CurrentObject);


        Debug.Log(m_RightClickAction.GetState(m_RightTargetSource));
        if (m_RightClickAction.GetStateDown(m_RightTargetSource))
            ProcessPress(m_Data);

        if (m_RightClickAction.GetStateUp(m_RightTargetSource))
            ProcessRelease(m_Data);
    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        //Debug.Log(newPointerPress.name);

        if (newPointerPress == null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;
    }

    private void ProcessRelease(PointerEventData data)
    {
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        if (data.pointerPress == pointerUpHandler)
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;

    }
}
