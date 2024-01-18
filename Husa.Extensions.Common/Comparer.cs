namespace Husa.Extensions.Common
{
    using System;
    using System.Linq;
    using Husa.Extensions.Common.Enums;

    public static class Comparer
    {
        public static bool CompareValues(string dependentValue, string targetValue, OperatorType conditionOperator, ComparedValueType? valueType) => valueType switch
        {
            null => CheckValue(dependentValue, conditionOperator),
            ComparedValueType.DateTime => CompareDates(dependentValue, targetValue, conditionOperator),
            ComparedValueType.Numeric => CompareNumbers(dependentValue, targetValue, conditionOperator),
            ComparedValueType.String => CompareStrings(dependentValue, targetValue, conditionOperator),
            ComparedValueType.DateTimeArray => CompareDatesArray(dependentValue, targetValue, conditionOperator),
            ComparedValueType.StringArray => CompareStringArray(dependentValue, targetValue, conditionOperator),
            ComparedValueType.NumericArray => CompareNumericArray(dependentValue, targetValue, conditionOperator),
            _ => false,
        };

        public static ComparedValueType? TypeOfValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (value.Contains(','))
            {
                if (DateTime.TryParse(value.Split(',')[0], out _))
                {
                    return ComparedValueType.DateTimeArray;
                }

                if (decimal.TryParse(value.Split(',')[0], out _))
                {
                    return ComparedValueType.NumericArray;
                }

                return ComparedValueType.StringArray;
            }

            if (DateTime.TryParse(value, out _))
            {
                return ComparedValueType.DateTime;
            }

            if (decimal.TryParse(value, out _))
            {
                return ComparedValueType.Numeric;
            }

            return ComparedValueType.String;
        }

        private static bool CheckValue(string value, OperatorType conditionOperator) => conditionOperator switch
        {
            OperatorType.Empty => string.IsNullOrEmpty(value),
            OperatorType.NotEmpty => !string.IsNullOrEmpty(value),
            OperatorType.Null => value is null,
            OperatorType.NotNull => value is not null,
            _ => false,
        };

        private static bool CompareDates(string mainDate, string dateToCompare, OperatorType conditionOperator)
        {
            if (string.IsNullOrEmpty(mainDate))
            {
                return false;
            }

            DateTime leftDate = DateTime.Parse(mainDate);
            DateTime rightDate = DateTime.Parse(dateToCompare);
            return conditionOperator switch
            {
                OperatorType.Equal => leftDate == rightDate,
                OperatorType.NotEqual => leftDate != rightDate,
                OperatorType.GreaterThan => leftDate > rightDate,
                OperatorType.LessThan => leftDate < rightDate,
                OperatorType.LessEqual => leftDate <= rightDate,
                _ => false,
            };
        }

        private static bool CompareNumbers(string number1, string number2, OperatorType conditionOperator)
        {
            if (string.IsNullOrEmpty(number1) || string.IsNullOrEmpty(number2))
            {
                return false;
            }

            var dec1 = decimal.Parse(number1);
            var dec2 = decimal.Parse(number2);

            return conditionOperator switch
            {
                OperatorType.Equal => dec1 == dec2,
                OperatorType.NotEqual => dec1 != dec2,
                OperatorType.GreaterThan => dec1 > dec2,
                OperatorType.GreaterEqual => dec1 >= dec2,
                OperatorType.LessThan => dec1 < dec2,
                OperatorType.LessEqual => dec1 <= dec2,
                _ => false,
            };
        }

        private static bool CompareStrings(string value1, string value2, OperatorType conditionOperator)
        {
            value1 ??= string.Empty;
            value2 ??= string.Empty;

            return conditionOperator switch
            {
                OperatorType.Equal => value1.ToLower() == value2.ToLower(),
                OperatorType.NotEqual => value1.ToLower() != value2.ToLower(),
                OperatorType.Contains => value1.ToLower().Contains(value2.ToLower()),
                OperatorType.NotContains => !value1.ToLower().Contains(value2.ToLower()),
                OperatorType.StartsWith => value1.ToLower().StartsWith(value2.ToLower()),
                OperatorType.NotStartsWith => !value1.ToLower().StartsWith(value2.ToLower()),
                OperatorType.EndsWith => value1.ToLower().EndsWith(value2.ToLower()),
                OperatorType.NotEndsWith => !value1.ToLower().EndsWith(value2.ToLower()),
                _ => false,
            };
        }

        private static bool CompareDatesArray(string value1, string value2, OperatorType conditionOperator)
        {
            var validValue = false;
            if (string.IsNullOrEmpty(value1))
            {
                return validValue;
            }

            var dt1 = DateTime.Parse(value1);
            var dts = DateTime.Parse(value2.Split(',')[0]);
            var dte = DateTime.Parse(value2.Split(',')[1]);

            return conditionOperator switch
            {
                OperatorType.Between => dt1 >= dts && dt1 <= dte,
                OperatorType.NotBetween => dt1 < dts && dt1 > dte,
                OperatorType.In => ValidatesTypeInArray(value2, dt1),
                OperatorType.NotIn => !ValidatesTypeInArray(value2, dt1),
                _ => false,
            };
        }

        private static bool CompareNumericArray(string value1, string value2, OperatorType conditionOperator)
        {
            var d1 = string.IsNullOrEmpty(value1) ? 0 : decimal.Parse(value1);
            var ds = string.IsNullOrEmpty(value2) ? 0 : decimal.Parse(value2.Split(',')[0]);
            var de = string.IsNullOrEmpty(value2) ? 0 : decimal.Parse(value2.Split(',')[1]);

            return conditionOperator switch
            {
                OperatorType.Between => d1 >= ds && d1 <= de,
                OperatorType.In => ValidatesTypeInArray(value2, d1),
                OperatorType.NotIn => !ValidatesTypeInArray(value2, d1),
                _ => false,
            };
        }

        private static bool CompareStringArray(string value1, string value2, OperatorType conditionOperator)
        {
            var validValue = false;
            value1 ??= string.Empty;
            value2 ??= string.Empty;

            if (string.IsNullOrEmpty(value1) || value1.Split(',').Length == 0)
            {
                return validValue;
            }

            var val1Array = value1.Split(',');
            var val2Array = value2.Split(',');

            validValue = conditionOperator switch
            {
                OperatorType.Equal => val1Array.Distinct().Count() == val2Array.Distinct().Count() &&
                            (from s1 in val1Array
                             join s2 in val2Array on s1 equals s2
                             select s1).Distinct().Count() == val1Array.Distinct().Count(),
                OperatorType.NotEqual => !(val1Array.Distinct().Count() == val2Array.Distinct().Count() &&
                                        (from s1 in val1Array
                                         join s2 in val2Array on s1 equals s2
                                         select s1).Distinct().Count() == val1Array.Distinct().Count()),
                OperatorType.In => (from s2 in val2Array
                                    join s1 in val1Array on s2 equals s1
                                    select s1).Any(),
                OperatorType.NotIn => !(from s2 in val2Array
                                        join s1 in val1Array on s2 equals s1
                                        select s1).Any(),
                _ => false,
            };

            return validValue;
        }

        private static bool ValidatesTypeInArray(string value2, DateTime dt1)
        {
            bool validValue;
            var dtss = value2.Split(',');
            var dtarray = Array.Empty<DateTime>();
            foreach (var s in dtss)
            {
                Array.Resize(ref dtarray, dtarray.Length + 1);
                dtarray[^1] = DateTime.Parse(s);
            }

            validValue = (from d in dtarray where d == dt1 select d).Any();
            return validValue;
        }

        private static bool ValidatesTypeInArray(string commaSeparatedValues, decimal decimalValue)
        {
            if (string.IsNullOrWhiteSpace(commaSeparatedValues))
            {
                return false;
            }

            var ndarray = Array.Empty<decimal>();
            foreach (string decimalPosition in commaSeparatedValues.Split(','))
            {
                Array.Resize(ref ndarray, ndarray.Length + 1);
                ndarray[^1] = decimal.Parse(decimalPosition);
            }

            return (from decimalItem in ndarray
                    where decimalItem == decimalValue
                    select decimalItem).Any();
        }
    }
}
