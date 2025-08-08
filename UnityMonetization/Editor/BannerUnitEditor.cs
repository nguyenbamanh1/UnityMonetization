using UnityEditor;
using UnityEngine;
using UnityMonetization.Unit;
using UnityMonetization.Unit.Admob;
[CustomPropertyDrawer(typeof(BannerUnit), true)]
public class BannerUnitEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
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
        property.isExpanded = EditorGUI.Foldout(
            new Rect(position.x, position.y, position.width, lineHeight),
            property.isExpanded, label);

        if (property.isExpanded)
        {
            EditorGUI.indentLevel++;

            Rect fieldRect = new Rect(position.x, position.y + lineHeight + spacing, position.width, lineHeight);

            EditorGUI.PropertyField(fieldRect, propPosition);
            fieldRect.y += lineHeight + spacing;
            var enumPosition = propPosition.enumValueIndex > 6;
            if (enumPosition)
            {
                EditorGUI.LabelField(fieldRect, "Warning: Admob not support for CenterLeft and CenterRight");
                fieldRect.y += lineHeight + spacing;
            }

            EditorGUI.PropertyField(fieldRect, propType);
            fieldRect.y += lineHeight + spacing;
            var typeEnum = propType.enumValueIndex > 1;
            if (typeEnum)
            {
                EditorGUI.LabelField(fieldRect, "Warning: Max not support for Collapsible");
                fieldRect.y += lineHeight + spacing;
            }

            SerializedProperty sizeTypeProp = propSizeType;
            EditorGUI.PropertyField(fieldRect, sizeTypeProp);
            fieldRect.y += lineHeight + spacing;

            if (sizeTypeProp.enumValueIndex == (int)BannerSize.CustomSize)
            {
                if (type == typeof(BannerAdmobUnit).Name)
                {
                    if (propType.enumValueIndex != 1)
                    {
                        EditorGUI.PropertyField(fieldRect, propSize);
                    }
                    else
                    {
                        EditorGUI.LabelField(fieldRect, "For Adaptive type, it is not possible to create custom size.");
                    }
                }
                else
                    EditorGUI.LabelField(fieldRect, "Custom size banner not support for " + type);
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lines = 1; // Foldout line

        if (property.isExpanded)
        {
            lines += 3; // position, type, sizeType

            SerializedProperty sizeTypeProp = property.FindPropertyRelative("sizeType");
            if (sizeTypeProp.enumValueIndex == (int)BannerSize.CustomSize)
            {
                lines += 1; // customSize
            }
        }

        return lines * EditorGUIUtility.singleLineHeight + (lines - 1) * EditorGUIUtility.standardVerticalSpacing;
    }
}
