using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshExtensions: MonoBehaviour
{
    public static List<Vector3> GetAllVertices(Mesh mesh)
    {
        if(mesh == null)
        {
            return new List<Vector3>();
        }
        return new List<Vector3>(mesh.vertices);
    }
}