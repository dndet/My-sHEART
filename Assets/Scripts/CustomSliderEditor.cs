#if UNITY_EDITOR
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(SliderSub))]
public class CustomSliderEditor : Editor
{
    // Các SerializedProperty của Slider
    SerializedProperty transition;
    SerializedProperty colors;
    SerializedProperty spriteState;
    SerializedProperty animationTriggers;

    // Các SerializedProperty thêm vào cho các thành phần cần hiển thị
    SerializedProperty handleRect;
    SerializedProperty fillRect;
    SerializedProperty onValueChanged;
    SerializedProperty direction;
    SerializedProperty minValue;
    SerializedProperty maxValue;
    SerializedProperty value;
    SerializedProperty wholeNumbers;

    private void OnEnable()
    {
        // Lấy các SerializedProperty từ đối tượng Slider
        transition = serializedObject.FindProperty("m_Transition");
        colors = serializedObject.FindProperty("m_Colors");
        spriteState = serializedObject.FindProperty("m_SpriteState");
        animationTriggers = serializedObject.FindProperty("m_AnimationTriggers");

        // Lấy các SerializedProperty cho các thành phần cần kiểm soát hiển thị
        handleRect = serializedObject.FindProperty("m_HandleRect");
        fillRect = serializedObject.FindProperty("m_FillRect");
        onValueChanged = serializedObject.FindProperty("m_OnValueChanged");
        direction = serializedObject.FindProperty("m_Direction");

        // Các SerializedProperty cơ bản của Slider
        minValue = serializedObject.FindProperty("m_MinValue");
        maxValue = serializedObject.FindProperty("m_MaxValue");
        value = serializedObject.FindProperty("m_Value");
        wholeNumbers = serializedObject.FindProperty("m_WholeNumbers");

        value.floatValue = minValue.floatValue;
    }

    public override void OnInspectorGUI()
    {
        // Cập nhật các thay đổi trong các thuộc tính
        serializedObject.Update();

        // Vẽ các thuộc tính cơ bản của Slider
        EditorGUILayout.PropertyField(minValue);
        EditorGUILayout.PropertyField(maxValue);
        //EditorGUILayout.PropertyField(value);
        if(wholeNumbers.boolValue)
            value.floatValue = (int) EditorGUILayout.Slider((float) value.floatValue, minValue.floatValue, maxValue.floatValue);
        else
            value.floatValue = EditorGUILayout.Slider(value.floatValue, minValue.floatValue, maxValue.floatValue);
        EditorGUILayout.PropertyField(wholeNumbers);

        // Vẽ lại thuộc tính CustomValue
        SliderSub slider = (SliderSub)target;
        slider.fillIndex = EditorGUILayout.FloatField("Fill Index", slider.fillIndex);
        slider.audioPlay = (AudioSource)EditorGUILayout.ObjectField("Audio Play", slider.audioPlay, typeof(AudioSource), true);
        slider.subText = EditorGUILayout.TextField("Sub Text", slider.subText);
        slider.textShow = (TextMeshProUGUI)EditorGUILayout.ObjectField("Show Value", slider.textShow, typeof(TextMeshProUGUI), true);

        // Vẽ các thuộc tính của Slider mà bạn muốn hiển thị
        EditorGUILayout.PropertyField(transition);

        // Kiểm tra loại Transition để vẽ các thuộc tính tương ứng
        if (transition.enumValueIndex == (int)Selectable.Transition.ColorTint)
        {
            EditorGUILayout.PropertyField(colors);  // Hiển thị Color Tint
        }
        else if (transition.enumValueIndex == (int)Selectable.Transition.SpriteSwap)
        {
            EditorGUILayout.PropertyField(spriteState);  // Hiển thị Sprite Swap
        }
        else if (transition.enumValueIndex == (int)Selectable.Transition.Animation)
        {
            EditorGUILayout.PropertyField(animationTriggers);  // Hiển thị Animation
        }

        // **Hiển thị các thuộc tính liên quan đến Slider mà bạn cần thêm**
        EditorGUILayout.PropertyField(handleRect);  // Hiển thị Handle Rect
        EditorGUILayout.PropertyField(fillRect);    // Hiển thị Fill Rect
        EditorGUILayout.PropertyField(onValueChanged);  // Hiển thị On Value Changed
        EditorGUILayout.PropertyField(direction);   // Hiển thị Direction

        // Cập nhật đối tượng sau khi chỉnh sửa
        serializedObject.ApplyModifiedProperties();
    }
}
#endif