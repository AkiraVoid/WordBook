using AkiraVoid.WordBook.Models;

namespace AkiraVoid.WordBook.ViewModels;

public class ExplanationEditorEditedEventArgs
{
    /// <summary>
    /// 被编辑的属性名称。
    /// </summary>
    public string EditedProperty { get; set; }

    /// <summary>
    /// 被编辑的解释。
    /// </summary>
    public WordExplanation Explanation { get; set; }

    /// <summary>
    /// 初始化一个 <see cref="ExplanationEditorEditedEventArgs"/> 实例。
    /// </summary>
    /// <param name="explanation">被编辑的属性名称。</param>
    /// <param name="editedProperty">被编辑的解释。</param>
    public ExplanationEditorEditedEventArgs(WordExplanation explanation, string editedProperty = null)
    {
        EditedProperty = editedProperty;
        Explanation = explanation;
    }
}