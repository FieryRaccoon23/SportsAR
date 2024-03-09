using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null) m_Instance = (T)FindFirstObjectByType(typeof(T));
            if (m_Instance == null) Debug.LogError("An instance of " + typeof(T) + " is needed in the scene, but there is none.");
            return m_Instance;
        }
    }

    protected void Awake()
    {
        if (m_Instance == null) m_Instance = this as T;
        else if (m_Instance != this) Destroy(this);
    }
}