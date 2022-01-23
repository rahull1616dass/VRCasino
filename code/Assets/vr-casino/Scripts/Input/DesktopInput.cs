using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DesktopInput : MonoBehaviour
{
    [SerializeField] private InputActionReference m_Rotation, m_Translate;

    [SerializeField] private float m_RotationSpeed = 60, m_TranslateSpeed = 1f;
    private float m_azimuth = 0;
    private float m_inclination = 0;

    private void Update()
    {
        Rotate();
        Translate();
    }

    private void Translate()
    {
        if (m_Translate.action == null)
            return;
        var t_position = transform.position;
        var t_delta = m_Translate.action.ReadValue<Vector3>();
        t_delta = transform.TransformDirection(t_delta);
        t_position += t_delta * (m_TranslateSpeed * Time.deltaTime);
        transform.position = t_position;
    }

    private void Rotate()
    {
        if (m_Rotation.action == null)
            return;
        var t_delta = m_Rotation.action.ReadValue<Vector2>();
        m_azimuth = (m_azimuth + t_delta.x * m_RotationSpeed * Time.deltaTime) % 360;
        m_inclination = Mathf.Clamp(m_inclination + t_delta.y * m_RotationSpeed * Time.deltaTime, -90, 90);
        transform.rotation = Quaternion.Euler(m_inclination, m_azimuth, 0);
    }
}
