using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowTest : MonoBehaviour
{
    [SerializeField] Text _test;
    [SerializeField] ScriptableTest _scriptableTest;

    private void Start()
    {
        
    }

    public void ChangeValue(int n)
    {
        _test.text = n.ToString();
    }
}
