using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkSFrancis.Reflection;
using MarkSFrancis.Extensions;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public class NameMap<T> : BaseMap<NameMap<T>, T, string> where T : new()
    {
        public NameMap(bool caseSensitiveMap = false, bool spaceSensitiveMap = false)
        {
            ColumnNameToColumnIndex = new Dictionary<string, int>();
            CaseSensitiveMap = caseSensitiveMap;
            SpaceSensitiveMap = spaceSensitiveMap;
        }

        public static NameMap<T> FromProperties(bool caseSensitiveMap = false, bool spaceSensitiveMap = false)
        {
            return new NameMap<T>(caseSensitiveMap, spaceSensitiveMap)
                .AutoMapProperties();
        }

        public static NameMap<T> FromFields(bool caseSensitiveMap = false, bool spaceSensitiveMap = false)
        {
            return new NameMap<T>(caseSensitiveMap, spaceSensitiveMap)
                .AutoMapFields();
        }

        public static NameMap<T> FromPropertiesAndFields(bool caseSensitiveMap = false, bool spaceSensitiveMap = false)
        {
            return new NameMap<T>(caseSensitiveMap, spaceSensitiveMap)
                .AutoMapProperties()
                .AutoMapFields();
        }

        // Read properties
        public bool CaseSensitiveMap { get; }
        public bool SpaceSensitiveMap { get; }

        // Write properties
        private Dictionary<string, int> ColumnNameToColumnIndex { get; }

        public NameMap<T> AutoMapProperties()
        {
            foreach (var property in TProperties)
            {
                var prettyName = NiceFormatCamelCase(property.Name);
                Map(property, prettyName);
            }

            return this;
        }

        public NameMap<T> AutoMapFields()
        {
            foreach (var field in TFields)
            {
                var prettyName = NiceFormatCamelCase(field.Name);
                Map(field, prettyName);
            }

            return this;
        }

        public override IEnumerable<string> GetColumnHeadings()
        {
            var mappedColumns = ColumnNameToColumnIndex.OrderBy(cn => cn.Value).ToList();

            int columnIndex = 0;
            foreach (var mappedColumn in mappedColumns)
            {
                while (mappedColumn.Value > columnIndex)
                {
                    yield return "";
                    columnIndex++;
                }

                yield return mappedColumn.Key;
                columnIndex++;
            }
        }

        public override NameMap<T> Map(PropertyFieldInfo<T, object> propFieldInfo, string columnName)
        {
            Unmap(columnName);

            var colName = CleanColumnName(columnName);

            KeyToPropertyField.Add(colName, propFieldInfo);
            PropertyFieldToKey.Add(propFieldInfo.Name, columnName);
            ColumnNameToColumnIndex.Add(columnName, TotalColumns());

            return this;
        }

        public NameMap<T> Unmap(string columnName)
        {
            var colName = CleanColumnName(columnName);

            if (!ColumnNameToColumnIndex.ContainsKey(colName))
            {
                // Not found
                return this;
            }

            var propFieldInfo = KeyToPropertyField[colName];
            KeyToPropertyField.Remove(colName);
            ColumnNameToColumnIndex.Remove(colName);
            PropertyFieldToKey.Remove(propFieldInfo.Name);

            return this;
        }

        private string CleanColumnName(string name)
        {
            if (!SpaceSensitiveMap)
            {
                // ReSharper disable once PossiblyMistakenUseOfParamsMethod
                name = name.Remove(" ");
            }

            if (!CaseSensitiveMap)
            {
                name = name.ToUpperInvariant();
            }

            return name;
        }

        /// <summary>
        /// Adds spaces between each word in a camel case variable name
        /// </summary>
        /// <param name="camelCase">The camel case text to prettify</param>
        /// <returns></returns>
        private string NiceFormatCamelCase(string camelCase)
        {
            if (string.IsNullOrWhiteSpace(camelCase))
            {
                return camelCase;
            }

            camelCase = camelCase.Trim();
            StringBuilder niceFormat = new StringBuilder();

            bool lastCharWasUpperNumberOrSpace = char.IsUpper(camelCase[0]) || char.IsNumber(camelCase[0]);
            niceFormat.Append(camelCase[0]);

            for (int charIndex = 1; charIndex < camelCase.Length; charIndex++)
            {
                var curCharWasUpperNumberOrSpace =
                    char.IsUpper(camelCase[charIndex]) ||
                    char.IsNumber(camelCase[charIndex]);

                if (curCharWasUpperNumberOrSpace && !lastCharWasUpperNumberOrSpace)
                {
                    niceFormat.Append(" ");
                }
                niceFormat.Append(camelCase[charIndex]);
            }

            return niceFormat.ToString();
        }

        public T MapToObject(IList<string> values, IList<string> columnHeadings)
        {
            T newT = new T();

            for (int columnId = 0; columnId < values.Count; columnId++)
            {
                TryAssignToMember(columnHeadings[columnId], values[columnId], newT);
            }

            return newT;
        }

        protected override bool TryAppendSpecificValue(T record, PropertyFieldInfo<T, object> propFieldInfo, IList<string> values)
        {
            if (!TryGetMapping(propFieldInfo, out string columnName))
            {
                return false;
            }

            var index = ColumnNameToColumnIndex[columnName];

            values[index] = GetFromMember(record, propFieldInfo);

            return true;
        }

        protected override bool TryGetMapping(PropertyFieldInfo<T, object> propField, out string columnName)
        {
            return PropertyFieldToKey.TryGetValue(propField.Name, out columnName);
        }

        protected override bool TryGetMapping(string columnName, out PropertyFieldInfo<T, object> propField)
        {
            return KeyToPropertyField.TryGetValue(CleanColumnName(columnName), out propField);
        }
    }
}