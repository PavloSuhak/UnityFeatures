using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

public class MaterialShaderEditor : MonoBehaviour
{
    [SerializeField] private ShaderReplacementSettings settings;

    [Button]
    public void ChangeMaterialShaders()
    {
        var materialGuids = AssetDatabase.FindAssets("t:Material");

        var oldShader = settings.oldMaterialRef.shader;
        var newShader = settings.newMaterialRef.shader;

        if (oldShader == null || newShader == null)
        {
            Debug.LogError("Null ref of materials");
            return;
        }

        foreach (var guid in materialGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (material != null && material.shader == oldShader)
            {
                var tempMaterial = new Material(material)
                {
                    shader = newShader
                };

                CopyMaterialProperties(material, tempMaterial);

                material.shader = newShader;

                CopyMaterialProperties(tempMaterial, material);

                DestroyImmediate(tempMaterial);
                EditorUtility.SetDirty(material);
            }
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Shader replacement completed!");
    }

    private void CopyMaterialProperties(Material from, Material to)
    {
        string[] texturePropertyNames = {
            "_BaseMap", "_NormalMap", "_BumpMap", "_EmissionMap", "_MetallicGlossMap", "_OcclusionMap", "_ParallaxMap"
        };

        foreach (var propName in texturePropertyNames)
        {
            if (from.HasProperty(propName) && to.HasProperty(propName))
            {
                to.SetTexture(propName, from.GetTexture(propName));
                to.SetTextureOffset(propName, from.GetTextureOffset(propName));
                to.SetTextureScale(propName, from.GetTextureScale(propName));
            }
        }

        string[] floatPropertyNames = {
            "_Metallic", "_Glossiness", "_BumpScale", "_Parallax", "_OcclusionStrength"
        };

        foreach (var propName in floatPropertyNames)
        {
            if (from.HasProperty(propName) && to.HasProperty(propName))
            {
                to.SetFloat(propName, from.GetFloat(propName));
            }
        }
        
        string[] vectorPropertyNames = {
            "_MainTex_ST", "_EmissionColor"
        };

        foreach (var propName in vectorPropertyNames)
        {
            if (from.HasProperty(propName) && to.HasProperty(propName))
            {
                to.SetVector(propName, from.GetVector(propName));
            }
        }
    }
}
