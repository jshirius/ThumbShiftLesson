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
    /// ターゲット文字列を取得する
    /// </summary>
	static public string TargetCharas(){
        TextAsset csvFile = Resources.Load("File/target_charas") as TextAsset; 
        StringReader reader = new StringReader(csvFile.text);

		string readString = "";
        int height = 0;
        while(reader.Peek() > -1) {
			
            string line = reader.ReadLine();

			readString += line;
        }

		return readString;
	}

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
            info.reverseShift =  int.Parse(str[4]);

            kanaKeyMapInfo[info.kana] = info;
        }

		return kanaKeyMapInfo;

    }

  /// <summary>
    /// キーの位置情報を取得（カナ文字の設定なし）
    /// </summary>
    static public Dictionary<string,KanaKeyPosInfo> ReadKeyPosInfo(){

        TextAsset csvFile = Resources.Load("File/key_pos") as TextAsset; /* Resouces/CSV下のCSV読み込み */
        StringReader reader = new StringReader(csvFile.text);

        Dictionary <string,KanaKeyPosInfo> kanaKeyPosInfo = new  Dictionary <string,KanaKeyPosInfo>();
        int height = 0;
        while(reader.Peek() > -1) {
			
            string line = reader.ReadLine();

            height++; // 行数加算
            if(height == 1) continue;   //１行目はコメントのため飛ばす
            

            KanaKeyPosInfo info = new KanaKeyPosInfo();

            string[] str = line.Split('\t');
            info.typeKey = str[0];
            info.yPos = int.Parse(str[1]);
            info.xPos = int.Parse(str[2]);
       

            kanaKeyPosInfo[info.typeKey] = info;
        }

		return kanaKeyPosInfo;

    }	

  /// <summary>
    /// キーの文字を仮名文字で補完
    /// </summary>
    static public void  CompletionKeyPosInfo(Dictionary<string,KanaKeyPosInfo> readKeyPosInfos){
        //キー情報を呼び出す
        Dictionary<string,KanaKeyMapInfo> readKeyMapInfos;
        readKeyMapInfos = Util.ReadKeyMapInfo();

        //キーボード表示のカナを設定する
        foreach(var readKeyPosInfo in readKeyPosInfos){

            //平仮名のループ
            foreach(var readKeyMapInfo in readKeyMapInfos){
                //キーボード表示のカナを設定する
                if(readKeyMapInfo.Value.typeKey == readKeyPosInfo.Value.typeKey){
                    
                    //シフトなし
                    if(readKeyMapInfo.Value.leftShift == 0 && readKeyMapInfo.Value.rightShift == 0){
                        readKeyPosInfo.Value.kana1 = readKeyMapInfo.Value.kana;
                    }else if(readKeyMapInfo.Value.reverseShift == 0 ){
                        if(readKeyMapInfo.Value.leftShift == 1 || readKeyMapInfo.Value.rightShift == 1){
                            readKeyPosInfo.Value.kana2 = readKeyMapInfo.Value.kana;
                        }
                        
                    }else if(readKeyMapInfo.Value.reverseShift == 1){
                        if(readKeyMapInfo.Value.leftShift == 1 || readKeyMapInfo.Value.rightShift == 1){
                            readKeyPosInfo.Value.kana3 = readKeyMapInfo.Value.kana;
                        }
                    }
                }
            }
        }  
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
