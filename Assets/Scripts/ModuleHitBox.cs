using UnityEngine;

public class ModuleHitbox : MonoBehaviour
{
    public TankModule module;
    private TankMechanics tank;

    void Start()
    {
        tank = GetComponentInParent<TankMechanics>();
    }

    public void TakeDamage(int damage)
    {
        if (tank != null)
            tank.HitModule(module, damage);
    }
}
