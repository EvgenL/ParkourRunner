using UnityEngine;
using System.Collections;
using UnityEditor;
using Invector;
using Invector.CharacterController;
using Shooter.Scripts.Shooter;

[CanEditMultipleObjects]
[CustomEditor(typeof(vShooterManager), true)]
public class vShooterManagerEditor : vEditorBase
{
    protected override void OnEnable()
    {        
        base.OnEnable();
        m_Logo = Resources.Load("shooterIcon") as Texture2D;
    }

    public override void OnInspectorGUI()
    {
        GUI.skin = skin;

        vShooterManager manager = (vShooterManager)this.target;

        GUILayout.BeginVertical("SHOOTER MANAGER", "window");
        GUILayout.Label(m_Logo, GUILayout.MaxHeight(25));

        openCloseWindow = GUILayout.Toggle(openCloseWindow, openCloseWindow ? "Close" : "Open", EditorStyles.toolbarButton);

        if (openCloseWindow)
        {
            base.OnInspectorGUI();

            if (Application.isPlaying && manager.tpCamera)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.HelpBox("Playmode Debug - Equip a weapon first", MessageType.Info);
                EditorGUILayout.Space();

                if (GUILayout.Button(manager.tpCamera.lockCamera ? "Unlock Camera" : "Lock Camera", EditorStyles.toolbarButton))
                {
                    manager.tpCamera.lockCamera = !manager.tpCamera.lockCamera;
                }
                EditorGUILayout.Space();
                if (GUILayout.Button(manager.alwaysAiming ? "Unlock Aiming" : "Lock Aiming", EditorStyles.toolbarButton))
                {
                    manager.alwaysAiming = !manager.alwaysAiming;
                }
                EditorGUILayout.Space();
                if (GUILayout.Button(manager.showCheckAimGizmos ? "Hide Aim Gizmos" : "Show Aim Gizmos", EditorStyles.toolbarButton))
                {
                    manager.showCheckAimGizmos = !manager.showCheckAimGizmos;
                }
                EditorGUILayout.EndVertical();
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space();
    }
}
