using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    private TurretData selectTurretData;

    public void OnLaserSelect(bool isOn)
    {
        if (isOn)
        {
            selectTurretData = laserTurretData;
        }
    }

    public void OnMissileSelect(bool isOn)
    {
        if (isOn)
        {
            selectTurretData = missileTurretData;
        }
    }

    public void OnStandardSelect(bool isOn)
    {
        if (isOn)
        {
            selectTurretData = standardTurretData;
        }
    }
}