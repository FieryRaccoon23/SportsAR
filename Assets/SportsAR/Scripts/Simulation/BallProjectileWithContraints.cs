using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

enum SimulationState
{ 
    Stopped,
    Running,
    Paused,
    Rewind
};

public class BallProjectileWithContraints : Singleton<BallProjectileWithContraints>
{
    private List<Constraints> m_Constraints;
    private Rigidbody BallRigidBodyComponent;
    private BallPhysics BallPhysicsComponent;
    private int m_CurrentStartConstraintIndex = 0;
    private int m_LastStartConstraintIndex = -1;
    private SimulationState m_SimulationState = SimulationState.Stopped;

    [SerializeField]
    float m_MockSpeedScaling = 0.5f;

    private void OnEnable()
    {
        DisableRigidBody();

        BallPhysicsComponent = GetComponent<BallPhysics>();

        //GetConstraints();

        //// There should be at least 2 constraint
        //Assert.AreNotEqual(m_Constraints.Count, 1, "Constraints are zero.");

        //for (int i = 0; i < m_Constraints.Count - 1; ++i)
        //{
        //    Constraints ConstraintStart = m_Constraints[i];
        //    Constraints ConstraintEnd = m_Constraints[i + 1];
        //    CalculateStartLaunchDirection(ref ConstraintStart, ConstraintEnd);
        //}
    }

    private void Start()
    {
        GetConstraints();

        // There should be at least 2 constraint
        Assert.AreNotEqual(m_Constraints.Count, 1, "Constraints are zero.");

        for (int i = 0; i < m_Constraints.Count - 1; ++i)
        {
            Constraints ConstraintStart = m_Constraints[i];
            Constraints ConstraintEnd = m_Constraints[i + 1];
            CalculateStartLaunchDirection(ref ConstraintStart, ConstraintEnd);
        }

        m_SimulationState = SimulationState.Stopped;
        //BeginSimulation();
    }

    private void FixedUpdate()
    {
        if(CricketManager.Instance != null) 
        {
            if(CricketManager.Instance.StartSimulation && m_SimulationState == SimulationState.Stopped)
            {
                BeginSimulation();
            }
        }

        UpdateSimulation();
    }

    private void DisableRigidBody()
    {
        BallRigidBodyComponent = GetComponent<Rigidbody>();
        if (BallRigidBodyComponent)
        {
            BallRigidBodyComponent.isKinematic = true;
            BallRigidBodyComponent.detectCollisions = false;
        }
    }

    private void GetConstraints()
    {
        m_Constraints = new List<Constraints>();
        // Add the first element
        Constraints FirstConstraint = new Constraints();
        FirstConstraint.m_Transforms = transform;
        FirstConstraint.m_Order = 0;
        m_Constraints.Add(FirstConstraint);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
        GameObject[] ConstraintGameObjects = GameObject.FindGameObjectsWithTag("Constraint");

        foreach (GameObject ConstraintGameObject in ConstraintGameObjects)
        {
            MockConstraints MockConstraint = ConstraintGameObject.GetComponent<MockConstraints>();
            if (MockConstraint)
            {
                Constraints Constraint = MockConstraint.Constraint;
                m_Constraints.Add(Constraint);
            }
        }

        // Sort
        m_Constraints.Sort((a, b) => a.CompareTo(b));
#endif
    }

    private void CalculateStartLaunchDirection(ref Constraints StartValue, Constraints EndValue)
    {
        StartValue.m_LaunchDirection = (EndValue.m_Transforms.position - StartValue.m_Transforms.position);
        // TODO: Maybe keep it squared
        StartValue.m_LaunchDirection.Normalize();
    }

    private float GetBallCurrentSpeed() 
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        return CricketMockSettings.Instance.BallInitSpeed;
#else
        return 0.0f;
#endif
    }

    private void InitVelocity(Constraints StartValue)
    {
        BallPhysicsComponent.InitialVelocity = StartValue.m_LaunchDirection * GetBallCurrentSpeed();
        BallPhysicsComponent.CurrentSpeed = BallPhysicsComponent.InitialVelocity;
    }

    private void ApplyForces()
    {
        BallPhysicsComponent.CurrentAcceleration = Vector3.zero;

        BallPhysicsComponent.CurrentAcceleration -= (PhysicsManager.Instance.GravitionalAcceleration * Time.fixedDeltaTime);
    }

    private void SimulateProjectileBetween2Constraints(Constraints StartValue, Constraints EndValue)
    {
        if (m_LastStartConstraintIndex != m_CurrentStartConstraintIndex)
        {
            m_LastStartConstraintIndex = m_CurrentStartConstraintIndex;
            InitVelocity(StartValue);
        }

        ApplyForces();

        // TODO: Check if fixed delta time needed
        transform.position += BallPhysicsComponent.CurrentSpeed * Time.fixedDeltaTime * m_MockSpeedScaling;

        // TODO: Use Verlet integration otherwise it can deviate

        // u = s/t

        Vector3 NewSpeed = BallPhysicsComponent.InitialVelocity + BallPhysicsComponent.CurrentAcceleration;

        BallPhysicsComponent.CurrentSpeed.Set(NewSpeed.x, NewSpeed.y, NewSpeed.z);

        //BallPhysicsComponent.CurrentSpeed.Set(BallPhysicsComponent.InitialSpeed.x,
        //                                      BallPhysicsComponent.InitialSpeed.y - (9.81f * Time.fixedDeltaTime),
        //                                      BallPhysicsComponent.InitialSpeed.z);

        //BallPhysicsComponent.CurrentSpeed = new Vector3(BallPhysicsComponent.InitialSpeed.x, BallPhysicsComponent.InitialSpeed.y - (9.81f * Time.fixedDeltaTime) , BallPhysicsComponent.InitialSpeed.z);

        //BallPhysicsComponent.CurrentSpeed = BallPhysicsComponent.InitialSpeed + BallPhysicsComponent.CurrentAcceleration;
    }

    private void PreUpdateSimulation()
    {

    }

    private void UpdateSimulation()
    {
        PreUpdateSimulation();

        if (m_SimulationState == SimulationState.Stopped)
        {
            return;
        }

        if (m_CurrentStartConstraintIndex >= m_Constraints.Count - 1)
        {
            EndSimulation();
            return;
        }

        Constraints StartValue = m_Constraints[m_CurrentStartConstraintIndex];
        Constraints EndValue = m_Constraints[m_CurrentStartConstraintIndex + 1];

        SimulateProjectileBetween2Constraints(StartValue, EndValue);

        PostUpdateSimulation();
    }

    private void PostUpdateSimulation()
    {
        Constraints StartValue = m_Constraints[m_CurrentStartConstraintIndex];
        Constraints EndValue = m_Constraints[m_CurrentStartConstraintIndex + 1];

        // TODO: RISKY
        // Check if ground hit
        Vector3 Offset = transform.position - EndValue.m_Transforms.position;
        float SqrLen = Offset.sqrMagnitude;
        if (SqrLen <= PhysicsManager.Instance.Epsilon1)
        {
            transform.position = EndValue.m_Transforms.position;
            m_CurrentStartConstraintIndex += 1;
        }
    }

    private void EndSimulation()
    {
        m_SimulationState = SimulationState.Paused;
    }

    //////////////////PUBLIC//////////////////
    public void BeginSimulation()
    {
        m_SimulationState = SimulationState.Running;
        m_CurrentStartConstraintIndex = 0;
        m_LastStartConstraintIndex = -1;
    }
}
