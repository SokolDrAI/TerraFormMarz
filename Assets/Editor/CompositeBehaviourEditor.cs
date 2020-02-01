using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositBehaviour))]
public class CompositeBehaviourEditor : Editor {


    public override void OnInspectorGUI()
    {
        //Setting up inspector
        CompositBehaviour cb = (CompositBehaviour)target;

        Rect r = EditorGUILayout.BeginHorizontal();
        r.height = EditorGUIUtility.singleLineHeight;

        //check for behaviours
        if(cb.behaviours == null||cb.behaviours.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            r.height = EditorGUIUtility.singleLineHeight;
        }
        else
        {
            r.x = 30f;
            r.width = EditorGUIUtility.currentViewWidth - 95f;
            EditorGUI.LabelField(r, "Behaviours");
            r.x = EditorGUIUtility.currentViewWidth - 65f;
            r.width = 60f;
            EditorGUI.LabelField(r, "Weights");
            r.y += EditorGUIUtility.singleLineHeight * 1.2f;

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                r.x = 5f;
                r.width = 20f;
                EditorGUI.LabelField(r, i.ToString());
                r.x = 30f;
                r.width = EditorGUIUtility.currentViewWidth - 95f;
                cb.behaviours[i] = (FlockBehaviour)EditorGUI.ObjectField(r, cb.behaviours[i], typeof(FlockBehaviour), false);

                r.x = EditorGUIUtility.currentViewWidth - 65f;
                r.width = 60f;
                cb.weights[i] = EditorGUI.FloatField(r, cb.weights[i]);
                r.y += EditorGUIUtility.singleLineHeight * 1.1f;
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);
            }
            EditorGUILayout.EndHorizontal();
        }

        r.x = 5f;
        r.width = EditorGUIUtility.currentViewWidth - 10f;
        r.y += EditorGUIUtility.singleLineHeight * 0.5f;

        if (GUI.Button(r, "Add Behaviour"))
        {
            AddBehavior(cb);
            EditorUtility.SetDirty(cb);
        }

        r.y += EditorGUIUtility.singleLineHeight * 1.5f;
        if (cb.behaviours.Length > 0 && cb.behaviours != null)
        {
            if (GUI.Button(r, "Remove Behaviour"))
            {
                RemoveBehaviours(cb);
                EditorUtility.SetDirty(cb);
            }
        }
    }

    void AddBehavior ( CompositBehaviour cb)
    {
        int oldcount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldcount + 1];
        float[] newWeights = new float[oldcount + 1];
        for (int i = 0; i < oldcount; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldcount] = 1f;
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }


    void RemoveBehaviours(CompositBehaviour cb)
    {
        int oldcount = cb.behaviours.Length;
        if(oldcount ==1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldcount - 1];
        float[] newWeights = new float[oldcount - 1];
        for (int i = 0; i < oldcount-1; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }

}
