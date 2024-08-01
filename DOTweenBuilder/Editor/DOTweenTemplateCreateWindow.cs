using System.IO;
using CCLBStudio;
using CCLBStudioEditor;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CCLBStudio.DOTweenBuilder
{
    public class DOTweenTemplateCreateWindow : EditorWindow
    {
        public bool requireFolderGeneration;
        public bool requireFileGeneration;
        private static readonly string[] DefaultTypes = new[] { "bool", "float", "string", "int", "TransformArray", nameof(Renderer), nameof(TextMeshProUGUI), nameof(Image), nameof(CanvasGroup), nameof(RectTransform),
            nameof(Vector2), nameof(Vector3), nameof(Transform), nameof(Camera), nameof(GameObject), nameof(AbsorbType), nameof(DOTweenEase), nameof(LoopType), nameof(DOTweenAxis), nameof(DOTweenAxis2D),
            nameof(DOTweenCameraClipPlane) , nameof(DOTweenAnchorType), nameof(Space), nameof(PathType), nameof(PathMode), nameof(AxisConstraint), nameof(DOTweenPathLookAtOption)};
        private string _templateFolderPath;
        private DOTweenBuilderEditorSettings _settings;

        [MenuItem("Tools/Demigiant/DOTween Builder Utility Panel")]
        public static void ShowWindow()
        {
            GetWindow<DOTweenTemplateCreateWindow>("My Custom Editor Window");
        }

        private void OnEnable()
        {
            _templateFolderPath = Application.dataPath + "/ScriptTemplates";
            _settings = EditorExtender.LoadScriptableAsset<DOTweenBuilderEditorSettings>();
        }

        private void OnGUI()
        {
            if (!requireFolderGeneration && !requireFileGeneration)
            {
                if (GUILayout.Button("Generate Default Types"))
                {
                    int variableIndex = _settings.templateFiles.FindIndex(x => x.id.Equals(_settings.DOTweenVariableTemplateId));
                    int scriptableValueIndex = _settings.templateFiles.FindIndex(x => x.id.Equals(_settings.DOTweenScriptableValueTemplateId));
                    if (variableIndex < 0 || scriptableValueIndex < 0)
                    {
                        Debug.LogError("Template for DOTweenVariable or DOTweenScriptableValue is missing. Can't generate files.");
                        return;
                    }

                    // string variableTemplate = _settings.templateFiles[variableIndex].fileContent;
                    // string scriptableValueTemplate = _settings.templateFiles[scriptableValueIndex].fileContent;
                    //
                    // string relativePath = AssetDatabase.GetAssetPath(_settings);
                    // string parent = Directory.GetParent(relativePath)?.FullName;
                    // string mainFolder = parent + "/Main/";
                    // string scriptableValueFolder = mainFolder + "ScriptableAsVariable/";
                    // string variableFolder = mainFolder + "DOTweenVariable/";
                    // bool createdOne = false;
                    
                    // foreach (var typeName in DefaultTypes)
                    // {
                    //     string typeCapitalized = char.ToUpper(typeName[0]) + typeName.Substring(1);
                    //     bool hasPrefix = typeCapitalized.StartsWith("DOTween");
                    //     string scriptableValueName = hasPrefix ? $"{typeCapitalized}Value" : $"DOTween{typeCapitalized}Value";
                    //     string scriptableValuePath = scriptableValueFolder + scriptableValueName + ".cs";
                    //     string variableName = hasPrefix ? $"{typeCapitalized}Variable" : $"DOTween{typeCapitalized}Variable";
                    //     string variablePath = variableFolder + variableName + ".cs";
                    //     string realTypeName = typeName.EndsWith("Array") ? typeName.Replace("Array", "[]") : typeName;
                    //
                    //     if (!File.Exists(scriptableValuePath))
                    //     {
                    //         string content = scriptableValueTemplate.Replace("#SCRIPTNAME#", scriptableValueName).Replace("TYPE", realTypeName);
                    //         File.WriteAllText(scriptableValuePath, content);
                    //         createdOne = true;
                    //     }
                    //     else
                    //     {
                    //         Debug.Log($"Script {scriptableValueName} already exist.");
                    //     }
                    //
                    //     if (!File.Exists(variablePath))
                    //     {
                    //         string content = variableTemplate.Replace("#SCRIPTNAME#", variableName).Replace("SCRIPTABLETYPE", scriptableValueName).Replace("TYPE", realTypeName);
                    //         File.WriteAllText(variablePath, content);
                    //         createdOne = true;
                    //     }
                    //     else
                    //     {
                    //         Debug.Log($"Script {variableName} already exist.");
                    //     }
                    // }
                    //
                    // if (createdOne)
                    // {
                    //     AssetDatabase.SaveAssets();
                    //     AssetDatabase.Refresh();
                    // }
                }
                
                return;
            }

            if (requireFolderGeneration)
            {
                GUILayout.TextArea("You are missing the ScriptTemplates folder in your project (see official documentation for more info).\n\n" +
                                "DOTween Builder use this concept to help you create custom data, See the Documentation file for more information.\n\n" +
                                "Create this missing folder ?", GUILayout.ExpandHeight(false));

                GUILayout.Space(10);
                if (GUILayout.Button("Create ScriptTemplates Folder"))
                {
                    if (!Directory.Exists(_templateFolderPath))
                    {
                        Directory.CreateDirectory(_templateFolderPath);
                    }

                    requireFolderGeneration = false;
                }
                
                GUILayout.Space(5);

                if (GUILayout.Button("Never Ask Again"))
                {
                    PlayerPrefs.SetInt(DOTweenBuilderEditorSettings.PlayerPrefNeverCheckTemplateAgain, 1);
                }
                return;
            }

            if (requireFileGeneration)
            {
                GUILayout.Label("You are missing at least one template file.");
                GUILayout.Space(5);
                GUILayout.Label("Create those missing files ?");
                if (GUILayout.Button("Create Missing Templates"))
                {
                    foreach (var templateFile in _settings.templateFiles)
                    {
                        string path = $"{_templateFolderPath}/{templateFile.fileName}.txt";
                        if (File.Exists(path))
                        {
                            Debug.Log($"File exist at path {path}");
                            continue;
                        }
                        
                        File.WriteAllText(path, templateFile.fileContent);
                    }
                    requireFileGeneration = false;
                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                }
                
                GUILayout.Space(5);

                if (GUILayout.Button("Never Ask Again"))
                {
                    PlayerPrefs.SetInt(DOTweenBuilderEditorSettings.PlayerPrefNeverCheckTemplateAgain, 1);
                }
            }
        }
    }
}
