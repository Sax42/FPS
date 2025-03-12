using System.Collections.Generic;
using UnityEngine;

public class DebugExtension: MonoBehaviour
{
    public static void DebugList(List<object> pList)
    {
        foreach(object t in pList)
        {
            Debug.Log(t);
        }
    }

    public static void DebugDictionary<TKey, TValue>(Dictionary<TKey,TValue> dictionary)
    {
        if(dictionary == null)
        {
            Debug.Log("Dictionary is null.");
            return;
        }

        Debug.Log("Dictionary contents:");
        foreach(KeyValuePair<TKey,TValue> kvp in dictionary)
        {
            string keyStr = kvp.Key != null ? kvp.Key.ToString() : "null";
            string valueStr = kvp.Value != null ? kvp.Value.ToString() : "null";
            Debug.Log("Key: " + keyStr + " | Value: " + valueStr);
        }
    }

    /// <summary>
    /// Draws a box using Debug.DrawLine given a center and size.
    /// </summary>
    public static void DrawBox(Vector3 center,Vector3 size,Color color,float duration = 0f)
    {
        Vector3 halfSize = size * 0.5f;

        // Calculate the eight corners of the box.
        Vector3 p0 = center + new Vector3(-halfSize.x,-halfSize.y,-halfSize.z);
        Vector3 p1 = center + new Vector3(halfSize.x,-halfSize.y,-halfSize.z);
        Vector3 p2 = center + new Vector3(halfSize.x,-halfSize.y,halfSize.z);
        Vector3 p3 = center + new Vector3(-halfSize.x,-halfSize.y,halfSize.z);
        Vector3 p4 = center + new Vector3(-halfSize.x,halfSize.y,-halfSize.z);
        Vector3 p5 = center + new Vector3(halfSize.x,halfSize.y,-halfSize.z);
        Vector3 p6 = center + new Vector3(halfSize.x,halfSize.y,halfSize.z);
        Vector3 p7 = center + new Vector3(-halfSize.x,halfSize.y,halfSize.z);

        // Draw bottom face.
        Debug.DrawLine(p0,p1,color,duration);
        Debug.DrawLine(p1,p2,color,duration);
        Debug.DrawLine(p2,p3,color,duration);
        Debug.DrawLine(p3,p0,color,duration);

        // Draw top face.
        Debug.DrawLine(p4,p5,color,duration);
        Debug.DrawLine(p5,p6,color,duration);
        Debug.DrawLine(p6,p7,color,duration);
        Debug.DrawLine(p7,p4,color,duration);

        // Draw vertical lines.
        Debug.DrawLine(p0,p4,color,duration);
        Debug.DrawLine(p1,p5,color,duration);
        Debug.DrawLine(p2,p6,color,duration);
        Debug.DrawLine(p3,p7,color,duration);
    }

    /// <summary>
    /// Draws a single line from the start point to the end point.
    /// </summary>
    /// <param name="start">The starting point in world space.</param>
    /// <param name="end">The ending point in world space.</param>
    /// <param name="color">The color of the line.</param>
    /// <param name="duration">How long (in seconds) the line will be visible. 0 means one frame.</param>
    public static void DrawLine(Vector3 start,Vector3 end,Color color,float duration = 0f)
    {
        Debug.DrawLine(start,end,color,duration);
    }

    /// <summary>
    /// Draws a polyline by connecting each consecutive pair of points from the provided list.
    /// </summary>
    /// <param name="points">A list of Vector3 points to connect.</param>
    /// <param name="color">The color of the polyline.</param>
    /// <param name="duration">How long (in seconds) the lines will be visible. 0 means one frame.</param>
    public static void DrawPolyline(List<Vector3> points,Color color,float duration = 0f)
    {
        if(points == null || points.Count < 2)
        {
            Debug.LogWarning("DrawPolyline requires at least 2 points.");
            return;
        }

        // Reuse DrawLine for each pair of points.
        for(int i = 0 ; i < points.Count - 1 ; i++)
        {
            DrawLine(points[i],points[i + 1],color,duration);
        }
    }

    /// <summary>
    /// Returns a new visualization of a BoxCollider by converting its local center and size into world space.
    /// </summary>
    public static void DrawColliderBox(BoxCollider boxCollider,Color color,float duration = 0f)
    {
        if(boxCollider == null)
        {
            Debug.LogWarning("BoxCollider is null.");
            return;
        }

        // Convert the collider's center to world space.
        Vector3 worldCenter = boxCollider.transform.TransformPoint(boxCollider.center);
        // Account for scaling.
        Vector3 worldSize = Vector3.Scale(boxCollider.size,boxCollider.transform.lossyScale);
        DrawBox(worldCenter,worldSize,color,duration);
    }

    /// <summary>
    /// Returns a new visualization of a SphereCollider by drawing circles in three axes.
    /// </summary>
    public static void DrawColliderSphere(SphereCollider sphereCollider,Color color,float duration = 0f)
    {
        if(sphereCollider == null)
        {
            Debug.LogWarning("SphereCollider is null.");
            return;
        }

        // Convert the collider's center to world space.
        Vector3 worldCenter = sphereCollider.transform.TransformPoint(sphereCollider.center);
        // Adjust radius according to the maximum scale axis.
        float worldRadius = sphereCollider.radius * Mathf.Max(
            sphereCollider.transform.lossyScale.x,
            sphereCollider.transform.lossyScale.y,
            sphereCollider.transform.lossyScale.z);

        // Draw three circles to approximate a sphere.
        DrawCircle(worldCenter,worldRadius,Vector3.up,color,duration);
        DrawCircle(worldCenter,worldRadius,Vector3.right,color,duration);
        DrawCircle(worldCenter,worldRadius,Vector3.forward,color,duration);
    }

    /// <summary>
    /// Draws a circle in the plane defined by the given axis.
    /// </summary>
    private static void DrawCircle(Vector3 center,float radius,Vector3 axis,Color color,float duration,int segments = 36)
    {
        Vector3 normal = axis.normalized;
        // Determine a vector perpendicular to the axis.
        Vector3 tangent = Vector3.Cross(normal,Vector3.up);
        if(tangent.sqrMagnitude < 0.001f)
        {
            tangent = Vector3.Cross(normal,Vector3.right);
        }
        tangent.Normalize();
        Vector3 bitangent = Vector3.Cross(normal,tangent);

        float angleStep = 360f / segments;
        Vector3 prevPoint = center + tangent * radius;
        for(int i = 1 ; i <= segments ; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 newPoint = center + (tangent * Mathf.Cos(angle) + bitangent * Mathf.Sin(angle)) * radius;
            Debug.DrawLine(prevPoint,newPoint,color,duration);
            prevPoint = newPoint;
        }
    }
}