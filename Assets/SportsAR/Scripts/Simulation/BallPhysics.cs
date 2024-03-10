using System;
using UnityEngine;
using UnityEngine.UIElements;

public class BallPhysics : MonoBehaviour
{
    private Vector3 m_InitialSpeed;
    public Vector3 InitialSpeed
    {
        get { return m_InitialSpeed; }
        set { m_InitialSpeed = value; }
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
