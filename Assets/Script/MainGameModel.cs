using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameModel  {
	//打つ予定の文字列
	//打つ文字のカウンター
	//今回打つ予定の文字
	//

	private Dictionary <string,KanaKeyMapInfo> _kanaKeyMapInfo;
	//キーマップ情報
 	public Dictionary <string,KanaKeyMapInfo> KanaKeyMapInfoData
	{
	 	get{return _kanaKeyMapInfo;}
	}
	
    // コンストラクタ
    public MainGameModel()
    {
        //キーマップ情報を取得する
		_kanaKeyMapInfo = Util.ReadKeyMapInfo();

    }
}
