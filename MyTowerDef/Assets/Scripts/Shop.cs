using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standardTurret;
    public TurretBluePrint MageTower;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void  SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMageTower()
    {
        Debug.Log("Mage Tower Selected");
        buildManager.SelectTurretToBuild(MageTower);
    }
}
