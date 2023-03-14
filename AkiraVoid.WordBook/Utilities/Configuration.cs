using System.Configuration;

namespace AkiraVoid.WordBook.Utilities;

/// <summary>
/// 提供一系列简化配置操作的方法。
/// </summary>
public static class Configuration
{
    /// <summary>
    /// 获取配置项。
    /// </summary>
    /// <param name="key">指向配置项的键。</param>
    /// <returns>返回获取的配置项。配置项不存在则返回 <see langword="null"/>。</returns>
    public static string GetConfiguration(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    /// <summary>
    /// 设置某项配置项。该方法使用 UPSERT 语义。
    /// </summary>
    /// <param name="key">配置项的键。</param>
    /// <param name="value">配置项的值。</param>
    /// <remarks>
    /// <b>UPSERT 语义</b>通常用于数据库操作，指代存在该实体则更新之（UPDATE），否则插入新的实体（INSERT）。
    /// </remarks>
    public static void SetConfiguration(string key, string value)
    {
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        var settings = config.AppSettings.Settings;
        if (settings[key] == null)
        {
            settings.Add(key, value);
        }
        else
        {
            settings[key].Value = value;
        }

        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.SectionName);
    }
}