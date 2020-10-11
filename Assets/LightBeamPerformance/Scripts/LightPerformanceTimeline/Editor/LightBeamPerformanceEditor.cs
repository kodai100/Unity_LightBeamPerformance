using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ProjectBlue.LightBeamPerformance
{

    [CustomEditor(typeof(LightBeamPerformance))]
    public class LightBeamPerformanceEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = target as LightBeamPerformance;

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Initialize for Editor"))
            {
                script.Initialize();
            }

        }
    }

}