using System;
using Player;
using Object = UnityEngine.Object;

[Serializable] public class AbilityManager : PlayerComponent
{
    public Ability primary;
    public Ability secondary;
    public Ability first;
    public Ability second;

    public override void Initialize(PlayerManager pm)
    {
        base.Initialize(pm);
        
        Ability instancePrimary = Object.Instantiate(primary);
        primary = instancePrimary;
        primary.Initialize();
    }
}
