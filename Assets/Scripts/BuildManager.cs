using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public int money = 1000;
    public Text moneyText;
    public Animator moneyAnim;
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

    private void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    public GameObject upgradeCanvas;
    public Button upgradeButton;
    public MapCube selectMapCube;

    private void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    private void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        Debug.Log("OK");
        selectMapCube.UpgradeTurret();
        HideUpgradeUI();
    }

    public void OnDestroyButtonDown()
    {
        Debug.Log("Cancel");
        selectMapCube.DestroyTurret();
        HideUpgradeUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool ishit = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (ishit)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.turrentGo == null && selectTurretData != null)
                    {
                        if (money >= selectTurretData.cost)
                        {
                            ChangeMoney(-selectTurretData.cost);
                            mapCube.BuildTurret(selectTurretData);
                        }
                        else
                        {
                            moneyAnim.SetTrigger("Filck");
                        }
                    }
                    else if (mapCube.turrentGo != null)
                    {
                        if (mapCube == selectMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            HideUpgradeUI();
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgrade);
                        }
                        selectMapCube = mapCube;
                    }
                }
            }
        }
    }
}