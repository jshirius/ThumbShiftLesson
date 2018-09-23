using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MainGameModel  {
	//打つ予定の文字列
	//打つ文字のカウンター
	//今回打つ予定の文字
	//

	//ターゲット文字列
	private string _targetCharas;
	public string TargetCharas{
		get{return _targetCharas;}
	}

	//ターゲット文字列のindex
	private ReactiveProperty<int> _targetIndex;
    public IReadOnlyReactiveProperty<int> TargetIndex
    {
        get { return _targetIndex; }
    }

	//キーマップ情報
	private Dictionary <string,KanaKeyMapInfo> _kanaKeyMapInfo;
 	public Dictionary <string,KanaKeyMapInfo> KanaKeyMapInfoData
	{
	 	get{return _kanaKeyMapInfo;}
	}


    // コンストラクタ
    public MainGameModel()
    {
        //キーマップ情報を取得する
		_kanaKeyMapInfo = Util.ReadKeyMapInfo();

		//ターゲット文字列を適当に入れておく
		_targetCharas = "あいうえおしなもをゅ";

		//ターゲットindexの初期化
		_targetIndex = new ReactiveProperty<int>();

    }

	//次のターゲット文字列へ
	public void NextTargetChara(){

		if(_targetCharas.Length >= _targetIndex.Value){
			_targetIndex.Value++;
		}
		
	}
}
