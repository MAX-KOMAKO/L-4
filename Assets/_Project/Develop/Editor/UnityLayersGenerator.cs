using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets._Project.Develop.Editor
{
    public static class UnityLayersGenerator
    {
        private const string MenuPath = "Tools/Generate Unity Layers";
        private const string FilePath = "Assets/_Project/Develop/Runtime/Utilities/UnityLayers.cs";

        [MenuItem(MenuPath)]
        [InitializeOnLoadMethod]
        public static void Generate()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("using UnityEngine;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("namespace Assets._Project.Develop.Runtime.Utilities");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    public static class UnityLayers");
            stringBuilder.AppendLine("    {");

            string[] layerNames = InternalEditorUtility.layers;
            foreach (string layerName in layerNames)
            {
                if (string.IsNullOrEmpty(layerName))
                    continue;

                string cleanName = layerName.Replace(" ", "");
                int layerIndex = LayerMask.NameToLayer(layerName);
                stringBuilder.AppendLine($"        public static readonly int Layer{cleanName} = {layerIndex};");
                stringBuilder.AppendLine($"        public static readonly int LayerMask{cleanName} = 1 << {layerIndex};");
            }

            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");

            string directoryPath = Path.GetDirectoryName(FilePath);
            if (Directory.Exists(directoryPath) == false)
                Directory.CreateDirectory(directoryPath!);

            File.WriteAllText(FilePath, stringBuilder.ToString());
            AssetDatabase.Refresh();
        }
    }
}