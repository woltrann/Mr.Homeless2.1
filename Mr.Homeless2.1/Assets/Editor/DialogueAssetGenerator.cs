using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using Unity.Plastic.Newtonsoft.Json;

public class DialogueAssetGenerator
{
    [MenuItem("Tools/Dialogue/Create CallDialogues from JSON")]
    public static void CreateDialogueAssets()
    {
        string sourceFolder = Path.Combine(Application.streamingAssetsPath, "Dialogues");
        string targetFolder = "Assets/GameData/Dialogue";

        if (!Directory.Exists(sourceFolder))
        {
            Debug.LogError($"JSON klasörü bulunamadý: {sourceFolder}");
            return;
        }

        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }

        string[] jsonFiles = Directory.GetFiles(sourceFolder, "*.json");
        int createdCount = 0;

        foreach (string jsonFilePath in jsonFiles)
        {
            try
            {
                string jsonText = File.ReadAllText(jsonFilePath);
                DialogueData data = JsonConvert.DeserializeObject<DialogueData>(jsonText);

                if (data == null)
                {
                    Debug.LogWarning($"Veri parse edilemedi: {jsonFilePath}");
                    continue;
                }

                CallDialogue asset = ScriptableObject.CreateInstance<CallDialogue>();
                asset.data = data;

                string fileName = Path.GetFileNameWithoutExtension(jsonFilePath);
                string assetPath = Path.Combine(targetFolder, fileName + ".asset").Replace("\\", "/");

                AssetDatabase.CreateAsset(asset, assetPath);
                createdCount++;
                Debug.Log($"Oluþturuldu: {assetPath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON dosyasý okunamadý: {jsonFilePath}\n{ex.Message}");
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Toplam {createdCount} adet ScriptableObject oluþturuldu.");
    }
}
