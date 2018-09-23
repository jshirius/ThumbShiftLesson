using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;

public class MainGamePresenter : MonoBehaviour {

	[SerializeField]	
	private MainGameModel _model;

	// Use this for initialization
	void Start () {

		//モデル初期化
		_model = new MainGameModel();

		//キーボードからの入力を取得する
		var keyStream = Observable.EveryUpdate()
		.Select(_ => Input.inputString) 
		.Where(xs => Input.anyKeyDown);

		//同時押しの間隔を200msにする
		keyStream.Buffer(keyStream.Throttle(TimeSpan.FromMilliseconds(200)))
    		.Where(xs => xs.Count >= 1)
            .Subscribe(x =>
            {
                //文字列の処理
				/*
                string str = GetKeybordCharactor(x);
                _texts[3].text = str;

                bool rtn =CheckTargetCharactor("あ",x);
 				*/
                //Debug.Log(rtn);

				//入力された文字を出力する

            });

	}

}
