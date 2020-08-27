using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

namespace ProjectBlue.LightBeamPerformance
{

    [CustomEditor(typeof(Tilt))]
    public class TiltEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = target as Tilt;

            if(GUILayout.Button("Register Default Rotation"))
            {
                script.RegisterDefaultRotation();

                EditorUtility.SetDirty(script);
            }

        }
    }

}
