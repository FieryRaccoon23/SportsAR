using System.Collections.Generic;
using UnityEngine;

public class MockConstraints : MonoBehaviour
{
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    /// Constraint Members
    [SerializeField]
    private Transform m_Transforms;

    [Range(1, 100)]
    [SerializeField]
    private uint m_Order;

    /// Constraint Members

    private Constraints m_Contraint;
    public Constraints Constraint
    {
        get { return m_Contraint; }
        private set { m_Contraint = value;}
    }

    private void Awake()
    {
        m_Transforms = transform;

        /// Init Constraint members
        m_Contraint = new Constraints();
        m_Contraint.m_Transforms = m_Transforms;
        m_Contraint.m_Order = m_Order;
        /// Init Constraint members
    }
#endif
}
