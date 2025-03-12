#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class AdvancedModularSpawner: MonoBehaviour
{
    [System.Serializable]
    public class LayerDistanceConstraint
    {
        public string targetTag;
        public float minDistance = 1f;

        public LayerDistanceConstraint(string tag = "",float distance = 1f)
        {
            targetTag = tag;
            minDistance = distance;
        }
    }

    [System.Serializable]
    public class SpawnLayerConfig
    {
        public string layerTag = "NewLayer";
        public int spawnPriority = 0;
        public Vector2 sizeRange = new Vector2(1,1);
        public Color color = Color.white;
        public List<LayerDistanceConstraint> distanceConstraints = new List<LayerDistanceConstraint>();
        public Vector2 spawnRadiusRange = new Vector2(5,10);
        public int maxSpawnAttempts = 100;
        public int maxSpawnCount = 20;

        public SpawnLayerConfig()
        {
            distanceConstraints = new List<LayerDistanceConstraint>
            {
                new LayerDistanceConstraint("", 1f)
            };
        }
    }

    [Header("Core Settings")]
    public GameObject basePrefab;

    public bool liveUpdate = false;
    public bool drawDebug = true;

    [Header("Layer Configuration")]
    [SerializeField] private List<SpawnLayerConfig> layerConfigs = new List<SpawnLayerConfig>();

    [Space]
    [SerializeField] private bool regenerate;

    [SerializeField] private bool resetToDefaults;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private Dictionary<string,List<GameObject>> taggedObjects = new Dictionary<string,List<GameObject>>();

    private void Reset()
    {
        InitializeDefaultLayers();
    }

    private void OnValidate()
    {
        if(resetToDefaults)
        {
            resetToDefaults = false;
            ResetToDefaults();
        }

        if(liveUpdate && !Application.isPlaying)
        {
            RegenerateSystem();
        }
    }

    private void Update()
    {
        if(regenerate)
        {
            regenerate = false;
            RegenerateSystem();
        }
    }

    [ContextMenu("Regenerate System")]
    public void RegenerateSystem()
    {
        ClearAll();
        GenerateAllLayers();
    }

    [ContextMenu("Reset to Defaults")]
    public void ResetToDefaults()
    {
        ClearAll();
        InitializeDefaultLayers();
        RegenerateSystem();
    }

    private void InitializeDefaultLayers()
    {
        layerConfigs = new List<SpawnLayerConfig>
        {
            new SpawnLayerConfig
            {
                layerTag = "Layer1",
                spawnPriority = 2,
                sizeRange = new Vector2(5, 6),
                color = Color.red,
                distanceConstraints = new List<LayerDistanceConstraint>
                {
                    new LayerDistanceConstraint("Layer1", 3f)
                }
            },
            new SpawnLayerConfig
            {
                layerTag = "Layer2",
                spawnPriority = 1,
                sizeRange = new Vector2(1, 3),
                color = Color.green,
                distanceConstraints = new List<LayerDistanceConstraint>
                {
                    new LayerDistanceConstraint("Layer1", 0f),
                    new LayerDistanceConstraint("Layer2", 1.5f)
                }
            },
            new SpawnLayerConfig
            {
                layerTag = "Layer3",
                spawnPriority = 0,
                sizeRange = new Vector2(0.1f, 0.5f),
                color = Color.blue,
                distanceConstraints = new List<LayerDistanceConstraint>
                {
                    new LayerDistanceConstraint("Layer2", 0f),
                    new LayerDistanceConstraint("Layer3", 0.3f)
                }
            }
        };
    }

    public void ClearAll()
    {
        foreach(var obj in spawnedObjects)
        {
            if(obj != null)
            {
#if UNITY_EDITOR
                if(!Application.isPlaying)
                    Undo.DestroyObjectImmediate(obj);
                else
#endif
                    Destroy(obj);
            }
        }
        spawnedObjects.Clear();
        taggedObjects.Clear();
    }

    private void GenerateAllLayers()
    {
        layerConfigs.Sort((a,b) => b.spawnPriority.CompareTo(a.spawnPriority));

        foreach(var config in layerConfigs)
        {
            if(!taggedObjects.ContainsKey(config.layerTag))
                taggedObjects[config.layerTag] = new List<GameObject>();

            for(int i = 0 ; i < config.maxSpawnCount ; i++)
            {
                Vector3 spawnPos = FindValidPosition(config);
                if(spawnPos != Vector3.zero)
                {
                    CreateObject(config,spawnPos);
                }
            }
        }
    }

    private Vector3 FindValidPosition(SpawnLayerConfig config)
    {
        for(int attempt = 0 ; attempt < config.maxSpawnAttempts ; attempt++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * Random.Range(config.spawnRadiusRange.x,config.spawnRadiusRange.y);
            randomPos.y = 0;

            if(IsPositionValid(randomPos,config))
                return randomPos;
        }
        return Vector3.zero;
    }

    private bool IsPositionValid(Vector3 position,SpawnLayerConfig config)
    {
        foreach(var constraint in config.distanceConstraints)
        {
            if(taggedObjects.TryGetValue(constraint.targetTag,out List<GameObject> targets))
            {
                foreach(var target in targets)
                {
                    if(Vector3.Distance(position,target.transform.position) < constraint.minDistance)
                        return false;
                }
            }
        }
        return true;
    }

    private void CreateObject(SpawnLayerConfig config,Vector3 position)
    {
#if UNITY_EDITOR
        GameObject newObj = (Application.isPlaying) ?
            Instantiate(basePrefab,position,Quaternion.identity) :
            PrefabUtility.InstantiatePrefab(basePrefab) as GameObject;

        if(!Application.isPlaying)
            Undo.RegisterCreatedObjectUndo(newObj,"Create Spawned Object");
#else
        GameObject newObj = Instantiate(basePrefab, position, Quaternion.identity);
#endif

        newObj.transform.position = position;
        newObj.name = $"{config.layerTag}_Object";
        newObj.tag = config.layerTag;

        newObj.transform.localScale = Vector3.one * Random.Range(config.sizeRange.x,config.sizeRange.y);

        var renderer = newObj.GetComponentInChildren<Renderer>(true); // Get renderer from child
        if(renderer != null)
        {
            var propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color",config.color);
            renderer.SetPropertyBlock(propBlock);
        }
        spawnedObjects.Add(newObj);
        taggedObjects[config.layerTag].Add(newObj);
    }

    private void OnDrawGizmos()
    {
        if(!drawDebug)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,layerConfigs.Count > 0 ?
            layerConfigs[0].spawnRadiusRange.y : 10f);

        foreach(var config in layerConfigs)
        {
            Gizmos.color = config.color;
            Gizmos.DrawWireCube(transform.position,Vector3.one * config.spawnRadiusRange.y * 0.5f);
        }
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(AdvancedModularSpawner))]
public class AdvancedModularSpawnerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AdvancedModularSpawner spawner = (AdvancedModularSpawner)target;

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Regenerate System"))
        {
            spawner.RegenerateSystem();
        }
        if(GUILayout.Button("Reset to Defaults"))
        {
            spawner.ResetToDefaults();
        }
        EditorGUILayout.EndHorizontal();
    }
}

#endif