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
    private const string ASSET_PATH = "Assets/Resources/ScriptableObjectSample.asset";


    private void OnGUI()
    {
        EditorGUILayout.LabelField("Project Settings", EditorStyles.boldLabel);
        using (new GUILayout.HorizontalScope())
        {

        }
        using (new GUILayout.HorizontalScope())
        {
            // 書き込みボタン
            if (GUILayout.Button("書き込み"))
            {
                Export();
            }
        }
    }

    private void Export()
    {
        // 新規の場合は作成
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(_sample, ASSET_PATH);
        }
        // インスペクターから設定できないようにする
        _sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(_sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }
}
