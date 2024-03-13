using UnityEngine;

public class CricketMockSettings : Singleton<CricketMockSettings>
{
    [SerializeField]
    private float m_BallInitSpeed = 44.44f;
    public float BallInitSpeed
    {
        get { return m_BallInitSpeed; }
        private set { m_BallInitSpeed = value; }
    }
}
