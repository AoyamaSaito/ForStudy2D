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
        // 生成
        UnityEditorTest window = GetWindow<UnityEditorTest>("サンプル");
        // 最小サイズ設定
        window.minSize = new Vector2(320, 320);
    }

    /// <summary>
    /// ScriptableObjectSampleの変数
    /// </summary>
    private ScriptableTest _sample;

    /// <summary>
    /// アセットパス
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
                GUILayout.Label("設定");
            }
            GUI.backgroundColor = defaultColor;

            _sample.SampleIntValue = EditorGUILayout.IntField("サンプル", _sample.SampleIntValue);
        }
        using (new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUI.backgroundColor = Color.gray;
            using (new GUILayout.HorizontalScope(EditorStyles.toolbar))
            {
                GUILayout.Label("ファイル操作");
            }
            GUI.backgroundColor = defaultColor;

            //GUILayout.Label("パス：" + ASSET_PATH);
            ASSET_PATH = EditorGUILayout.TextField("パス", ASSET_PATH);

            using (new GUILayout.HorizontalScope(GUI.skin.box))
            {
                // 読み込みボタン
                if (GUILayout.Button("読み込み"))
                {
                    Import();
                }
                // 書き込みボタン
                if (GUILayout.Button("書き込み"))
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

        // コピーする
        //_sample.Copy(sample);
        EditorUtility.CopySerialized(sample, _sample);
    }

    private void Export()
    {
        // 読み込み
        ScriptableTest sample = AssetDatabase.LoadAssetAtPath<ScriptableTest>(ASSET_PATH);
        if (sample == null)
        {
            sample = ScriptableObject.CreateInstance<ScriptableTest>();
        }

        // 新規の場合は作成
        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

        // コピー
        //sample.Copy(_sample);
        EditorUtility.CopySerialized(_sample, sample);
        // 直接編集できないようにする
        sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }
}
