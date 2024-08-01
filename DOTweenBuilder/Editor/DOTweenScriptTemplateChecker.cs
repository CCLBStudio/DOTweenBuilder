using System.IO;
using CCLBStudio;
using CCLBStudioEditor;
using UnityEditor;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    //[InitializeOnLoad]
    public static class DOTweenScriptTemplateChecker
    {
        private const string ScriptTemplateFolder = "ScriptTemplates";

        static DOTweenScriptTemplateChecker()
        {
            EditorApplication.quitting += OnQuit;

            if (PlayerPrefs.GetInt(DOTweenBuilderEditorSettings.PlayerPrefNeverCheckTemplateAgain, -1) != -1)
            {
                return;
            }
            if (PlayerPrefs.GetInt(DOTweenBuilderEditorSettings.PlayerPrefTemplateCheckRequired, -1) != -1)
            {
                return;
            }
            
            EditorApplication.update += CheckTemplates;
        }

        private static void CheckTemplates()
        {
            EditorApplication.update -= CheckTemplates;

            var settings = EditorExtender.LoadScriptableAsset<DOTweenBuilderEditorSettings>();
            string fullTemplateFolderPath = Application.dataPath + "/" + ScriptTemplateFolder;
            if (!Directory.Exists(fullTemplateFolderPath))
            {
                PlayerPrefs.SetInt(DOTweenBuilderEditorSettings.PlayerPrefTemplateCheckRequired, 1);
                var window = EditorWindow.GetWindow<DOTweenTemplateCreateWindow>(false, "Template Script Generator", true);
                window.requireFileGeneration = true;
                window.requireFolderGeneration = true;
            }
            else
            {
                foreach (var template in settings.templateFiles)
                {
                    if (File.Exists(fullTemplateFolderPath + $"/{template.fileName}.txt"))
                    {
                        continue;
                    }
                    
                    PlayerPrefs.SetInt(DOTweenBuilderEditorSettings.PlayerPrefTemplateCheckRequired, 1);
                    EditorWindow.GetWindow<DOTweenTemplateCreateWindow>(false, "Template Script Generator", true).requireFileGeneration = true;
                    return;
                }
            }
        }

        private static void OnQuit()
        {
            PlayerPrefs.SetInt(DOTweenBuilderEditorSettings.PlayerPrefTemplateCheckRequired, -1);
        }
    }
}
