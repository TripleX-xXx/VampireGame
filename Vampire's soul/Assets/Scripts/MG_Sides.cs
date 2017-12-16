using UnityEngine;

public static class MG_Sides {

    public enum Side
    {
        up,
        down,
        left,
        right,
        none
    };

    public static IntVector2[] sideIntVector2 = {
        new IntVector2(0, 1),
        new IntVector2(0, -1),
        new IntVector2(-1, 0),
        new IntVector2(1, 0),
        new IntVector2(0, 0)
    };

    public static Vector3[] sideVector3 = {
        Vector3.up,
        Vector3.down,
        Vector3.left,
        Vector3.right,
        Vector3.zero
    };

    public static IntVector2 SideToIntVector2(Side side) // Transform the enum Side to its respective IntVector2
    {
        return sideIntVector2[(int)side];
    }

    public static Vector3 SideToVector3(Side side) // Transform the enum Side to its respective Vector3
    {
        return sideVector3[(int)side];
    }

    public static Side EulerVectorToSide(Vector3 vec) // Transform rotation as Vector3 to its respective the enum Side
    {
        if (vec.z > -0.1 && vec.z < 0.1) return Side.up;
        else if (vec.z > 89 && vec.z < 91) return Side.left;
        else if ((vec.z > -91 && vec.z < -89) || (vec.z > 269 && vec.z < 271)) return Side.right;
        else if (vec.z > 179 && vec.z < 181) return Side.down;
        return Side.none;
    }

}
