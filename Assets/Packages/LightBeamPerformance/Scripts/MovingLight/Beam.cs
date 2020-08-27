using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Beam : MonoBehaviour
{

    public Color color;
    public float intensity = 1f;
    public float intensityMultiplier = 0.5f;

    public void Process()
    {
        ProcessInternal();
    }

    protected abstract void ProcessInternal();
}
