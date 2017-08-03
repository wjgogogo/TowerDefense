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

    public GameObject upgradeCanvas;
    public Button upgradeButton;
    public Text upgradeText;
    private TurretData selectTurretData;

    private MapCube selectMapCube;

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

    public void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    private void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false, int upgradeMoney = 0)

    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
        if (upgradeButton.interactable)
        {
            upgradeText.text = "升级(￥" + upgradeMoney + ")";
        }
        else
        {
            upgradeText.text = "不能再升级";
        }
    }

    private void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectMapCube.turretData.costUpgrade)
        {
            ChangeMoney(-selectMapCube.turretData.costUpgrade);
            selectMapCube.UpgradeTurret();
            HideUpgradeUI();
        }
        else
        {
            moneyAnim.SetTrigger("Filck");
        }
    }

    public void OnDestroyButtonDown()
    {
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
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgrade, mapCube.turretData.costUpgrade);
                        }
                        selectMapCube = mapCube;
                    }
                }
            }
        }
    }
}