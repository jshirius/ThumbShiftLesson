using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// ユーティリティクラス
/// 単一の機能を提供する
/// </summary>
public class Util  {


    /// <summary>
    /// キー情報を取得する
    /// </summary>
    static public Dictionary<string,KanaKeyMapInfo> ReadKeyMapInfo(){

        TextAsset csvFile = Resources.Load("File/kana_key_map") as TextAsset; /* Resouces/CSV下のCSV読み込み */
        StringReader reader = new StringReader(csvFile.text);

        Dictionary <string,KanaKeyMapInfo> kanaKeyMapInfo = new  Dictionary <string,KanaKeyMapInfo>();
        int height = 0;
        while(reader.Peek() > -1) {

            string line = reader.ReadLine();

            height++; // 行数加算
            if(height == 1) continue;   //１行目はコメントのため飛ばす
            

            KanaKeyMapInfo info = new KanaKeyMapInfo();

            string[] str = line.Split('\t');
            info.kana = str[0];
            info.typeKey = str[1];
            info.leftShift = int.Parse(str[2]);
            info.rightShift = int.Parse(str[3]);

            kanaKeyMapInfo[info.kana] = info;
        }

		return kanaKeyMapInfo;

    }

    /// <summary>
    /// IListをstring型に変換する
    /// </summary>
    static public  string  GetKeybordCharactor(IList<string> inputdatas){

        string str = "";

        foreach(string s in inputdatas){
            str += s;
        }        
        Debug.Log("文字列:"+str + "文字数" + str.Length);

        return str;
    }


}
