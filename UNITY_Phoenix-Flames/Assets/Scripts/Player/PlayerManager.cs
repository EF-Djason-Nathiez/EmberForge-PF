using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    protected PlayerComponent data, inputs, ability, inventory;

    private void InitializePlayer(){
        data.Initialize(this);
        inputs.Initialize(this);
        ability.Initialize(this);
        inventory.Initialize(this);
    }
}
