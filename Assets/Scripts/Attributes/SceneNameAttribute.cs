using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class SceneNameAttribute : PropertyAttribute
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneNameAttribute))]
    private class SceneNameAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type != "string") return;

            var propertyValue = property.stringValue;
            var sceneNames = EditorBuildSettings.scenes
                .Select(scene => scene.path)
                .Select(scenePath => scenePath.Split('/').Last().Replace(".unity", ""))
                .ToArray();
            var currentIndex = Array.IndexOf(sceneNames, propertyValue);
            sceneNames = new[] { "NO_MATCH" }.Concat(sceneNames).ToArray();
            currentIndex++;

            // ドロップダウンメニューを表示
            var nextIndex = EditorGUI.Popup(position, label.text, currentIndex, sceneNames);
            if (nextIndex == 0)
            {
                nextIndex = currentIndex;
            }

            property.stringValue = sceneNames[nextIndex];
        }
    }
#endif
}