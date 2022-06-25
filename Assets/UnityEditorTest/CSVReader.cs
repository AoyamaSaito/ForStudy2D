using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    const string SHEET_ID = "�V�[�gID";
    const string SHEET_NAME = "�V�[�g1";

    void Start()
    {
        StartCoroutine(Method(SHEET_NAME));
    }

    IEnumerator Method(string _SHEET_NAME)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + _SHEET_NAME);
        yield return request.SendWebRequest();

        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.Log
                (
                    @"�T�[�o�Ƃ̒ʐM�Ɏ��s�B
���N�G�X�g���ڑ��ł��Ȃ������A
�Z�L�����e�B�ŕی삳�ꂽ�`���l�����m���ł��Ȃ������ȂǁB"
                );
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log
                (
                    @"�T�[�o���G���[������Ԃ����B
�T�[�o�Ƃ̒ʐM�ɂ͐����������A
�ڑ��v���g�R���Œ�`����Ă���G���[���󂯎�����B"
                );
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log
                (
                    @"�f�[�^�̏������ɃG���[�������B
���N�G�X�g�̓T�[�o�Ƃ̒ʐM�ɐ����������A
��M�����f�[�^�̏������ɃG���[�������B
�f�[�^���j�����Ă��邩�A�������`���ł͂Ȃ��ȂǁB"
                );
                break;

            default:
                List<string[]> characterDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
                foreach (string[] characterDataArray in characterDataArrayList)
                {
                    CharacterData characterData = new CharacterData(characterDataArray);
                    characterData.DebugParametaView();
                }
                break;
        }
        //if (request.isHttpError || request.isNetworkError)
        //{
        //    Debug.Log(request.error);
        //}
        //else
        //{
        //    List<string[]> characterDataArrayList = ConvertToArrayListFrom(request.downloadHandler.text);
        //    foreach (string[] characterDataArray in characterDataArrayList)
        //    {
        //        CharacterData characterData = new CharacterData(characterDataArray);
        //        characterData.DebugParametaView();
        //    }
        //}
    }

    List<string[]> ConvertToArrayListFrom(string _text)
    {
        List<string[]> characterDataArrayList = new List<string[]>();
        StringReader reader = new StringReader(_text);
        reader.ReadLine();  // 1�s�ڂ̓��x���Ȃ̂ŊO��
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();        // ��s���ǂݍ���
            string[] elements = line.Split(',');    // �s�̃Z����,�ŋ�؂���
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;                       // �󔒂͏���
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            characterDataArrayList.Add(elements);
        }
        return characterDataArrayList;
    }
}