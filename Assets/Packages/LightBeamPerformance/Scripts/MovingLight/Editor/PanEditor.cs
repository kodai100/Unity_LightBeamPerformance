using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace ProjectBlue.LightBeamPerformance
{

    [CustomEditor(typeof(Pan))]
    public class PanEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = target as Pan;

            if(GUILayout.Button("Register Default Rotation"))
            {
                script.RegisterDefaultRotation();
            }

        }
    }

}
