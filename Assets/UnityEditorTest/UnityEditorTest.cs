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
    private string ASSET_PATH = "Assets/UnityEditorTest/Resources/ScriptableTest.asset";


    private void OnGUI()
    {
        Color defaultColor = GUI.backgroundColor;
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("�ݒ�");
            }
            GUI.backgroundColor = defaultColor;

            _sample.SampleIntValue = EditorGUILayout.IntField("�T���v��", _sample.SampleIntValue);
        }
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("�t�@�C������");
            }
            GUI.backgroundColor = defaultColor;

            //GUILayout.Label("�p�X�F" + ASSET_PATH);
            ASSET_PATH = EditorGUILayout.TextField("�p�X", ASSET_PATH);

            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                // �ǂݍ��݃{�^��
                if (GUILayout.Button("�ǂݍ���"))
                {
                    Import();
                }
                // �������݃{�^��
                if (GUILayout.Button("��������"))
                {
                    Export();
                }
            }
        }
    }

    private void Import()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<ScriptableTest>();
        }

        ScriptableTest sample = AssetDatabase.LoadAssetAtPath<ScriptableTest>(ASSET_PATH);
        if (sample == null)
            return;

        // �R�s�[����
        //_sample.Copy(sample);
        EditorUtility.CopySerialized(sample, _sample);
    }

    private void Export()
    {
        // �ǂݍ���
        ScriptableTest sample = AssetDatabase.LoadAssetAtPath<ScriptableTest>(ASSET_PATH);
        if (sample == null)
        {
            sample = ScriptableObject.CreateInstance<ScriptableTest>();
        }

        // �V�K�̏ꍇ�͍쐬
        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // �A�Z�b�g�쐬
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

        // �R�s�[
        //sample.Copy(_sample);
        EditorUtility.CopySerialized(_sample, sample);
        // ���ڕҏW�ł��Ȃ��悤�ɂ���
        sample.hideFlags = HideFlags.NotEditable;
        // �X�V�ʒm
        EditorUtility.SetDirty(sample);
        // �ۑ�
        AssetDatabase.SaveAssets();
        // �G�f�B�^���ŐV�̏�Ԃɂ���
        AssetDatabase.Refresh();
    }
}
