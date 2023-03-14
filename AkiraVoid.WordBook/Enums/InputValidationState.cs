namespace AkiraVoid.WordBook.Enums;

/// <summary>
/// 指示输入验证器的验证状态。
/// </summary>
public enum InputValidationState
{
    /// <summary>
    /// 输入验证检测到错误。
    /// </summary>
    Error,

    /// <summary>
    /// 输入验证已通过。
    /// </summary>
    Passed,

    /// <summary>
    /// 输入验证还未开始。
    /// </summary>
    Unvalidated
}