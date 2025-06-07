using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    private bool isInitialized = false;
    private List<Locale> availableLocales;

    private void Awake()
    {
        // シングルトン化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(InitializeLocalization());
    }

    private System.Collections.IEnumerator InitializeLocalization()
    {
        yield return LocalizationSettings.InitializationOperation;

        availableLocales = new List<Locale>(LocalizationSettings.AvailableLocales.Locales);
        isInitialized = true;

        Debug.Log($"初期言語: {GetCurrentLanguageCode()}");

        // debug
        ChangeLanguage("ja");
    }

    /// <summary>
    /// 使用可能な言語コード一覧を取得（例: "en", "ja", "zh-Hans"）
    /// </summary>
    public List<string> GetAvailableLanguageCodes()
    {
        var codes = new List<string>();
        foreach (var locale in availableLocales)
        {
            codes.Add(locale.Identifier.Code);
        }
        return codes;
    }

    /// <summary>
    /// 現在の言語コードを取得（例: "ja"）
    /// </summary>
    public string GetCurrentLanguageCode()
    {
        return LocalizationSettings.SelectedLocale.Identifier.Code;
    }

    /// <summary>
    /// 言語をコードで切り替える（例: "en", "ja", "zh-Hans"）
    /// </summary>
    public void ChangeLanguage(string languageCode)
    {
        if (!isInitialized)
        {
            Debug.LogWarning("Localization 未初期化");
            return;
        }

        foreach (var locale in availableLocales)
        {
            if (locale.Identifier.Code == languageCode)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log($"言語切替: {languageCode}");
                return;
            }
        }

        Debug.LogWarning($"指定された言語が見つかりません: {languageCode}");
    }

    /// <summary>
    /// 指定したテーブル名とキーから翻訳済みの文字列を取得
    /// </summary>
    /// <param name="tableName">String Table の名前</param>
    /// <param name="key">翻訳キー</param>
    /// <returns>翻訳済みテキスト。見つからない場合は "#key"</returns>
    public string GetLocalizedText(string key)
    {
        var tableName = "Words";
        var table = LocalizationSettings.StringDatabase.GetTable(tableName);
        if (table == null)
        {
            Debug.LogWarning($"String Table '{tableName}' が見つかりません");
            return $"#{key}";
        }

        var entry = table.GetEntry(key);
        if (entry == null)
        {
            Debug.LogWarning($"キー '{key}' がテーブル '{tableName}' に見つかりません");
            return $"#{key}";
        }

        return entry.GetLocalizedString();
    }
}
