using UnityEditor;
using UnityEngine;
using UnityMonetization;
using UnityMonetization.Unit;
using UnityMonetization.Unit.Admob;
[CustomPropertyDrawer(typeof(BannerOption), true)]
public class BannerOptionEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var propAdName = property.FindPropertyRelative("_adName");
        var propAdUnitId = property.FindPropertyRelative("_adUnitId");
        var propAdNetType = property.FindPropertyRelative("_adNetType");
        var propPosition = property.FindPropertyRelative("position");
        var propType = property.FindPropertyRelative("type");
        var propSizeType = property.FindPropertyRelative("sizeType");
        var propSize = property.FindPropertyRelative("customSize");


        EditorGUI.BeginProperty(position, label, property);

        var type = property.type;

        // Tính chiều cao từng dòng
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        // Vẽ foldout nếu bạn muốn (không bắt buộc)
        property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(
            new Rect(position.x, position.y, position.width, lineHeight),
            property.isExpanded, label);
        EditorGUILayout.EndFoldoutHeaderGroup();
        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            Rect fieldRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, lineHeight);

            EditorGUI.PropertyField(fieldRect, propAdName);
            fieldRect.y += lineHeight + spacing;
            EditorGUI.PropertyField(fieldRect, propAdUnitId);
            fieldRect.y += lineHeight + spacing;
            EditorGUI.PropertyField(fieldRect, propAdNetType);
            fieldRect.y += lineHeight + spacing;

            EditorGUI.PropertyField(fieldRect, propPosition);
            fieldRect.y += lineHeight + spacing;
            var enumPosition = propPosition.enumValueIndex > 6;
            if (enumPosition)
            {
                EditorGUI.indentLevel++;
                EditorGUI.LabelField(fieldRect, "Warning: Admob not support for <b>CenterLeft</b> and <b>CenterRight</b>", GetLableStyle(color: Color.yellow));
                fieldRect.y += lineHeight + spacing;
                EditorGUI.indentLevel--;
            }

            EditorGUI.PropertyField(fieldRect, propType);
            fieldRect.y += lineHeight + spacing;
            var typeEnum = propType.enumValueIndex > 1;
            if (typeEnum)
            {
                EditorGUI.indentLevel++;
                EditorGUI.LabelField(fieldRect, "Warning: Max not support for <b>Collapsible</b>", GetLableStyle(color: Color.yellow));
                fieldRect.y += lineHeight + spacing;
                EditorGUI.indentLevel--;
            }

            SerializedProperty sizeTypeProp = propSizeType;
            EditorGUI.PropertyField(fieldRect, sizeTypeProp);
            fieldRect.y += lineHeight + spacing;

            if (sizeTypeProp.enumValueIndex == (int)BannerSize.CustomSize)
            {
                EditorGUI.PropertyField(fieldRect, propSize);
                fieldRect.y += lineHeight + spacing;
                if (propType.enumValueIndex == 1)
                {
                    EditorGUI.indentLevel++;
                    EditorGUI.LabelField(fieldRect, "For <b><color=green>Adaptive</color><b> type, it is not possible to create <b>Custom Size</b>.", GetLableStyle(color: Color.yellow));
                    fieldRect.y += lineHeight + spacing;
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel++;
                EditorGUI.LabelField(fieldRect, "<color=green><b>Custom Size</b></color> not support for <b><color=red>MAX</color></b>", GetLableStyle(color: Color.yellow));
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private GUIStyle GetLableStyle(FontStyle fontStyle = FontStyle.Normal, Color color = default)
    {
        var style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = color;
        style.fontStyle = fontStyle;
        style.richText = true;
        return style;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lines = 1; // Foldout line

        if (property.isExpanded)
        {
            lines += 6; // position, type, sizeType

            var propPosition = property.FindPropertyRelative("position");
            var propType = property.FindPropertyRelative("type");
            var propSizeType = property.FindPropertyRelative("sizeType");
            var propSize = property.FindPropertyRelative("customSize");
            if (propSizeType.enumValueIndex == (int)BannerSize.CustomSize)
            {
                lines += 2; // customSize
                if (propType.enumValueIndex == (int)BannerType.Adaptive)
                    lines += 1;
            }
            if (propType.enumValueIndex > (int)BannerType.Adaptive)
                lines += 1;
            if (propPosition.enumValueIndex > (int)BannerPosition.Center)
                lines += 1;
        }

        return lines * EditorGUIUtility.singleLineHeight + (lines - 1) * EditorGUIUtility.standardVerticalSpacing;
    }
}
