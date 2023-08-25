#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RevertLODGroups))]
public class RevertLODGroupsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RevertLODGroups script = (RevertLODGroups)target;

        if (GUILayout.Button("Revert LODGroups to Prefab"))
        {
            script.RevertSelectedLODGroups();
        }
    }
}

public class RevertLODGroups : MonoBehaviour
{
    [SerializeField] private string name;

    public void RevertSelectedLODGroups()
    {
        LODGroup[] lodGroups = FindObjectsOfType<LODGroup>();

        foreach (LODGroup lodGroup in lodGroups)
        {
            if (lodGroup.name.Contains(name))
            {
                RevertLODGroupToPrefab(lodGroup);
            }
        }
    }

    private void RevertLODGroupToPrefab(LODGroup lodGroup)
    {
        PrefabUtility.RevertObjectOverride(lodGroup, InteractionMode.AutomatedAction);
    }
}

#endif