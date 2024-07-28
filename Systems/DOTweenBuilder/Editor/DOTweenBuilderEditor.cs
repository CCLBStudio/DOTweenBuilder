using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CCLBStudio.Utils.Extensions;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace CCLBStudio.Systems.DOTweenBuilder
{
    [CustomEditor(typeof(DOTweenBuilder))]
    public class DOTweenBuilderEditor : Editor
    {
        public static DOTweenBuilderEditorSettings Settings;
        private static List<DOTweenGenericElementDrawer> _drawers = new List<DOTweenGenericElementDrawer>();

        private SerializedProperty _tweenElements;
        private SerializedProperty _loop;
        private SerializedProperty _loopType;
        private SerializedProperty _onCompleted;
        
        private ReorderableList _list;
        private int _removeAt;
        private List<SerializedProperty> _toDraw;
        private GenericMenu _menu;
        private GUIStyle _addNewStyle;
        private GUIStyle _deleteStyle;

        private void OnEnable()
        {
            _removeAt = -1;
            _tweenElements = serializedObject.FindProperty(DOTweenBuilder.TweenElementsProperty);
            _loop = serializedObject.FindProperty(DOTweenBuilder.LoopProperty);
            _loopType = serializedObject.FindProperty(DOTweenBuilder.LoopTypeProperty);
            _onCompleted = serializedObject.FindProperty(DOTweenBuilder.OnCompletedProperty);
            
            
            _list = new ReorderableList(serializedObject, _tweenElements, true, false, false, false)
            {
                drawElementCallback = DrawListItems,
                elementHeightCallback = ElementHeightCallback
            };
            
            Settings = EditorExtender.LoadScriptableAsset<DOTweenBuilderEditorSettings>();
            _toDraw = EditorExtender.GetSelfPropertiesExcluding(serializedObject, DOTweenBuilder.TweenElementsProperty, DOTweenBuilder.LoopProperty, DOTweenBuilder.LoopTypeProperty, DOTweenBuilder.OnCompletedProperty);
            
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            string relativePath = AssetDatabase.GetAssetPath(Settings);
            string absolutePath = Path.GetFullPath(relativePath);
            string settingsFolder = Path.GetDirectoryName(absolutePath);

            if (!Directory.Exists(settingsFolder))
            {
                throw new Exception($"Folder at path {settingsFolder} does not exists.");
            }
            
            string parentFolder = Directory.GetParent(settingsFolder)?.FullName + "/";
            List<string> filteredFolders = Directory.EnumerateDirectories(parentFolder).Where(x => !Settings.foldersToIgnore.Contains(Path.GetFileName(x))).ToList();
            _menu = new GenericMenu();

            foreach (var folderPath in filteredFolders)
            {
                IEnumerable<string> files = Directory.EnumerateFiles(folderPath, "*cs", SearchOption.AllDirectories);
                string familyName = ObjectNames.NicifyVariableName(Path.GetFileName(folderPath));
                
                foreach (var file in files)
                {
                    string relative = "Assets/" + Path.GetRelativePath(Application.dataPath, file);
                    Type type = AssetDatabase.LoadAssetAtPath<MonoScript>(relative).GetClass();
                    if (!typeof(DOTweenElement).IsAssignableFrom(type))
                    {
                        continue;
                    }
                    
                    string typeNameModified = type.Name.Replace("Tween", "");
                    
                    string menuPath = familyName + "/" + ObjectNames.NicifyVariableName(typeNameModified);
                    _menu.AddItem(new GUIContent(menuPath), false, MenuAction, type);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorExtender.DrawScriptField(serializedObject);
            if (GUILayout.Button("Ease Functions", EditorExtender.URLButtonStyle))
            {
                Application.OpenURL("https://easings.net/");
            }

            EditorGUI.BeginChangeCheck();
            
            if (_removeAt >= 0)
            {
                _tweenElements.DeleteArrayElementAtIndex(_removeAt);
                _removeAt = -1;
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);

                foreach (var t in _drawers)
                {
                    t.ClearInternalDrawers();
                }
                
                _drawers.Clear();
                return;
            }
            
            EditorExtender.DrawProperties(_toDraw);
            EditorGUILayout.PropertyField(_loop);
            if (_loop.boolValue)
            {
                EditorGUILayout.PropertyField(_loopType);
            }
            GUILayout.Space(5);
            EditorGUILayout.PropertyField(_onCompleted);
            
            EditorGUILayout.Space(5);

            _deleteStyle ??= new GUIStyle(GUI.skin.button)
            {
                normal = { textColor = Settings.deleteButtonColor },
                hover = { textColor = Settings.deleteButtonColor },
                fontStyle = FontStyle.Bold
            };

            _list.DoLayoutList();

            _addNewStyle ??= new GUIStyle(GUI.skin.button)
            {
                normal = { textColor = Settings.addNewButtonColor },
                hover = { textColor = Settings.addNewButtonColor },
                fontStyle = FontStyle.Bold
            };

            if (GUILayout.Button("Add New...", _addNewStyle, GUILayout.Width(250), GUILayout.Height(30)))
            {
                _menu.ShowAsContext();
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            EditorGUI.indentLevel++;
            Rect actualRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            var p = _tweenElements.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(actualRect, p);
            
            EditorGUI.indentLevel--;

            if (!p.isExpanded)
            {
                return;
            }

            float btnWidth = Mathf.Min(200, rect.width / 2f);
            rect.y += EditorGUI.GetPropertyHeight(p, true) - EditorGUIUtility.singleLineHeight;
            rect.x = (rect.width - btnWidth) / 2f;
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.width = btnWidth;

            if (GUI.Button(rect, "Delete", _deleteStyle))
            {
                _removeAt = index;
            }
        }
        
        private float ElementHeightCallback(int index)
        {
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(element, true);
        }
        
        private void MenuAction(object userData)
        {
            Type type = userData as Type;
            if (null == type)
            {
                Debug.LogError($"Parameter {userData} is not a type.");
                return;
            }

            if (!typeof(DOTweenElement).IsAssignableFrom(type))
            {
                Debug.LogError($"Type {type.Name} needs to be in the inheritance tree of {nameof(DOTweenElement)}.");
                return;
            }

            var script = (DOTweenBuilder)target;
            var instance = Activator.CreateInstance(type);
            script.TweenElements.Add(instance as DOTweenElement);
            EditorUtility.SetDirty(target);
        }

        public static void Hello(DOTweenGenericElementDrawer drawer)
        {
            if (!_drawers.Contains(drawer))
            {
                _drawers.Add(drawer);
            }
        }
    }
}
