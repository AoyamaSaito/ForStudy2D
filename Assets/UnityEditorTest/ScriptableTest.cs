using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScriptableTest : ScriptableObject
{
    [SerializeField]
    private int _sampleIntValue;

    public int SampleIntValue
    {
        get { return _sampleIntValue; }
#if UNITY_EDITOR
        set { _sampleIntValue = Mathf.Clamp(value, 0, int.MaxValue); }
#endif
    }
}
