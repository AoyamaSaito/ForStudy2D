using UnityEngine;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.UIElements;
using UnityEditor.Toolbars;

// �I�[�o�[���C�{��
[Overlay(typeof(SceneView), MenuPath)]
public class SceneControlExample : ToolbarOverlay
{
    const string MenuPath = "Custom/SceneControl";

    SceneControlExample() : base(
        SelectPlayerButton.ID // ID���g����UI��o�^
        )
    { }
}

// �{�^���̋��� 
[EditorToolbarElement(ID, typeof(SceneView))]
public class SelectPlayerButton : ToolbarButton
{
    public const string ID = "SceneControlExample.SelectPlayerButton"; // ���j�[�N��ID

    SelectPlayerButton()
    {
        tooltip = "Player�^�O���ݒ肳��Ă���I�u�W�F�N�g��I�����܂��B";
        text = "�v���C���[��I��";
        clicked += OnClicked;
    }

    private void OnClicked()
    {
        Selection.activeGameObject = GameObject.FindWithTag("Player");
    }
}