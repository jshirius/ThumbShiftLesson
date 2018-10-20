using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MainGameModel  {


	//ターゲット文字列
	private string _targetCharas;
	public string TargetCharas{
		get{return _targetCharas;}
		set{_targetCharas = value;}
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

	//キー位置情報
	private Dictionary <string,KanaKeyPosInfo> _kanaKeyPosInfo;
 	public Dictionary <string,KanaKeyPosInfo> KanaKeyPosInfoData
	{
	 	get{return _kanaKeyPosInfo;}
	}

	//キーの練習用シナリオ情報
	//training_history
	private List<TrainingHistoryInfo> _trainingHistory;
 	public List<TrainingHistoryInfo> TrainingHistory
	{
	 	get{return _trainingHistory;}
	}

	//練習シナリオのindex
	private ReactiveProperty<int> _trainingHistoryIndex;
    public IReadOnlyReactiveProperty<int> TrainingHistoryIndex
    {
        get { return _trainingHistoryIndex; }
    }


    // コンストラクタ
    public MainGameModel()
    {
        //キーマップ情報を取得する
		_kanaKeyMapInfo = Util.ReadKeyMapInfo();

		//キー位置情報を取得する	
		_kanaKeyPosInfo = Util.ReadKeyPosInfo();
		Util.CompletionKeyPosInfo(_kanaKeyPosInfo);

		//練習ファイルを読み込む
		_trainingHistory = Util.ReadTrainingHistory();

		//ターゲットindexの初期化
		_targetIndex = new ReactiveProperty<int>();

		//練習文字列のindex
		_trainingHistoryIndex = new ReactiveProperty<int>();

		//ターゲット文字列を適当に入れておく
		_targetCharas = _trainingHistory[0].trainingString;
    }

	//KanaKeyPosInfoにデータを補完
	//Completion
	//public kanaKeyPosInfo

	//次のターゲット文字列へ
	public void NextTargetChara(){

		if(_targetCharas.Length-1 > _targetIndex.Value){
			_targetIndex.Value++;
		}else{
			//全ての文字を入力したら元に戻る
			_targetIndex.Value = 0;
		}
		
	}

	public void ResetTargetCharas(int index){
		//文字設定
		_targetIndex.Value = 0;
		_targetCharas = _trainingHistory[index].trainingString;
	}

	//練習する文字列の位置変更
	//dec true:マイナス  false:プラス
	public void ChangeTrainingHistory(bool dec){

		//プラスの場合の処理
		if(dec == false){
			if(_trainingHistoryIndex.Value  >= _trainingHistory.Count -1  ){
				_trainingHistoryIndex.Value = 0;
			}
			else{
				_trainingHistoryIndex.Value++;
			}
		}
		//マイナスのとき
		else{
			if(_trainingHistoryIndex.Value <= 0){
				_trainingHistoryIndex.Value = _trainingHistory.Count -1 ;
			}
			else{
				_trainingHistoryIndex.Value--;
			}
		}


	}

}
