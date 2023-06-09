﻿// <auto-generated />
using System;
using AkiraVoid.WordBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AkiraVoid.WordBook.Migrations
{
    [DbContext(typeof(WordBankContext))]
    [Migration("20230310064651_WordBank")]
    partial class WordBank
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("AkiraVoid.WordBook.Models.PartOfSpeech", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("TEXT")
                        .HasColumnName("abbreviation");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT")
                        .HasColumnName("display_name");

                    b.Property<int>("Name")
                        .HasColumnType("INTEGER")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("parts_of_speech");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5d46507e-65ac-44d7-987e-ddd9b06a8b38"),
                            Abbreviation = "n.",
                            DisplayName = "noun",
                            Name = 0
                        },
                        new
                        {
                            Id = new Guid("34cc4f49-c92a-453a-aebe-e3917afb806a"),
                            Abbreviation = "pron.",
                            DisplayName = "pronoun",
                            Name = 1
                        },
                        new
                        {
                            Id = new Guid("43b5f7f3-860f-408c-9614-a5ba26d6a18b"),
                            Abbreviation = "adj.",
                            DisplayName = "adjective",
                            Name = 2
                        },
                        new
                        {
                            Id = new Guid("67959c5c-ae7f-40b7-8953-5382852476e4"),
                            Abbreviation = "adv.",
                            DisplayName = "adverb",
                            Name = 3
                        },
                        new
                        {
                            Id = new Guid("634b15d4-3934-4977-ae86-f14ac0307ae3"),
                            Abbreviation = "v.",
                            DisplayName = "verb",
                            Name = 4
                        },
                        new
                        {
                            Id = new Guid("fb9c654e-1e02-49ca-9378-b6dd37cc043a"),
                            Abbreviation = "num.",
                            DisplayName = "numeral",
                            Name = 5
                        },
                        new
                        {
                            Id = new Guid("7f433147-fa2b-408a-be15-835cf1b7aa2f"),
                            Abbreviation = "art.",
                            DisplayName = "article",
                            Name = 6
                        },
                        new
                        {
                            Id = new Guid("9a95aa8d-0089-4a83-a255-52028645fbde"),
                            Abbreviation = "prep.",
                            DisplayName = "preposition",
                            Name = 7
                        },
                        new
                        {
                            Id = new Guid("2254bace-a80c-41b6-9f68-8e6519c3458b"),
                            Abbreviation = "conj.",
                            DisplayName = "conjunction",
                            Name = 8
                        },
                        new
                        {
                            Id = new Guid("b71ab6c6-79d2-429b-bbe0-30336aa27219"),
                            Abbreviation = "interj.",
                            DisplayName = "interjection",
                            Name = 9
                        },
                        new
                        {
                            Id = new Guid("2235054b-f47d-4156-ad20-1b14a4114292"),
                            Abbreviation = "名",
                            DisplayName = "名詞",
                            Name = 10
                        },
                        new
                        {
                            Id = new Guid("9c61aff0-79a6-49e1-ba7c-e7432c6ae604"),
                            Abbreviation = "代",
                            DisplayName = "代名詞",
                            Name = 11
                        },
                        new
                        {
                            Id = new Guid("7513069d-13f5-4b1a-bac4-792330a225c0"),
                            Abbreviation = "数",
                            DisplayName = "数詞",
                            Name = 12
                        },
                        new
                        {
                            Id = new Guid("ad2b60da-cc90-40f3-b672-085552fe16c6"),
                            Abbreviation = "動",
                            DisplayName = "動詞",
                            Name = 13
                        },
                        new
                        {
                            Id = new Guid("f361aa7f-dd59-4a68-9c7a-6a67c8c702e1"),
                            Abbreviation = "形",
                            DisplayName = "形容詞",
                            Name = 14
                        },
                        new
                        {
                            Id = new Guid("5d127d36-9841-4198-b435-44e976ca6d6a"),
                            Abbreviation = "形動",
                            DisplayName = "形容動詞",
                            Name = 15
                        },
                        new
                        {
                            Id = new Guid("49f06470-0b2a-46dc-8b8d-63ae2a036634"),
                            Abbreviation = "副",
                            DisplayName = "副詞",
                            Name = 16
                        },
                        new
                        {
                            Id = new Guid("78753e63-283d-4419-87d9-a159795c8950"),
                            Abbreviation = "連体",
                            DisplayName = "連体詞",
                            Name = 17
                        },
                        new
                        {
                            Id = new Guid("b4f48c14-ddac-4baa-bb24-fdcec7bdb76f"),
                            Abbreviation = "接続",
                            DisplayName = "接続詞",
                            Name = 18
                        },
                        new
                        {
                            Id = new Guid("6f438479-edd2-46ef-b40e-10086f8cb638"),
                            Abbreviation = "感動",
                            DisplayName = "感動詞",
                            Name = 19
                        },
                        new
                        {
                            Id = new Guid("1315f153-1596-4622-bdba-434b239c63b9"),
                            Abbreviation = "助動",
                            DisplayName = "助動詞",
                            Name = 20
                        },
                        new
                        {
                            Id = new Guid("c6360920-2930-4db2-84da-024ca152a4c0"),
                            Abbreviation = "助",
                            DisplayName = "助詞",
                            Name = 21
                        });
                });

            modelBuilder.Entity("AkiraVoid.WordBook.Models.Word", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("added_at");

                    b.Property<bool>("HasMemorized")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("has_memorized");

                    b.Property<bool>("IsImportant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("is_important");

                    b.Property<int>("Language")
                        .HasColumnType("INTEGER")
                        .HasColumnName("language");

                    b.Property<DateTime>("LastMemorizedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_memorized_at");

                    b.Property<int>("MemorizationTimes")
                        .HasColumnType("INTEGER")
                        .HasColumnName("memorization_times");

                    b.Property<string>("Pronunciation")
                        .HasColumnType("TEXT")
                        .HasColumnName("Pronunciation");

                    b.Property<string>("Spell")
                        .HasColumnType("TEXT")
                        .HasColumnName("spell");

                    b.HasKey("Id");

                    b.ToTable("words");
                });

            modelBuilder.Entity("AkiraVoid.WordBook.Models.WordExplanation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Explanation")
                        .HasColumnType("TEXT")
                        .HasColumnName("explanation");

                    b.Property<Guid>("PartOfSpeechId")
                        .HasColumnType("TEXT")
                        .HasColumnName("part_of_speech_id");

                    b.Property<string>("Translation")
                        .HasColumnType("TEXT")
                        .HasColumnName("translation");

                    b.Property<Guid>("WordId")
                        .HasColumnType("TEXT")
                        .HasColumnName("word_id");

                    b.HasKey("Id");

                    b.HasIndex("PartOfSpeechId");

                    b.HasIndex("WordId");

                    b.ToTable("explanations");
                });

            modelBuilder.Entity("AkiraVoid.WordBook.Models.WordExplanation", b =>
                {
                    b.HasOne("AkiraVoid.WordBook.Models.PartOfSpeech", "PartOfSpeech")
                        .WithMany("Explanations")
                        .HasForeignKey("PartOfSpeechId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("part_of_speech_to_explanations");

                    b.HasOne("AkiraVoid.WordBook.Models.Word", "Word")
                        .WithMany("Explanations")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("word_to_explanations");

                    b.Navigation("PartOfSpeech");

                    b.Navigation("Word");
                });

            modelBuilder.Entity("AkiraVoid.WordBook.Models.PartOfSpeech", b =>
                {
                    b.Navigation("Explanations");
                });

            modelBuilder.Entity("AkiraVoid.WordBook.Models.Word", b =>
                {
                    b.Navigation("Explanations");
                });
#pragma warning restore 612, 618
        }
    }
}
