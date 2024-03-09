using UnityEngine;

public class MockBallPhysics : MonoBehaviour
{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    /// Ball Physics Members
    [SerializeField]
    Vector3 m_InitialSpeed;

    [SerializeField]
    Vector3 m_InitialAcceleration;
    /// Ball Physics Members

    private BallPhysics m_BallPhysics;
    public BallPhysics BallPhysics
    {
        get { return m_BallPhysics; }
        private set { m_BallPhysics = value; }
    }

    private void Awake()
    {
        /// Init Constraint members
        m_BallPhysics.m_InitialSpeed = m_InitialSpeed;
        m_BallPhysics.m_InitialAcceleration = m_InitialAcceleration;
        /// Init Constraint members
    }

#endif
}
