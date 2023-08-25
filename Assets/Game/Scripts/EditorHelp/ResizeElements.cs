#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.EditorHelp
{
    public class ResizeElements : MonoBehaviour
    {
        public float divisionFactor = 1.73625f;

        public void Resize()
        {
            ResizeRectTransforms(transform);
            ResizeTextMeshProTexts(transform);
            ResizeLayoutGroups(transform);
        }

        private void ResizeRectTransforms(Transform parentTransform)
        {
            RectTransform[] rectTransforms = parentTransform.GetComponentsInChildren<RectTransform>(true);

            foreach (RectTransform rectTransform in rectTransforms)
            {
                rectTransform.anchoredPosition /= divisionFactor;
                rectTransform.sizeDelta /= divisionFactor;
            }
        }

        private void ResizeTextMeshProTexts(Transform parentTransform)
        {
            TMP_Text[] textMeshProTexts = parentTransform.GetComponentsInChildren<TMP_Text>(true);

            foreach (TMP_Text textMeshProText in textMeshProTexts)
            {
                textMeshProText.fontSize /= divisionFactor;
            }
        }
        
        private void ResizeLayoutGroups(Transform parentTransform)
        {
            LayoutGroup[] layoutGroups = parentTransform.GetComponentsInChildren<LayoutGroup>(true);

            foreach (LayoutGroup layoutGroup in layoutGroups)
            {
                HorizontalLayoutGroup horizontalLayoutGroup = layoutGroup as HorizontalLayoutGroup;
                if (horizontalLayoutGroup != null)
                {
                    horizontalLayoutGroup.spacing /= divisionFactor;
                    horizontalLayoutGroup.padding.left = Mathf.CeilToInt(horizontalLayoutGroup.padding.left / divisionFactor);
                    horizontalLayoutGroup.padding.right = Mathf.CeilToInt(horizontalLayoutGroup.padding.right / divisionFactor);
                    horizontalLayoutGroup.padding.top = Mathf.CeilToInt(horizontalLayoutGroup.padding.top / divisionFactor);
                    horizontalLayoutGroup.padding.bottom = Mathf.CeilToInt(horizontalLayoutGroup.padding.bottom / divisionFactor);
                }

                VerticalLayoutGroup verticalLayoutGroup = layoutGroup as VerticalLayoutGroup;
                if (verticalLayoutGroup != null)
                {
                    verticalLayoutGroup.spacing /= divisionFactor;
                    verticalLayoutGroup.padding.top = Mathf.CeilToInt(verticalLayoutGroup.padding.top / divisionFactor);
                    verticalLayoutGroup.padding.bottom = Mathf.CeilToInt(verticalLayoutGroup.padding.bottom / divisionFactor);
                    verticalLayoutGroup.padding.left = Mathf.CeilToInt(verticalLayoutGroup.padding.left / divisionFactor);
                    verticalLayoutGroup.padding.right = Mathf.CeilToInt(verticalLayoutGroup.padding.right / divisionFactor);
                }
            }
        }
    }
    
    [CustomEditor(typeof(ResizeElements))]
    public class ResizeElementsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ResizeElements script = (ResizeElements)target;

            if (GUILayout.Button("Resize"))
            {
                script.Resize();
            }
        }
    }
}
#endif
