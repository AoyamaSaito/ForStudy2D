using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEditor.Toolbars;

// オーバーレイ本体
[Overlay(typeof(SceneView), MenuPath)]
public class SceneControlExample : ToolbarOverlay
{
    const string MenuPath = "Custom/SceneControl";

    SceneControlExample() : base(
        SelectPlayerButton.ID // IDを使ってUIを登録
        )
    { }
}

// ボタンの挙動 
[EditorToolbarElement(ID, typeof(SceneView))]
public class SelectPlayerButton : ToolbarButton
{
    public const string ID = "SceneControlExample.SelectPlayerButton"; // ユニークなID

    SelectPlayerButton()
    {
        tooltip = "Playerタグが設定されているオブジェクトを選択します。";
        text = "プレイヤーを選択";
        clicked += OnClicked;
    }

    private void OnClicked()
    {
        Selection.activeGameObject = GameObject.FindWithTag("Player");
    }
}