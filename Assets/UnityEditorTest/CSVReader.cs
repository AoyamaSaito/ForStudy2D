using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    const string SHEET_ID = "シートID";
    const string SHEET_NAME = "シート1";

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
                    @"サーバとの通信に失敗。
リクエストが接続できなかった、
セキュリティで保護されたチャネルを確立できなかったなど。"
                );
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log
                (
                    @"サーバがエラー応答を返した。
サーバとの通信には成功したが、
接続プロトコルで定義されているエラーを受け取った。"
                );
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log
                (
                    @"データの処理中にエラーが発生。
リクエストはサーバとの通信に成功したが、
受信したデータの処理中にエラーが発生。
データが破損しているか、正しい形式ではないなど。"
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
        reader.ReadLine();  // 1行目はラベルなので外す
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();        // 一行ずつ読み込み
            string[] elements = line.Split(',');    // 行のセルは,で区切られる
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    continue;                       // 空白は除去
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            characterDataArrayList.Add(elements);
        }
        return characterDataArrayList;
    }
}