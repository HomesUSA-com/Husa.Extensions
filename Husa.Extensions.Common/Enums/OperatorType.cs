namespace Husa.Extensions.Common.Enums
{
    using System.Runtime.Serialization;

    public enum OperatorType
    {
        [EnumMember(Value = "=")]
        Equal = 0,
        [EnumMember(Value = "!=")]
        NotEqual = 1,
        [EnumMember(Value = ">")]
        GreaterThan = 2,
        [EnumMember(Value = ">=")]
        GreaterEqual = 3,
        [EnumMember(Value = "<")]
        LessThan = 4,
        [EnumMember(Value = "<=")]
        LessEqual = 5,
        [EnumMember(Value = "><")]
        Between = 6,
        [EnumMember(Value = "!><")]
        NotBetween = 7,
        [EnumMember(Value = "null")]
        Null = 8,
        [EnumMember(Value = "!null")]
        NotNull = 9,
        [EnumMember(Value = "_")]
        Empty = 10,
        [EnumMember(Value = "!_")]
        NotEmpty = 11,
        [EnumMember(Value = "startwith")]
        StartsWith = 12,
        [EnumMember(Value = "!startwith")]
        NotStartsWith = 13,
        [EnumMember(Value = "endwith")]
        EndsWith = 14,
        [EnumMember(Value = "!endwith")]
        NotEndsWith = 15,
        [EnumMember(Value = "in")]
        In = 16,
        [EnumMember(Value = "!in")]
        NotIn = 17,
        [EnumMember(Value = "Contains")]
        Contains = 18,
        [EnumMember(Value = "!Contains")]
        NotContains = 19,
    }
}
