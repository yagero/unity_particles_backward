using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(ParticleSystem))]
public class PlayBackwardTest : MonoBehaviour
{
    [SerializeField] bool withChildren = true;
    Coroutine m_CoPlayBackward = null;

    public void Play()
    {
        StopBackward();
        var ps = GetComponent<ParticleSystem>();
        ps.Stop();
        ps.Clear();
        ps.Play(withChildren);
    }

    public void PlayBackward()
    {
        StopBackward();
        var ps = GetComponent<ParticleSystem>();
        m_CoPlayBackward = ps.PlayBackward(this, withChildren); // = StartCoroutine(ps.CoPlayBackward(withChildren));
    }

    void StopBackward()
    {
        if (m_CoPlayBackward != null)
            StopCoroutine(m_CoPlayBackward);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PlayBackwardTest))]
public class PlayBackwardTestEd : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = (PlayBackwardTest)target;
        DrawDefaultInspector();

        if (GUILayout.Button("Play"))
        {
            myScript.Play();
        }

        if (GUILayout.Button("Play Backward"))
        {
            myScript.PlayBackward();
        }
    }
}
#endif


