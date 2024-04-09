using System;
using System.Threading.Tasks;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityType entityType;
    public Action<float> Push;
    public Action<float> Stun;
    public Action<float> Contain;
    
    [SerializeField] private float health;
    private float currentHealth;

    protected void InitializeEntity()
    {
        InitHealth();
        InitSpeed();
    }

    public float CurrentHealth()
    {
        return currentHealth;
    }

    private void InitHealth()
    {
        currentHealth = health; 
        Debug.Log(currentHealth);
    }
    
    public virtual void UpdateHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth <= 0)
        {
            //Dead behavior.
        }
        
    }

    [SerializeField] private float speed;
    private float currentSpeed;
    public float CurrentSpeed()
    {
        return currentSpeed;
    }
    private void InitSpeed() => currentSpeed = speed;

    public virtual async void UpdateSpeed(float amount, float duration = 0f)
    {
        float backupSpeed = currentSpeed;
        currentSpeed += amount;
        
        if (duration > 0)
        {
            int durationInMilliseconds = Mathf.FloorToInt(duration * 1000);
            await Task.Delay(durationInMilliseconds);
            currentSpeed = backupSpeed;
        }
    }
}

public enum EntityType
{
    Player,
    Amical,
    Hostile
}