using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[CustomEditor(typeof(ZoneOfPart))]
public class ZoneOfPartEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        ZoneOfPart example = (ZoneOfPart)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(example.center, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Area Of Effect");
            example.offset = newTargetPosition - example.transform.position;
        }
    }
}

#endif
