using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;
using AkiraVoid.WordBook.Enums;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.ViewModels;

namespace AkiraVoid.WordBook.Utilities;

/// <summary>
/// 提供与听写有关的一系列方法。
/// </summary>
public static class Teachers
{
    /// <summary>
    /// 合成语音并播放。
    /// </summary>
    /// <param name="spell">单词的拼写。</param>
    /// <param name="language">单词的语言。</param>
    /// <param name="player">媒体播放器。</param>
    /// <returns>异步任务。</returns>
    private static async Task SynthesizeAndPlayAsync(string spell, WordLanguage language, MediaPlayer player)
    {
        var teacher = new SpeechSynthesizer();
        SetVoice(language, ref teacher);
        var source = await teacher.SynthesizeTextToStreamAsync(spell);
        player.SetStreamSource(source);
        teacher.Dispose();
        player.Play();
    }

    /// <summary>
    /// 获取一次性使用的 <see cref="MediaPlayer"/> 实例。
    /// </summary>
    /// <returns>一次性使用的 <see cref="MediaPlayer"/> 实例。</returns>
    private static MediaPlayer GetOneTimeMediaPlayerPlayer()
    {
        var mediaPlayer = new MediaPlayer();
        mediaPlayer.MediaEnded += (player, _) => player.Dispose();
        return mediaPlayer;
    }

    /// <summary>
    /// 设置语音合成器使用的人声。
    /// </summary>
    /// <param name="language">人声语言。</param>
    /// <param name="synthesizer">语音合成器。</param>
    private static void SetVoice(WordLanguage language, ref SpeechSynthesizer synthesizer)
    {
        var teacher = GetConfiguredTeacher(language)?.ToVoiceInformation();

        synthesizer.Voice = teacher;
    }

    /// <summary>
    /// 播放合成语言。
    /// </summary>
    /// <param name="word">要合成语音的单词。</param>
    /// <returns>异步任务。</returns>
    public static async Task SpeakAsync(Word word)
    {
        await SynthesizeAndPlayAsync(word.Spell, word.Language, GetOneTimeMediaPlayerPlayer());
    }

    /// <summary>
    /// 播放合成语言。
    /// </summary>
    /// <param name="text">要合成语音的文本。</param>
    /// <param name="language">文本语言。</param>
    /// <returns>异步任务。</returns>
    public static async Task SpeakAsync(string text, WordLanguage language)
    {
        await SynthesizeAndPlayAsync(text, language, GetOneTimeMediaPlayerPlayer());
    }

    /// <summary>
    /// 设置对应语言的讲述人，并将该设置保存至配置项。
    /// </summary>
    /// <param name="language">讲述人语言。</param>
    /// <param name="teacher">讲述人。</param>
    public static void SetTeacher(WordLanguage language, Teacher teacher)
    {
        Configuration.SetConfiguration($"{language.ToString().ToLowerInvariant()}TeacherId", teacher.Id);
    }

    /// <summary>
    /// 获取所有可用讲述人。
    /// </summary>
    /// <returns>所有可用讲述人。</returns>
    public static IEnumerable<Teacher> GetTeachers()
    {
        return SpeechSynthesizer.AllVoices.Map((v) => new Teacher(v));
    }

    /// <summary>
    /// 获取所有指定语言下的可用讲述人。
    /// </summary>
    /// <param name="language">讲述人语言，使用标准化 BCP-47 语言代码表示。</param>
    /// <returns>所有指定语言下的可用讲述人。</returns>
    public static List<Teacher> GetTeachers(WordLanguage language)
    {
        var languageCode = language == WordLanguage.English ? "en-US" : "ja-JP";
        return SpeechSynthesizer.AllVoices.Where(v => v.Language == languageCode).Map(v => new Teacher(v)).ToList();
    }

    /// <summary>
    /// 获取指定语言的保存在配置项中的讲述人。
    /// </summary>
    /// <param name="language">讲述人语言。</param>
    /// <returns>指定语言的保存在配置项中的讲述人。</returns>
    public static Teacher GetConfiguredTeacher(WordLanguage language)
    {
        var defaultTeacher = language == WordLanguage.English
            ? SpeechSynthesizer.AllVoices.FirstOrDefault(v => v.Language == "en-US")
            : SpeechSynthesizer.AllVoices.FirstOrDefault(v => v.Language == "ja-JP");
        if (defaultTeacher == null)
        {
            defaultTeacher = SpeechSynthesizer.DefaultVoice;
        }

        var configuredTeacherId = Configuration.GetConfiguration($"{language.ToString().ToLowerInvariant()}TeacherId");
        if (string.IsNullOrEmpty(configuredTeacherId) || configuredTeacherId == "null")
        {
            return new(defaultTeacher);
        }

        return new(SpeechSynthesizer.AllVoices.FirstOrDefault(v => v.Id == configuredTeacherId) ?? defaultTeacher);
    }

    /// <summary>
    /// 根据讲述人 ID 获取讲述人。
    /// </summary>
    /// <param name="id">讲述人 ID。</param>
    /// <returns>对应的讲述人。</returns>
    public static Teacher GetTeacher(string id)
    {
        return GetTeachers().FirstOrDefault(v => v.Id == id);
    }
}