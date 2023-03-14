using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AkiraVoid.WordBook.Enums;
using AkiraVoid.WordBook.Utilities;

namespace AkiraVoid.WordBook.Models;

[Table("parts_of_speech")]
public class PartOfSpeech
{
    [Key] [Column("id")] public Guid Id { get; set; }
    [Column("name")] public PartOfSpeechName Name { get; set; }
    [Column("display_name")] public string DisplayName { get; set; }
    [Column("abbreviation")] public string Abbreviation { get; set; }
    public IList<WordExplanation> Explanations { get; set; }

    /// <summary>
    /// 检查两个 <see cref="PartOfSpeech"/> 实例是否相等。
    /// </summary>
    /// <param name="partOfSpeech"></param>
    /// <returns>相等则返回 <see langword="true"/>，否则返回 <see langword="false"/>。</returns>
    public bool Equals(PartOfSpeech partOfSpeech)
    {
        return Name == partOfSpeech.Name;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return DisplayName.Capitalize();
    }
}