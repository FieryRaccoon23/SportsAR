using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public struct Constraints
{
    public Transform m_Transforms;
    public uint m_Order;

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