using Unity.VisualScripting;
using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager>
{
    static float GRAVITIONAL_ACCELERATION = 9.80665f;
    static float HALF_GRAVITIONAL_ACCELERATION = 4.903325f;

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
    private Vector3 m_GravitionalAcceleration = new Vector3(0.0f, GRAVITIONAL_ACCELERATION, 0.0f);
    public Vector3 GravitionalAcceleration
    {
        get { return m_GravitionalAcceleration; }
        private set { m_GravitionalAcceleration = value; }
    }

    [SerializeField]
    private Vector3 m_HalfGravitionalAcceleration = new Vector3(0.0f, HALF_GRAVITIONAL_ACCELERATION, 0.0f);
    public Vector3 HalfGravitionalAcceleration
    {
        get { return m_HalfGravitionalAcceleration; }
        private set { m_HalfGravitionalAcceleration = value; }
    }
}
