using UnityEngine;
using UnityEditor;

namespace dnSR_Coding
{
    ///<summary> DefaultUIWindowEditor description <summary>
    [CanEditMultipleObjects]
    [CustomEditor( typeof(DefaultUIWindow), true )]
    public class DefaultUIWindowEditor : BaseEditor
    {
        DefaultUIWindow _defaultUIWindow = null;

        private void OnEnable()
        {
            _defaultUIWindow = ( DefaultUIWindow ) target;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Space( 20f );
            using ( new GUILayout.VerticalScope() )
            {
                GUILayout.FlexibleSpace();

                using ( new GUILayout.HorizontalScope() )
                {
                    GUILayout.FlexibleSpace();

                    GUIStyle editStyle = new( GUI.skin.button )
                    {
                        fontStyle = FontStyle.Bold
                    };
                    GUIContent editContent = new ( "Edit".ToUpper() );

                    if ( GUILayout.Button( editContent, editStyle, GUILayout.Height( 25f ), GUILayout.Width( 125f ) ) )
                    {
                        Transform parentOfWindow = _defaultUIWindow.transform.parent;

                        SceneVisibilityManager.instance.Hide( parentOfWindow.gameObject, true );
                        SceneVisibilityManager.instance.Show( _defaultUIWindow.gameObject, true );
                    }

                    GUILayout.Space( 2f );

                    if ( GUILayout.Button( "Stop".ToUpper(), GUILayout.Height( 25f ), GUILayout.Width( 85f ) ) )
                    {
                        Transform parentOfWindow = _defaultUIWindow.transform.parent;

                        SceneVisibilityManager.instance.Show( parentOfWindow.gameObject, true );
                    }

                    GUILayout.FlexibleSpace();
                }

                GUILayout.FlexibleSpace();
            }                      

            GUILayout.Space( 10f );

            base.OnInspectorGUI();
        }
    }
}