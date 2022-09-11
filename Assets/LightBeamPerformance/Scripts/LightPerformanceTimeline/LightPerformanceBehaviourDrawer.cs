
using UnityEditor;
using UnityEngine;

namespace ProjectBlue.LightBeamPerformance
{
    [CustomPropertyDrawer(typeof(LightPerformanceBehaviour))]
    public class LightPerformanceBehaviourDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
        {
            var fieldCount = 1;
            return fieldCount * EditorGUIUtility.singleLineHeight;
        }
 
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            
            var preset = property.FindPropertyRelative("preset");
            EditorGUILayout.PropertyField(preset);

            if (preset.objectReferenceValue == null)
            {
                var color = property.FindPropertyRelative("color");
                
                var saturation = property.FindPropertyRelative("saturation");

                var dimmer = property.FindPropertyRelative("dimmer");
                var motion = property.FindPropertyRelative("motion");

                var bpm = property.FindPropertyRelative("bpm");
                var gradient = property.FindPropertyRelative("lightGradient");
                var intensityMultiplier = property.FindPropertyRelative("intensityMultiplier");

                var speed = property.FindPropertyRelative("speed");
                var offsetStrength = property.FindPropertyRelative("offsetStrength");

                var panRange = property.FindPropertyRelative("panRange");
                var tiltRange = property.FindPropertyRelative("tiltRange");


                var singleFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUILayout.PropertyField(color);
                EditorGUILayout.PropertyField(saturation);
                EditorGUILayout.PropertyField(dimmer);
                EditorGUILayout.PropertyField(motion);
                EditorGUILayout.PropertyField(bpm);
                EditorGUILayout.PropertyField(gradient);
                EditorGUILayout.PropertyField(intensityMultiplier);
                EditorGUILayout.PropertyField(speed);
                EditorGUILayout.PropertyField(offsetStrength);
                EditorGUILayout.PropertyField(panRange);
                EditorGUILayout.PropertyField(tiltRange);
            }
            else
            {
                // readonlyで表示したい
                EditorGUI.BeginDisabledGroup(true);

                var p = preset.objectReferenceValue as LightPerformanceClipPreset;

                EditorGUILayout.EnumPopup("Color Animation Mode", p.colorAnimationMode);

                EditorGUILayout.Slider("Saturation", p.saturation, 0f, 1f);

                EditorGUILayout.EnumPopup("Dimmer Animation Mode", p.dimmerAnimationMode);
                EditorGUILayout.EnumPopup("Motion Animation Mode", p.motionAnimationMode);

                EditorGUILayout.FloatField("BPM", p.bpm);

                EditorGUILayout.GradientField("Gradient", p.gradient);

                EditorGUILayout.FloatField("Intensity Multiplier", p.intensityMultiplier);  

                EditorGUILayout.FloatField("Offset Strength", p.offsetStrength);
                
                EditorGUILayout.MinMaxSlider("Pan Range", ref p.panRange.min, ref p.panRange.max, p.panRange.minLimit, p.panRange.maxLimit);
                EditorGUILayout.MinMaxSlider("Tilt Range", ref p.tiltRange.min, ref p.tiltRange.max, p.tiltRange.minLimit, p.tiltRange.maxLimit);

                EditorGUI.EndDisabledGroup();
            }
            
            
        }
    }
}

