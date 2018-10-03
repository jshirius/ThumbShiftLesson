using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateKeyButton : EditorWindow {

	private GameObject parent;
	private GameObject prefab;
	

    [MenuItem("GameObject/Create KeyBotton")]
    static void Init() {
        EditorWindow.GetWindow<CreateKeyButton>(true, "Create KeyBotton");
    }

	void OnGUI() {
		try {
			parent = EditorGUILayout.ObjectField("Parent", parent, typeof(GameObject), true) as GameObject;
			prefab = EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true) as GameObject;
	
		    GUILayout.Label("", EditorStyles.boldLabel);
        	if (GUILayout.Button("Create")) Create();
		} catch (System.FormatException) {}
	}

	private void Create() {
		if (prefab == null) return;

		 GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                obj.name = prefab.name ;
        if (parent) obj.transform.parent = parent.transform;
	}
}
