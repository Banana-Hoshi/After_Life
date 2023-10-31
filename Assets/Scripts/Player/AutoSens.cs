using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class AutoSens : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = SensitivityAdjustment.xSens;
        vcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = SensitivityAdjustment.ySens;
    }
    private void Update()
    {
        if (!PlayerDisable.IsPlayerDisabled())
        {
            vcam.GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = SensitivityAdjustment.xSens;
            vcam.GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = SensitivityAdjustment.ySens;
        }
        else
        {
            vcam.GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = 0;
            vcam.GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        }
    }
}
