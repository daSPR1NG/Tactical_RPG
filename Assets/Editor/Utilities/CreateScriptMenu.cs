using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace dnSR_Coding
{
    ///<summary>
    /// Create Script Menu helps us create scripts
    ///</summary>
     public static class CreateScriptMenu
     {
        #region Behaviour Related

        #region MonoBehaviour

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/Create MonoBehaviour", priority = 0 )]
        [MenuItem( "Create Script Menu/Create MonoBehaviour", priority = 0 )]
        #endregion
        static void CreateMonoBehaviourMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create Mono Behaviour", GetCurrentPath(), "NewMonoBehaviour.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/MonoBehaviourTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #region Abstract Class

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/Behaviour/Create Abstract Class" )]
        [MenuItem( "Create Script Menu/Behaviour/Create Abstract Class" )]
        #endregion
        static void CreateAbstractClassMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create Abstract Class", GetCurrentPath(), "NewAbstractClass.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/AbstractClassTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #region Scriptable Object

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/Behaviour/Create Scriptable Object" )]
        [MenuItem( "Create Script Menu/Behaviour/Create Scriptable Object" )]
        #endregion
        static void CreateScriptableObjectMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create ScriptableObject", GetCurrentPath(), "NewScriptableObject.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/ScriptableObjectTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #region Interface

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/Behaviour/Create Interface" )]
        [MenuItem( "Create Script Menu/Behaviour/Create Interface" )]
        #endregion
        static void CreateInterfaceMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create Interface", GetCurrentPath(), "NewInterface.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/InterfaceTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #endregion

        #region Editor Related

        #region Editor Class

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/Editor/Create Editor" )]
        [MenuItem( "Create Script Menu/Editor/Create Editor" )]
        #endregion
        static void CreateEditorMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create Editor", GetCurrentPath(), "NewEditor.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/EditorTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #region Property Drawer

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/Editor/Create Property Drawer" )]
        [MenuItem( "Create Script Menu/Editor/Create Property Drawer" )]
        #endregion
        static void CreatePropertyDrawerMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create Property Drawer", GetCurrentPath(), "NewPropertyDrawer.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/PropertyDrawerTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #endregion

        #region ECS

        #region System

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/ECS/Create System" )]
        [MenuItem( "Create Script Menu/ECS/Create System" )]
        #endregion
        static void CreateECSSystemMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create System", GetCurrentPath(), "NewSystem.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/ECS/ECSSystemTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #region Component

        #region Menu Items
        [MenuItem( "Assets/Create Script Menu/ECS/Create Component" )]
        [MenuItem( "Create Script Menu/ECS/Create Component" )]
        #endregion
        static void CreateECSComponentMenuItem()
        {
            string pathToNewFile = EditorUtility.SaveFilePanel( "Create Component", GetCurrentPath(), "NewComponent.cs", "cs" );
            string pathToTemplate = Application.dataPath + "/ScriptTemplates/ECS/ECSComponentTemplate.txt";

            MakeScriptFromTemplate( pathToNewFile, pathToTemplate );
        }

        #endregion

        #endregion

        static void MakeScriptFromTemplate( string pathToNewFile, string pathToTemplate )
        {
            if ( !string.IsNullOrWhiteSpace( pathToNewFile ) )
            {
                FileInfo fileInfo = new( pathToNewFile );
                string nameOfScript = Path.GetFileNameWithoutExtension( fileInfo.Name );

                string text = File.ReadAllText( pathToTemplate );

                text = text.Replace( "#SCRIPTNAME#", nameOfScript );
                text = text.Replace( "#SCRIPTNAMEWITHOUTEDITOR#", nameOfScript.Replace( "Editor", "") );
                text = text.Replace( "#SCRIPTNAMEWITHOUTDRAWER#", nameOfScript.Replace( "Drawer", "") );

                File.WriteAllText( pathToNewFile, text );
                AssetDatabase.Refresh();
            }
        }

        static string GetCurrentPath()
        {
            string path = AssetDatabase.GUIDToAssetPath( Selection.assetGUIDs [ 0 ] );
            if ( path.Contains(".") )
            {
                int index = path.LastIndexOf( "/" );
                path = path.Substring( 0, index );
            }

            return path;
        }
     }
}