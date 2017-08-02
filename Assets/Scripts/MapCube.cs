using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turrentGo;

    public GameObject buildEffect;
    public GameObject destroyEffect;

    [HideInInspector]
    public bool isUpgrade = false;

    private TurretData turretData;
    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turret)
    {
        turrentGo = Instantiate(turret.turretPrefab, transform.position, Quaternion.identity);
        turretData = turret;
        isUpgrade = false;
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 0.5f);
    }

    private void OnMouseEnter()
    {
        if (turrentGo == null && !EventSystem.current.IsPointerOverGameObject())
        {
            render.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            render.material.color = Color.white;
        }
    }

    public void UpgradeTurret()
    {
        if (isUpgrade)
            return;
        Destroy(turrentGo);
        isUpgrade = true;
        turrentGo = Instantiate(turretData.turetPrefabUpgrade, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 0.5f);
    }

    public void DestroyTurret()
    {
        Destroy(turrentGo);
        GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 0.8f);
        isUpgrade = false;
        turrentGo = null;
        turretData = null;
    }
}