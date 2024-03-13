using UnityEngine;
using UnityEngine.UIElements;

public class CricketManager : Singleton<CricketManager>
{
    [SerializeField]
    private bool m_StartSimulation = false;
    public bool StartSimulation
    {
        get { return m_StartSimulation; }
        set { m_StartSimulation = value; }
    }

    // Distance in meters
    [SerializeField]
    private float m_PitchLengthFromCrease = 17.68f;
    public float PitchLengthFromCrease
    {
        get { return m_PitchLengthFromCrease; }
        private set { m_PitchLengthFromCrease = value; }
    }

    [SerializeField]
    private float m_PitchLengthFromStumps = 20.12f;
    public float PitchLengthFromStumps
    {
        get { return m_PitchLengthFromStumps; }
        private set { m_PitchLengthFromStumps = value; }
    }

}
