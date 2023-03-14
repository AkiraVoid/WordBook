using System;
using System.Collections.Generic;
using System.IO;
using AkiraVoid.WordBook.Enums;
using Microsoft.EntityFrameworkCore;

namespace AkiraVoid.WordBook.Models;

public class WordBankContext : DbContext
{
    public DbSet<Word> Words { get; set; }
    public DbSet<PartOfSpeech> PartsOfSpeech { get; set; }
    public DbSet<WordExplanation> WordExplanations { get; set; }

    public WordBankContext()
    {
    }

    public WordBankContext(DbContextOptions<WordBankContext> options) : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = $"Data Source={Path.Combine(Environment.CurrentDirectory, "WordBank.db")}";
            optionsBuilder.UseSqlite(connectionString);

            // 开发环境可以通过取消下方注释来查看 EntityFramework Core 的日志输出。

            //optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message))
            //    .EnableDetailedErrors()
            //    .EnableSensitiveDataLogging();
        }

        base.OnConfiguring(optionsBuilder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Word>()
            .HasMany(entity => entity.Explanations)
            .WithOne(entity => entity.Word)
            .HasForeignKey(entity => entity.WordId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("word_to_explanations");
        modelBuilder.Entity<Word>().Property(entity => entity.HasMemorized).HasDefaultValue(false);
        modelBuilder.Entity<Word>().Property(entity => entity.IsImportant).HasDefaultValue(false);

        modelBuilder.Entity<PartOfSpeech>()
            .HasMany(entity => entity.Explanations)
            .WithOne(entity => entity.PartOfSpeech)
            .HasForeignKey(entity => entity.PartOfSpeechId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("part_of_speech_to_explanations");
        modelBuilder.Entity<PartOfSpeech>().Property(entity => entity.Id).ValueGeneratedOnAdd();
        SeedPartsOfSpeech(modelBuilder);

        modelBuilder.Entity<WordExplanation>().Property(entity => entity.Id).ValueGeneratedOnAdd();
    }

    /// <summary>
    /// 向数据库中插入词性数据。
    /// </summary>
    /// <param name="modelBuilder"></param>
    private void SeedPartsOfSpeech(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PartOfSpeech>()
            .HasData(
                new List<PartOfSpeech>
                {
                    new()
                    {
                        Name = PartOfSpeechName.Noun,
                        DisplayName = "noun",
                        Abbreviation = "n.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Pronoun,
                        DisplayName = "pronoun",
                        Abbreviation = "pron.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Adjective,
                        DisplayName = "adjective",
                        Abbreviation = "adj.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Adverb,
                        DisplayName = "adverb",
                        Abbreviation = "adv.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Verb,
                        DisplayName = "verb",
                        Abbreviation = "v.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Numeral,
                        DisplayName = "numeral",
                        Abbreviation = "num.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Article,
                        DisplayName = "article",
                        Abbreviation = "art.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Preposition,
                        DisplayName = "preposition",
                        Abbreviation = "prep.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Conjunction,
                        DisplayName = "conjunction",
                        Abbreviation = "conj.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Interjection,
                        DisplayName = "interjection",
                        Abbreviation = "interj.",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Meishi,
                        DisplayName = "名詞",
                        Abbreviation = "名",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Daimeishi,
                        DisplayName = "代名詞",
                        Abbreviation = "代",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Suushi,
                        DisplayName = "数詞",
                        Abbreviation = "数",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Doushi,
                        DisplayName = "動詞",
                        Abbreviation = "動",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Keiyoushi,
                        DisplayName = "形容詞",
                        Abbreviation = "形",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Keiyoudoushi,
                        DisplayName = "形容動詞",
                        Abbreviation = "形動",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Fukushi,
                        DisplayName = "副詞",
                        Abbreviation = "副",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Rentaishi,
                        DisplayName = "連体詞",
                        Abbreviation = "連体",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Setsuzokushi,
                        DisplayName = "接続詞",
                        Abbreviation = "接続",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Kandoushi,
                        DisplayName = "感動詞",
                        Abbreviation = "感動",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Jodoushi,
                        DisplayName = "助動詞",
                        Abbreviation = "助動",
                        Id = Guid.NewGuid()
                    },
                    new()
                    {
                        Name = PartOfSpeechName.Joshi,
                        DisplayName = "助詞",
                        Abbreviation = "助",
                        Id = Guid.NewGuid()
                    },
                });
    }
}