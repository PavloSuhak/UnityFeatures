using UnityEngine;

[CreateAssetMenu(fileName = "ShaderReplacementSettings", menuName = "Shader Replacement/Settings", order = 1)]
public class ShaderReplacementSettings : ScriptableObject
{
    public Material oldMaterialRef;
    public Material newMaterialRef;
}