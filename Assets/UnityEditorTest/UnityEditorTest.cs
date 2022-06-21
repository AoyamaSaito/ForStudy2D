using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UnityEditorTest : EditorWindow
{
    [MenuItem("Editor/Sample")]
    private static void Create()
    {
        // ����
        UnityEditorTest window = GetWindow<UnityEditorTest>("�T���v��");
        // �ŏ��T�C�Y�ݒ�
        window.minSize = new Vector2(320, 320);
    }

    /// <summary>
    /// ScriptableObjectSample�̕ϐ�
    /// </summary>
    private ScriptableTest _sample;

    /// <summary>
    /// �A�Z�b�g�p�X
    /// </summary>
    private const string ASSET_PATH = "Assets/Resources/ScriptableObjectSample.asset";


    private void OnGUI()
    {
        EditorGUILayout.LabelField("Project Settings", EditorStyles.boldLabel);
        using (new GUILayout.HorizontalScope())
        {

        }
        using (new GUILayout.HorizontalScope())
        {
            // �������݃{�^��
            if (GUILayout.Button("��������"))
            {
                Export();
            }
        }
    }

    private void Export()
    {
        // �V�K�̏ꍇ�͍쐬
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // �A�Z�b�g�쐬
            AssetDatabase.CreateAsset(_sample, ASSET_PATH);
        }
        // �C���X�y�N�^�[����ݒ�ł��Ȃ��悤�ɂ���
        _sample.hideFlags = HideFlags.NotEditable;
        // �X�V�ʒm
        EditorUtility.SetDirty(_sample);
        // �ۑ�
        AssetDatabase.SaveAssets();
        // �G�f�B�^���ŐV�̏�Ԃɂ���
        AssetDatabase.Refresh();
    }
}
