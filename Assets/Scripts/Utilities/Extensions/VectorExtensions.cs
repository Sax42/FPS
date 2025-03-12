using UnityEngine;

public class VectorExtensions
{
    public static Vector3 Direction(Vector3 pStart,Vector3 pEnd)
    {
        return pEnd - pStart;
    }

    public Vector3 RotateAroundPoint(Vector3 point,Vector3 pivot,Vector3 axis,float angle)
    {
        return Quaternion.AngleAxis(angle,axis) * (point - pivot) + pivot;
    }

    public static Vector3 Divide(Vector3 pFirstVector,Vector3 pSecondVector)
    {
        Vector3 lResult = Vector3.zero;
        if(pFirstVector.x != 0 && pSecondVector.x != 0)
            lResult.x = pFirstVector.x / pSecondVector.x;
        if(pFirstVector.y != 0 && pSecondVector.y != 0)
            lResult.y = pFirstVector.y / pSecondVector.y;
        if(pFirstVector.z != 0 && pSecondVector.z != 0)
            lResult.z = pFirstVector.z / pSecondVector.z;
        return lResult;
    }

    public static Vector3 Multiply(Vector3 pFirstVector,Vector3 pSecondVector)
    {
        Vector3 lResult = Vector3.zero;
        lResult.x = pFirstVector.x * pSecondVector.x;
        lResult.y = pFirstVector.y * pSecondVector.y;
        lResult.z = pFirstVector.z * pSecondVector.z;
        return lResult;
    }

    public static Vector3 ClampMax(Vector3 pVectorToClamp,Vector3 pMaxValue)
    {
        Vector3 lResult = Vector3.zero;
        lResult.x = pVectorToClamp.x > pMaxValue.x ? pMaxValue.x : pVectorToClamp.x;
        lResult.y = pVectorToClamp.y > pMaxValue.y ? pMaxValue.y : pVectorToClamp.y;
        lResult.y = pVectorToClamp.z > pMaxValue.z ? pMaxValue.z : pVectorToClamp.z;

        return lResult;
    }

    public static Vector3 SetToValueUnderThreshold(Vector3 pValueToCheck,Vector3 pValueToSetTo,Vector3 pThreshold,
        bool pSetIfUnder = true,bool pAbsoluteValue = false)
    {
        Vector3 lValue = pValueToCheck;
        lValue.x = MathExtensions.SetToValueUnderThreshold(lValue.x,pValueToSetTo.x,pThreshold.x,pSetIfUnder,
            pAbsoluteValue);
        lValue.y = MathExtensions.SetToValueUnderThreshold(lValue.y,pValueToSetTo.y,pThreshold.y,pSetIfUnder,
            pAbsoluteValue);
        lValue.z = MathExtensions.SetToValueUnderThreshold(lValue.z,pValueToSetTo.z,pThreshold.z,pSetIfUnder,
            pAbsoluteValue);
        return lValue;
    }

    public static Vector3 SetToValueUnderThreshold(float pValueToCheck,float pValueToSetTo,float pThreshold,
        bool pSetIfUnder = true,bool pAbsoluteValue = false)
    {
        Vector3 lValue = SetToValueUnderThreshold(pValueToCheck * Vector3.one,pValueToSetTo * Vector3.one,
            pThreshold * Vector3.one,pSetIfUnder,pAbsoluteValue);
        return lValue;
    }

    public static Vector3 CalculateOffset(Vector3 pStartPos,Vector3 pEndPos)
    {
        Vector3 lResult = pEndPos - pStartPos;
        return lResult;
    }

    public static Vector3 ClampMax(Vector3 pVectorToClamp,float pMaxValue)
    {
        return ClampMax(pVectorToClamp,pMaxValue * Vector3.one);
    }

    public static Vector3 SnapTo0UnderThreshold(Vector3 pFirstVector,float pThreshold)
    {
        return SnapTo0UnderThreshold(pFirstVector,pThreshold * Vector3.one);
    }

    public static Vector3 SnapTo0UnderThreshold(Vector3 pFirstVector,Vector3 pThreshold)
    {
        Vector3 lResult = Vector3.zero;
        lResult.x = MathExtensions.SnapTo0UnderThreshold(pFirstVector.x,pThreshold.x);
        lResult.y = MathExtensions.SnapTo0UnderThreshold(pFirstVector.y,pThreshold.y);
        lResult.z = MathExtensions.SnapTo0UnderThreshold(pFirstVector.z,pThreshold.z);
        return lResult;
    }

    public static Vector3 RoundXDecimals(Vector3 pVector,int pDecimalsToRemove)
    {
        Vector3 lResult = Vector3.zero;
        lResult.x = Mathf.Floor(Mathf.Log10(pVector.x)) + 1;
        lResult.y = (float)Mathf.FloorToInt(pVector.y * pDecimalsToRemove * 10) / pDecimalsToRemove * 10;
        lResult.z = (float)Mathf.FloorToInt(pVector.z * pDecimalsToRemove * 10) / pDecimalsToRemove * 10;
        return lResult;
    }

    public static Vector3 RoundKeepXDecimals(Vector3 pVector,int pDecimalsToKeep)
    {
        Vector3 lResult = pVector;
        lResult.x = MathExtensions.TruncateFloat(lResult.x,pDecimalsToKeep);
        lResult.y = MathExtensions.TruncateFloat(lResult.y,pDecimalsToKeep);
        lResult.z = MathExtensions.TruncateFloat(lResult.z,pDecimalsToKeep);
        return lResult;
    }

    public static Vector3 Replace(Vector3 pFirstVector,Vector3 pSecondVector,Vector3 pBool)
    {
        Vector3 lVec = pFirstVector;
        if(pBool.x > 0)
            lVec.x = pSecondVector.x;
        if(pBool.y > 0)
            lVec.y = pSecondVector.y;
        if(pBool.z > 0)
            lVec.z = pSecondVector.z;
        return lVec;
    }

    /// <summary>
    ///     check if each value of the first Vector are superior To corresponding Value of 2nd vector
    /// </summary>
    /// <param name="pFirstVector"></param>
    /// <param name="pSecondVector"></param>
    public static Vector3 CompareSuperiority(Vector3 pFirstVector,Vector3 pSecondVector)
    {
        Vector3 lResult = Vector3.zero;
        lResult.x = pFirstVector.x > pSecondVector.x ? 1 : 0;
        lResult.y = pFirstVector.y > pSecondVector.y ? 1 : 0;
        lResult.z = pFirstVector.z > pSecondVector.z ? 1 : 0;
        return lResult;
    }

    public static Vector3 Absolute(Vector3 pFirstVector)
    {
        Vector3 lResult = Vector3.zero;
        lResult.x = Mathf.Abs(pFirstVector.x);
        lResult.y = Mathf.Abs(pFirstVector.y);
        lResult.z = Mathf.Abs(pFirstVector.z);
        return lResult;
    }

    public static Vector3 Vector2ToVector3(Vector2 pVector,float pZValue)
    {
        Vector3 lVector = new(pVector.x,pVector.y,pZValue);
        return lVector;
    }
}