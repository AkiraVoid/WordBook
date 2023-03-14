using System.Linq;
using Windows.Media.SpeechSynthesis;

namespace AkiraVoid.WordBook.ViewModels;

public class Teacher
{
    public string Id { get; set; }
    public string Name { get; set; }

    public Teacher()
    {
    }

    public Teacher(VoiceInformation voiceInformation)
    {
        Id = voiceInformation.Id;
        Name = voiceInformation.DisplayName;
    }

    public VoiceInformation ToVoiceInformation()
    {
        return SpeechSynthesizer.AllVoices.FirstOrDefault(v => v.Id == Id);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Name;
    }

    /// <inheritdoc cref="object.Equals" />
    public bool Equals(Teacher teacher)
    {
        return Id == teacher?.Id;
    }
}