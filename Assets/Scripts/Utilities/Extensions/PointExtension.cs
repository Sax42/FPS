using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point: MonoBehaviour
{
    public Vector3 position;
    public List<string> tags = new List<string>();
}

public class PointExtension: MonoBehaviour
{
    public static List<Point> ConvertVector3ToPoint(List<Vector3> lAllVector3)
    {
        List<Point> lAllPoints = new List<Point>();
        int lLength = lAllVector3.Count;
        Point lPoint;
        for(int i = 0 ; i < lLength ; i++)
        {
            lPoint = new Point();
            lPoint.position = lAllVector3[i];
            lAllPoints.Add(lPoint);
        }

        return lAllPoints;
    }

    public static List<Point> SelectClosePoint(Point lPoint,List<Point> PointToLoook,float pSearcDistance,bool pExcludeClose)
    {
        List<Point> lList = new List<Point>();
        int lLength = PointToLoook.Count;
        Point lPointToCheck;
        float lDistance;
        for(int i = 0 ; i < lLength ; i++)
        {
            lPointToCheck = PointToLoook[i];
            lDistance = VectorExtensions.Direction(lPoint.position,lPointToCheck.position).magnitude;

            if(pExcludeClose ? lDistance < pSearcDistance : lDistance < pSearcDistance)
            {
                lList.Add(lPointToCheck);
            }
        }
        return lList;
    }

    public static List<Point> SelectClosePoint(List<Point> lPoint,float pSearcDistance,bool pExcludeClose)
    {
        List<Point> lList = new List<Point>();
        int lLength = lPoint.Count;
        for(int i = 0 ; i < lLength ; i++)
        {
            lList.AddRange(SelectClosePoint(lPoint[i],lPoint,pSearcDistance,pExcludeClose));
        }
        return lList;
    }
}