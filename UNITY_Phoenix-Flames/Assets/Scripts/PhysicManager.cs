using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicManager : MonoBehaviour
{
    public static PhysicManager instance;

    public Action OnUpdate;

    void Awake(){
        if(!instance){
            instance = this;
        }else{
            Destroy(this);
            Debug.WarningLog($"There is two instance of PhysicManager, please check {this.gameObject}.");
        }
    }
}
