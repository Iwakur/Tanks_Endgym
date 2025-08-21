public enum TankModule
{
    Hull,
    Turret,
    Tracks,
    Optics,
    Ammo,
    Engine
    
}

[System.Serializable]
public class Module
{
    public TankModule type;
    public float maxHP;
    public float currentHP;
    public bool isDestroyed;

    public Module(TankModule type, float hp)
    {
        this.type = type;
        maxHP = hp;
        currentHP = hp;
    }

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0 && !isDestroyed)
            isDestroyed = true;
    }
}
