using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Player.PlayerAbilities;
using Player.PlayerComponents;
using UnityEngine;
using Timer = Utilities.Timer;

public class AbilitySolver
{
    private Ability parent;

    private RaycastDetector _detector;
    
    public Timer cooldownTimer;
    private float timer;
    public Timer delayTimer;

    public AbilitySolver(Ability a)
    {
        parent = a;
        _detector = new RaycastDetector();
        cooldownTimer = new Timer();
        
        cooldownTimer.OnTimerEnded += () =>
        {
            parent.SetAbilityState(AbilityEnums.AbilityState.Ready);
        };
        cooldownTimer.OnTimerStart += () =>
        {
            parent.SetAbilityState(AbilityEnums.AbilityState.OnCooldown);
        };
    }

    public async void StartAbility()
    {
        if (parent._abilityState == AbilityEnums.AbilityState.OnUse)
        {
            Debug.Log("Already on using ability");
            return;
        } 
        
        parent.SetAbilityState(AbilityEnums.AbilityState.OnUse);
        
        TimeSpan t = TimeSpan.FromSeconds(parent.castDuration);
        await Task.Delay(t);

        SetEffect(FilterTargets(CollectTargets()));
    }
    
    Vector3 origin;
    private Vector3 direction;
    public GameObject[] CollectTargets()
    {
        GameObject[] t = new GameObject[1];
        
        switch (parent._targetingType)
        {
            case AbilityEnums.TargetingType.OnSelf:
                t[0] = GameRepository.instance.playerManager.player.gameObject;
                return t; 
            
            case AbilityEnums.TargetingType.Selected:
                t[0] = GameRepository.instance.playerManager.target.gameObject;
                return t; 
            
            case AbilityEnums.TargetingType.RaycastDetection:
                GetOriginAndDirection();

                switch (parent._raycastType)
                {
                    case AbilityEnums.RaycastType.FrontRay:
                        t[0] = _detector.GetObjectInFrontRay(origin, direction, parent.detectionLenght);
                        return t;
                    
                    case AbilityEnums.RaycastType.FrontCone:
                        return _detector.GetObjectsInFrontCone(origin, direction, parent.detectionRadius,
                            parent.detectionLenght);
                    
                    case AbilityEnums.RaycastType.FrontRectangle:
                        return _detector.GetObjectsInFrontRectangle(origin, parent.detectionWidth,
                            parent.detectionHeight);
                    
                    case AbilityEnums.RaycastType.AroundCircle:
                        return _detector.GetObjectsAroundInCircle(origin, parent.detectionRadius);
                }
                break; 
        }
        
        return null; 
    }

    private Entity[] FilterTargets(GameObject[] targets)
    {
        List<Entity> filtered = new List<Entity>();

        switch (parent._targetType)
        {
            case AbilityEnums.TargetType.All:
                for (int i = 0; i < targets.Length; i++)
                {
                    Entity e = targets[i].GetComponent<Entity>();
                    Debug.Log(e);
                    if (e)
                    {
                        filtered.Add(e);
                    }
                }

                return filtered.ToArray();
            
            case AbilityEnums.TargetType.Amical:
                for (int i = 0; i < targets.Length; i++)
                {
                    Entity e = targets[i].GetComponent<Entity>();

                    if (e && e.entityType == EntityType.Amical)
                    {
                        filtered.Add(e);
                    }
                }

                return filtered.ToArray();
            
            case AbilityEnums.TargetType.Hostile:
                for (int i = 0; i < targets.Length; i++)
                {
                    Entity e = targets[i].GetComponent<Entity>();

                    if (e && e.entityType == EntityType.Hostile)
                    {
                        filtered.Add(e);
                    }
                }

                return filtered.ToArray();
        }

        return null;
    }
    
    public void SetEffect(Entity[] filteredTargets)
    {
        if (filteredTargets.Length == 0 )
        {
            Debug.Log("entity not finds");
            Debug.Log(filteredTargets[0]);
            EndAbility();
        }
        else
        {
            for (int j = 0; j < filteredTargets.Length; j++) 
            {
                Entity entity = filteredTargets[j];
                
                for (int i = 0; i < parent.effects.Length; i++)
                {
                    Debug.Log(parent.effects.Length);
                    AbilityEffect effect = parent.effects[i];
                    ApplyEffect(entity, effect);
                }
            }
           
        }
    }
    
    public async void ApplyEffect(Entity entity, AbilityEffect e)
    {               
        switch (e._effectTempo)
        {
            case AbilityEnums.EffectTempo.Delayed:
                TimeSpan t = TimeSpan.FromSeconds(e.tempoDelay);
                await Task.Delay(t);
                break;
        }

        switch (e._effectApplication)
        {
            case AbilityEnums.EffectApplication.Once:
                EffectApplicationOn(entity, e);
                EndAbility();
                Debug.Log("Yo");
                break;
            
            case AbilityEnums.EffectApplication.OverTime:
                PhysicManager.instance.OnFixedUpdate += () =>  OvertimeApplication(entity, e);
                break;
        }
        
    }

    private void OvertimeApplication(Entity entity, AbilityEffect e)
    {
        Debug.Log("OT Effect");
        timer += Time.deltaTime;
                    
        if (timer % e.overtimeTickRate == 0)
        {
            EffectApplicationOn(entity, e);
        }

        if (timer <= e.overtimeDuration)
        {
            PhysicManager.instance.OnFixedUpdate -= () =>  OvertimeApplication(entity, e);
            EndAbility();
        }
    }

    private void EffectApplicationOn(Entity entity, AbilityEffect e)
    {
        float value;
        switch (e._effectType)
        {
            case AbilityEnums.EffectType.Amélioration: 
                value = +e.effectValue;
                switch (e._valueType)
                {
                    case AbilityEnums.ValueType.Damage:
                        break;
                    
                    case AbilityEnums.ValueType.Health:
                        entity.UpdateHealth(value);
                        break;
                    
                    case AbilityEnums.ValueType.Speed:
                        entity.UpdateSpeed(value);
                        break;
                }
                break;
                    
            case AbilityEnums.EffectType.Diminution:
                value = -e.effectValue;
                switch (e._valueType)
                {
                    case AbilityEnums.ValueType.Damage:
                        break;
                    
                    case AbilityEnums.ValueType.Health:
                        entity.UpdateHealth(value);
                        break;
                    
                    case AbilityEnums.ValueType.Speed:
                        entity.UpdateSpeed(value);
                        break;
                }
                break;
                    
            case AbilityEnums.EffectType.Altération:
                switch (e._alterationType)
                {
                    case AbilityEnums.AlterationType.Stun:
                        entity.Stun?.Invoke(e.alterationDuration); 
                        break;
                            
                    case AbilityEnums.AlterationType.Push:
                        entity.Push?.Invoke(e.alterationDuration);
                        break;
                    case AbilityEnums.AlterationType.Contain:
                        entity.Contain?.Invoke(e.alterationDuration);
                        break;
                }
                break;
        }
    }

    public void Instantiate()
    {
        
    }


    public void EndAbility()
    {
        cooldownTimer.StartTimer(parent.cooldownDuration);
        Debug.Log("Ability Over");
    }
    
    void GetOriginAndDirection()
    {
        switch (parent._raycastOrigin)
        {
            case AbilityEnums.RaycastOrigin.OnSelf:
                origin = GameRepository.instance.playerManager.player.position;
                direction = GameRepository.instance.playerManager.player.forward;
                break;
                            
            case AbilityEnums.RaycastOrigin.Selected:
                origin = GameRepository.instance.playerManager.target.position;
                direction = GameRepository.instance.playerManager.target.position - GameRepository.instance.playerManager.player.forward;
                break;
                            
            case AbilityEnums.RaycastOrigin.WorldPoint:
                //Input.GetMouseWorldPosition;
                break;
        }
    }
}
