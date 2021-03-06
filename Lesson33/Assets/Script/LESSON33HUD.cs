using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class LESSON33HUD : MonoBehaviour {

	public GUIStyle styleTitle;
	public GUIStyle styleStart;
	public GUIStyle styleExit;


    void OnGUI(){
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.FlexibleSpace();
            drawLayout();
            GUILayout.FlexibleSpace();
        GUILayout.EndArea();
    }
    

    void drawLayout(){
        GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
		GUILayout.Label("", styleTitle);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
		GUILayout.Button("", styleStart);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
		GUILayout.Button("", styleExit);
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

}