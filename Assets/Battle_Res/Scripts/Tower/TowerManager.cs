using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerManager : Singleton<TowerManager>
{
    public Text allCoinText;
    public int currentMoney = 200;
    public Text coinText;
    public TurretDate missileTurret;
    public TurretDate turret;
    public TurretDate laserTurret;
    public TurretDate selectedTurret;

    [HideInInspector] public MapCube isSameMapCube;
    [HideInInspector] public MapCube mapCube;

    void Start()
    {
        selectedTurret = missileTurret;
        coinText.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("MapCube")))
                {
                    mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.transform.childCount<=0 && selectedTurret != null)
                    {
                        if (currentMoney >= selectedTurret.normalCost)
                        {
                            ChangeMoney(selectedTurret.normalCost);
                            mapCube.CreateTurret(selectedTurret, hit.collider.transform);
                        }
                        else
                        {
                            ShowCoinText();
                        }
                    }
                    else if (mapCube.transform.childCount > 0 && mapCube.currentTurret.turretType==selectedTurret.turretType)
                    {
                        if (currentMoney >= selectedTurret.upgradeCost)
                        {
                            isSameMapCube = mapCube;
                        }
                        else
                        {
                            ShowCoinText();
                        }
                        
                    }
                }
            }
        }
    }

    public void SetCoinText()
    {
        allCoinText.text = currentMoney.ToString();
    }

    void ShowCoinText()
    {
        coinText.enabled = true;
        Invoke("HideCoinText", 2);
    }

    void HideCoinText()
    {
        coinText.enabled = false;
    }

    public void MissileTurretToggle()
    {
        selectedTurret = missileTurret;
    }

    public void TurretToggle()
    {
        selectedTurret = turret;
    }

    public void LaserTurretToggle()
    {
        selectedTurret = laserTurret;
    }

    public void ChangeMoney(int money)
    {
        currentMoney -= money;
        SetCoinText();
    }

    public void DestroyTurretMethod()
    {
        isSameMapCube.DestroyTurret();
    }
}
