using UnityEngine;
using UnityEditor;

namespace ProjectBlue.LightBeamPerformance
{
    /// <summary>
    /// MinMaxSlider with DelayedFloatField
    /// </summary>
    [System.Serializable]
    public class FloatRange
    {
        public float min;
        public float max;

        public float minLimit;
        public float maxLimit;
        public bool clampMin;
        public bool clampMax;

        /// <summary>
        /// return max - min
        /// </summary>
        public float Range { get { return max - min; } }

        /// <summary>
        /// return Random.Range(min, max)
        /// </summary>
        public float RandomRange() { return Random.Range(min, max); }

        /// <summary>
        /// MinMaxSlider with DelayedFloatField
        /// </summary>
        public FloatRange(float min, float max, float minLimit = 0f, float maxLimit = 1f, bool clampMin = true, bool clampMax = true)
        {
            this.min = min;
            this.max = max;
            this.minLimit = minLimit;
            this.maxLimit = Mathf.Max(minLimit, maxLimit);
            this.clampMin = clampMin;
            this.clampMax = clampMax;
        }

    #if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(FloatRange))]
        public class FloatRangeDrawer : PropertyDrawer
        {
            const int NUMBER_OF_DECIMAL_DIGITS = 3;
            const int CLEARANCE_X = 4;
            const int FLOAT_FIELD_WIDTH = 50;

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                using (new EditorGUI.PropertyScope(position, label, property))
                {
                    //各プロパティー取得
                    var minProperty = property.FindPropertyRelative("min");
                    var maxProperty = property.FindPropertyRelative("max");
                    var min = minProperty.floatValue;
                    var max = maxProperty.floatValue;
                    var minLimit = property.FindPropertyRelative("minLimit").floatValue;
                    var maxLimit = property.FindPropertyRelative("maxLimit").floatValue;
                    var clampMin = property.FindPropertyRelative("clampMin").boolValue;
                    var clampMax = property.FindPropertyRelative("clampMax").boolValue;

                    //表示位置を調整
                    var labelRect = new Rect(position)
                    {
                        width = EditorGUIUtility.labelWidth
                    };

                    float indentWidth = labelRect.width - EditorGUI.IndentedRect(labelRect).width;
                    labelRect.width -= indentWidth;
                    float inputFieldWidth = position.width - labelRect.width;

                    var minFloatRect = new Rect(position)
                    {
                        x = position.x + labelRect.width,
                        width = FLOAT_FIELD_WIDTH + indentWidth
                    };

                    var sliderRect = new Rect(minFloatRect)
                    {
                        x = minFloatRect.x + FLOAT_FIELD_WIDTH + CLEARANCE_X,
                        width = inputFieldWidth - 2f * (FLOAT_FIELD_WIDTH + CLEARANCE_X)
                    };

                    var maxFloatRect = new Rect(sliderRect)
                    {
                        x = sliderRect.x + sliderRect.width + CLEARANCE_X - indentWidth,
                        width = FLOAT_FIELD_WIDTH + indentWidth
                    };

                    EditorGUI.LabelField(labelRect, label);

                    //数値入力
                    EditorGUI.BeginChangeCheck();
                    {
                        min = EditorGUI.DelayedFloatField(minFloatRect, min);
                        max = EditorGUI.DelayedFloatField(maxFloatRect, max);
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        //min優先でClamp
                        min = Mathf.Min(min, clampMax ? maxLimit : float.MaxValue);
                        max = Mathf.Clamp(max, min, clampMax ? maxLimit : float.MaxValue);
                        min = Mathf.Clamp(min, clampMin ? minLimit : float.MinValue, max);
                        maxProperty.floatValue = max;
                        minProperty.floatValue = min;
                    }

                    //スライダー入力
                    EditorGUI.BeginChangeCheck();
                    {
                        EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, minLimit, maxLimit);
                    }
                    if (EditorGUI.EndChangeCheck())
                    {
                        GUI.FocusControl("");
                        min = Mathf.Min(min, maxLimit);
                        max = Mathf.Max(max, minLimit);
                        minProperty.floatValue = Round(min, NUMBER_OF_DECIMAL_DIGITS);
                        maxProperty.floatValue = Round(max, NUMBER_OF_DECIMAL_DIGITS);
                    }
                }
            }

            //少数桁数n桁に四捨五入
            static float Round(float val, int n)
            {
                var unit = Mathf.Pow(10, n);
                return Mathf.Round(val * unit) / unit;
            }
        }
    #endif
    }
}

