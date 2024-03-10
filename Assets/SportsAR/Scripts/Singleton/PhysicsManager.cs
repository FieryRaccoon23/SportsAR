using Unity.VisualScripting;
using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager>
{
    [SerializeField]
    private float m_Epsilon1 = 0.001f;
    public float Epsilon1
    {
        get { return m_Epsilon1; }
        private set { m_Epsilon1 = value; }
    }

    [SerializeField]
    private float m_COROfBall = 0.75f;
    public float COROfBall
    {
        get { return m_COROfBall; }
        private set { m_COROfBall = value;}
    }

    [SerializeField]
    private float m_GravitionalAcceleration = 9.80665f;
    public float GravitionalAcceleration
    {
        get { return m_GravitionalAcceleration; }
        private set { m_GravitionalAcceleration = value; }
    }

    [SerializeField]
    private float m_HalfGravitionalAcceleration = 4.903325f;
    public float HalfGravitionalAcceleration
    {
        get { return m_HalfGravitionalAcceleration; }
        private set { m_HalfGravitionalAcceleration = value; }
    }
}
