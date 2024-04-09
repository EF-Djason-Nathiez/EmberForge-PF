using System;
using Artificial_Intelligence;
using UnityEngine;

public class PhysicManager : MonoSingleton<PhysicManager>
{
    public Action OnUpdate;
    public Action OnFixedUpdate;
    
    private void Update()
    {
        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
        CheckDistance();
    }

    public Action<float, AI> OnDistance;

    private void CheckDistance()
    {
        if(GameRepository.instance.visibleNPC.Count <= 0) return;
        
        foreach (AI npc in GameRepository.instance.visibleNPC)
        {
            float d = Vector3.Distance(GameRepository.instance.playerManager.player.position,
                npc.go.transform.position);
            OnDistance?.Invoke(d, npc);
        }
    }

}
