using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCLBStudio.DOTweenBuilder
{
    [CreateAssetMenu(menuName = "CCLB Studio/Systems/DOTween Builder/Editor Settings", fileName = "DOTweenBuilderEditorSettings")]
    public class DOTweenBuilderEditorSettings : ScriptableObject
    {
        public static string PlayerPrefTemplateCheckRequired => "DOTweenBuilder_requireTemplateCheck";
        public static string PlayerPrefNeverCheckTemplateAgain => "DOTweenBuilder_neverCheckTemplateAgain";
        public string DOTweenVariableTemplateId => "DOTweenVariable";
        public string DOTweenScriptableValueTemplateId => "DOTweenScriptableValue";
        public static List<DOTweenTrackedType> TrackedTypes { get; set; }
        public static List<DOTweenTrackedType> TrackedScriptableVariableTypes => GetTrackedScriptableTypes();
        private static bool _updatedTrackedSoTypes = false;

        public Color deleteButtonColor = Color.red;
        public Color foldoutColor = Color.yellow;
        public Color addNewButtonColor = Color.white;
        public Texture2D iconNormalValue;
        public Texture2D iconScriptableValue;

        public List<string> foldersToIgnore = new List<string>() {"Editor", "Main"};
        public List<TemplateFile> templateFiles;
        
        private static List<DOTweenTrackedType> _trackedScriptableVariableTypes;
        
        private static List<DOTweenTrackedType> GetTrackedScriptableTypes()
        {
            if (_updatedTrackedSoTypes)
            {
                return _trackedScriptableVariableTypes;
            }

            var tracker = new DOTweenTypesTracker();
            _trackedScriptableVariableTypes = tracker.TrackScriptableTypes();
            _updatedTrackedSoTypes = true;

            return _trackedScriptableVariableTypes;
        }
    }

    [Serializable]
    public struct TemplateFile
    {
        public string id;
        public string fileName;
        [TextArea(12, 100)]
        public string fileContent;
    }
}
