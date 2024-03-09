using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BallProjectileWithContraints : MonoBehaviour
{
    private List<Constraints> m_Contraints;

    private void OnEnable()
    {
        DisableRigidBody();
    }

    private void Start()
    {
        GetConstraints();

        // There should be at least 1 constraint
        Assert.AreNotEqual(m_Contraints.Count, 0, "Constraints are zero.");

    }

    private void Update()
    {
    }

    void DisableRigidBody()
    {
        Rigidbody Body = GetComponent<Rigidbody>();
        if (Body)
        {
            Body.isKinematic = true;
            Body.detectCollisions = false;
        }
    }

    private void GetConstraints()
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        GameObject[] ConstraintGameObjects = GameObject.FindGameObjectsWithTag("Constraint");
        List<Constraints> RawContraints = new List<Constraints>();

        foreach (GameObject ConstraintGameObject in ConstraintGameObjects)
        {
            MockConstraints MockConstraint = ConstraintGameObject.GetComponent<MockConstraints>();
            if (MockConstraint)
            {
                Constraints Constraint = MockConstraint.Constraint;
                RawContraints.Add(Constraint);
            }
        }

        m_Contraints = new List<Constraints>(RawContraints);

        // Sort
        m_Contraints.Sort((a, b) => a.CompareTo(b));
#endif
    }
}
