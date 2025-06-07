using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization.Tables;
using Newtonsoft.Json.Linq;

public class JsonLocalizationImporter
{
    private const string TableCollectionName = "Words";
    private static readonly string JsonPath = "Assets/Localization/translations.json";

    [MenuItem("Tools/Import JSON to Localization Tables")]
    public static void ImportJsonToLocalizationTables()
    {
        var jsonText = File.ReadAllText(JsonPath);
        var root = JObject.Parse(jsonText);

        // 最初にテーブルコレクション取得 or 作成
        var collection = LocalizationEditorSettings.GetStringTableCollection(TableCollectionName);
        if (collection == null)
        {
            collection = LocalizationEditorSettings.CreateStringTableCollection(TableCollectionName, "Assets/Localization/Tables");
        }

        // 各ロケールごとにテーブルを生成
        foreach (var localeCode in new[] { "en", "ja", "zh-Hans" })
        {
            var table = collection.GetTable(localeCode) as StringTable;
            if (table == null)
            {
                table = collection.AddNewTable(localeCode) as StringTable;
            }

            foreach (var pair in root)
            {
                string key = pair.Key;
                var translations = pair.Value as JObject;

                if (translations != null && translations.TryGetValue(localeCode, out var valueToken))
                {
                    string value = valueToken.ToString();
                    table.AddEntry(key, value);
                    Debug.Log($"[{localeCode}] {key} = {value}");
                }
            }

            EditorUtility.SetDirty(table);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("✅ JSON Localization Import Complete!");
    }
}
