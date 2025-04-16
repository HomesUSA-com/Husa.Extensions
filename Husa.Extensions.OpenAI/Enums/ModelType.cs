namespace Husa.Extensions.OpenAI.Enums;

using System.Runtime.Serialization;

public enum ModelType
{
    [EnumMember(Value = "gpt-4")]
    Gpt4,
    [EnumMember(Value = "gpt-4o")]
    Gpt4o,
    [EnumMember(Value = "gpt-4o-mini")]
    Gpt4oMini,
    [EnumMember(Value = "gpt-3.5-turbo")]
    Gpt3Turbo,
}
