using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    public float offset;
    public float laseroffset;
    public GameObject effect;
    public AudioClip createEffectClip;

    [HideInInspector]public GameObject turret;
    public bool isGrade = true;

    private Material material;
    private Color currentColor;
    [HideInInspector] public TurretDate currentTurret;

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        currentColor = material.color;
    }

    public void CreateTurret(TurretDate turretDate,Transform parent)
    {
        currentTurret = turretDate;
        isGrade = true;
        if (turretDate.turretType == TurretType.LaserTurret)
        {

            turret = Instantiate(turretDate.normalTurrent, transform.position + new Vector3(0, laseroffset, 0), Quaternion.identity);
            turret.transform.SetParent(parent);
        }
        else
        {

            turret = Instantiate(turretDate.normalTurrent, transform.position + new Vector3(0, offset, 0), Quaternion.identity);
            turret.transform.SetParent(parent);
        }
        GameObject effect = Instantiate(this.effect, transform.position + new Vector3(0, offset, 0), Quaternion.identity);
        effect.transform.rotation = Quaternion.Euler(-90, 0, 0);
        effect.GetComponent<ParticleSystem>().Play();
        AudioSource.PlayClipAtPoint(createEffectClip, transform.position);
    }

    public void UpgradeTurret()
    {
        MapCube mapCube = TowerManager.Instance.isSameMapCube;

        if (mapCube==null || !mapCube.isGrade) return;

        Destroy(mapCube.turret);

        GameObject effect = Instantiate(this.effect, mapCube.transform.position + new Vector3(0, offset, 0), Quaternion.identity);
        effect.transform.rotation = Quaternion.Euler(-90, 0, 0);
        effect.GetComponent<ParticleSystem>().Play();
        AudioSource.PlayClipAtPoint(createEffectClip, mapCube.transform.position);

        try
        {
            if (mapCube.GetComponentInChildren<Turret>().isLaser)
            {
                turret = Instantiate(mapCube.currentTurret.upgradeTurrent, mapCube.transform.position + new Vector3(0, laseroffset, 0), Quaternion.identity);
                turret.transform.SetParent(mapCube.transform);
                TowerManager.Instance.ChangeMoney(1000);
            }
            else
            {
                turret = Instantiate(mapCube.currentTurret.upgradeTurrent, mapCube.transform.position + new Vector3(0, offset, 0), Quaternion.identity);
                turret.transform.SetParent(mapCube.transform);
                TowerManager.Instance.ChangeMoney(1000);
            }

            mapCube.isGrade = false;
        }
        catch (Exception e)
        {

        }
    }

    public void DestroyTurret()
    {
        MapCube mapCube = TowerManager.Instance.isSameMapCube;
        if (mapCube != null)
        {
            Destroy(mapCube.transform.GetChild(0).gameObject);
            mapCube.turret = null;
            mapCube.currentTurret = null;
        }
    }

    void OnMouseEnter()
    {
        material.color = Color.red;
    }

    void OnMouseExit()
    {
        material.color = currentColor;
    }
}
