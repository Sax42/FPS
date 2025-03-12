using UnityEngine;

public class MathExtensions
{
    public static int GetNumberOfDigit(float pNumber)
    {
        int lResult = (int)Mathf.Floor(Mathf.Log10(pNumber)) + 1;
        return lResult;
    }

    public static float KeepXDigits(float pNumber,int pDigitToKeep)
    {
        float lResult = 0;
        int lDigitAmount = GetNumberOfDigit(pNumber);
        lResult = Mathf.Floor(pNumber * 10 * lDigitAmount) / (10 * lDigitAmount);
        return lResult;
    }

    public static float Lerp(float pA,float pB,float pF)
    {
        return (pA * (1.0f - pF)) + (pB * pF);
    }

    public static float FitRange(float pValue,Vector2 pFromRange,Vector2 pToRange)
    {
        float lResult = 0;
        float lGapStartRange = pFromRange.y - pFromRange.x;
        float lPercentageStartRange = pValue / lGapStartRange;
        float lGapEndRange = pToRange.y - pToRange.x;
        lResult = pToRange.x + lPercentageStartRange * lGapEndRange;
        return lResult;
    }

    public static float SetToValueUnderThreshold(float pValueToCheck,float pValueToSetTo,float pThreshold,
        bool pSetIfUnder = true,bool pAbsoluteValue = false)
    {
        float lResult = pValueToCheck;
        if(pAbsoluteValue)
            lResult = Mathf.Abs(lResult);
        if(pSetIfUnder)
            if(lResult < pThreshold)
                return pValueToSetTo;
        if(lResult > pThreshold)
            return pValueToSetTo;
        return pValueToCheck;
    }

    public static float SnapTo0UnderThreshold(float pValue,float pThreshold)
    {
        if(Mathf.Abs(pValue) < pThreshold)
            return 0;
        return pValue;
    }

    public static float TruncateFloat(float pValue,int pRecision)
    {
        // Convert to string to check actual decimal places
        string[] lParts = pValue.ToString().Split('.');

        // If there are fewer decimal places than precision, return the value as is
        if(lParts.Length < 2 || lParts[1].Length <= pRecision)
            return pValue;

        // Otherwise, truncate using math
        float lFactor = Mathf.Pow(10,pRecision);
        return Mathf.Floor(pValue * lFactor) / lFactor;
    }
}