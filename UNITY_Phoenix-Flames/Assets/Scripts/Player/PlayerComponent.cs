using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent
{
    private PlayerManager manager;
    public virtual void Initialize(PlayerManager pm)
    {
        manager = pm;
    }
}
