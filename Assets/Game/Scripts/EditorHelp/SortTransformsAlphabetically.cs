using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SortTransformsAlphabetically : MonoBehaviour
{
#if UNITY_EDITOR
    [CustomEditor(typeof(SortTransformsAlphabetically))]
    public class SortTransformsAlphabeticallyEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SortTransformsAlphabetically sorter = (SortTransformsAlphabetically)target;
            if (GUILayout.Button("Sort Children Alphabetically"))
            {
                sorter.SortChildrenAlphabetically();
            }
        }
    }
#endif

    void Start()
    {
        // Ваш код початкової ініціалізації (якщо потрібно)
    }

#if UNITY_EDITOR
    public void SortChildrenAlphabetically()
    {
        // Отримати всі дочірні трансформи поточного об'єкта
        Transform[] childTransforms = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
        }

        // Сортування трансформів за ім'ям (алфавітом)
        System.Array.Sort(childTransforms, (a, b) => a.name.CompareTo(b.name));

        // Перерозташування дочірніх об'єктів відсортованими трансформами
        for (int i = 0; i < childTransforms.Length; i++)
        {
            childTransforms[i].SetSiblingIndex(i);
        }
    }
#endif
}