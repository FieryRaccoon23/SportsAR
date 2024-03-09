using Unity.VisualScripting;
using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager>
{
    [SerializeField]
    private float m_COROfBall = 0.75f;
    public float COROfBall
    {
        get { return m_COROfBall; }
        private set { m_COROfBall = value;}
    }
}
