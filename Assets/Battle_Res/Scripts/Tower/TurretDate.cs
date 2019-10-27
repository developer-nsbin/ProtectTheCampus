using UnityEngine;

[System.Serializable]
public class TurretDate
{
    public GameObject normalTurrent;
    public int normalCost;
    public GameObject upgradeTurrent;
    public int upgradeCost;
    public TurretType turretType;
}

public enum TurretType
{
    LaserTurret,
    MissileTurret,
    StandardTurret
}
