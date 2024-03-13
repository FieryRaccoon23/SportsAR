using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BallPhysics : MonoBehaviour
{
    private float m_Mass;
    public float Mass
    {
        get { return m_Mass; }
        set { m_Mass = value; }
    }

    private Vector3 m_InitialVelocity;
    public Vector3 InitialVelocity
    {
        get { return m_InitialVelocity; }
        set { m_InitialVelocity = value; }
    }

    private Vector3 m_CurrentSpeed;
    public Vector3 CurrentSpeed
    {
        get { return m_CurrentSpeed; }
        set { m_CurrentSpeed = value; }
    }

    private Vector3 m_CurrentAcceleration;
    public Vector3 CurrentAcceleration
    {
        get { return m_CurrentAcceleration; }
        set { m_CurrentAcceleration = value; }
    }
}
