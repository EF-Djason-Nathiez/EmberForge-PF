using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
{
    public static T instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            Debug.Log($"This singleton already exist so {this} have been deleted");
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = GetComponent<T>();
        }
    }
}