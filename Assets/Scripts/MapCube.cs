using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turrentGo;

    public GameObject buildEffect;

    public void BuildTurret(GameObject turret)
    {
        Instantiate(turret, transform.position, Quaternion.identity);
        turrentGo = turret;
        GameObject effect = Instantiate(buildEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(effect, 0.5f);
    }
}