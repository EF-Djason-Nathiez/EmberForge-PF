using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerComponent
{
    public override void Initialize(PlayerManager pm){
        base.Initialize(pm);

        PhysicManager.instance.OnUpdate += Move;
    }

    private void Move(){

    }
}
