using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemyControl))]
public class EnemyControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EnemyControl enemyControl = target as EnemyControl;

        enemyControl.Type = (EnemyControl.MoveType)EditorGUILayout.EnumFlagsField("Movetype", enemyControl.Type);

        EditorGUI.indentLevel++;    //インデントを入れる
        
        switch (enemyControl.Type)
        {
            case EnemyControl.MoveType.Position:
                enemyControl.moveParam.direction = EditorGUILayout.Vector3Field("Direction", enemyControl.moveParam.direction);
                enemyControl.moveParam.speed = EditorGUILayout.FloatField("Speed", enemyControl.moveParam.speed);
                enemyControl.moveParam.startTime = EditorGUILayout.FloatField("StartTime", enemyControl.moveParam.startTime);
                enemyControl.moveParam.movingTime = EditorGUILayout.FloatField("MovingTime", enemyControl.moveParam.movingTime);
                break;

            case EnemyControl.MoveType.Rotation:
                enemyControl.rotateParam.LR = (EnemyControl.EnemyRotate.Direction)EditorGUILayout.EnumPopup("Direction", enemyControl.rotateParam.LR);
                enemyControl.rotateParam.speed = EditorGUILayout.FloatField("Speed", enemyControl.rotateParam.speed);
                break;

            case (EnemyControl.MoveType)(-1):
                enemyControl.moveParam.direction = EditorGUILayout.Vector3Field("Position Direction", enemyControl.moveParam.direction);
                enemyControl.moveParam.speed = EditorGUILayout.FloatField("Position Speed", enemyControl.moveParam.speed);
                enemyControl.moveParam.startTime = EditorGUILayout.FloatField("Position StartTime", enemyControl.moveParam.startTime);
                enemyControl.moveParam.movingTime = EditorGUILayout.FloatField("Position MovingTime", enemyControl.moveParam.movingTime);
            
                enemyControl.rotateParam.LR = (EnemyControl.EnemyRotate.Direction)EditorGUILayout.EnumPopup("Rotation Direction", enemyControl.rotateParam.LR);
                enemyControl.rotateParam.speed = EditorGUILayout.FloatField("Rotation Speed", enemyControl.rotateParam.speed);
                break;
        }

        EditorGUI.indentLevel--;    //インデントを戻す

        //obj.iconImage = EditorGUILayout.TextField("Icon Image", obj.iconImage); //アイコン画像

        EditorUtility.SetDirty(target);
    }
}
