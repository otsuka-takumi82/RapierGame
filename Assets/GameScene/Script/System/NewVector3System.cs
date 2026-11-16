using UnityEngine;

public static class NewVector3System
{
    public static Vector3 GetNewVector3(this Vector3 needVector3, float x = 0, float y = 0, float z = 0, float xpile = 1, float ypile = 1, float zpile = 1)
    {
        return new Vector3((needVector3.x + x) * xpile, (needVector3.y + y) * ypile, (needVector3.z + z) * ypile);
    }
    public static Vector3 ChangeVector3(this Vector3 needVector3, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? needVector3.x, y ?? needVector3.y, z ?? needVector3.z);
    }
}
