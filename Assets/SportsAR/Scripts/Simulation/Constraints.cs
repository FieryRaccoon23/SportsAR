using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Constraints
{
    public Transform m_Transforms;
    public uint m_Order;
    public Vector3 m_LaunchDirection;

    public int CompareTo(Constraints Obj)
    {
        var A = this;
        var B = Obj;

        if (A.m_Order < B.m_Order)
            return -1;

        if (A.m_Order > B.m_Order)
            return 1;

        return 0;
    }
}