using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MarkSFrancis.Reflection;

namespace MarkSFrancis.IO.DelimitedData.Maps
{
    public abstract class BaseMap<TMap, T, TMapBy> where T : new()

    {
        private Type TType { get; }
        protected PropertyInfo[] TProperties { get; }
        protected FieldInfo[] TFields { get; }

        protected Dictionary<TMapBy, PropertyFieldInfo<T, object>> KeyToPropertyField { get; }
        protected Dictionary<string, TMapBy> PropertyFieldToKey { get; }

        public BaseMap()
        {
            TType = typeof(T);
            TProperties = TType.GetProperties().Where(p => p.CanWrite && !p.SetMethod.IsStatic).ToArray();
            TFields = TType.GetFields().Where(p => !p.IsInitOnly && !p.IsLiteral && !p.IsStatic).ToArray();

            KeyToPropertyField = new Dictionary<TMapBy, PropertyFieldInfo<T, object>>();
            PropertyFieldToKey = new Dictionary<string, TMapBy>();
        }

        public abstract IEnumerable<string> GetColumnHeadings();

        public int TotalColumns() => GetColumnHeadings().Count();

        protected void AssignToMember(string value, PropertyFieldInfo<T, object> propFieldInfo, T assignTo)
        {
            var columnType = propFieldInfo.Type;

            object convertedValue = Convert.ChangeType(value, columnType);

            propFieldInfo.SetValue(assignTo, convertedValue);
        }

        protected string GetFromMember(T getFrom, PropertyFieldInfo<T, object> propFieldInfo)
        {
            return propFieldInfo.GetValue(getFrom).ToString();
        }

        /// <summary>
        /// Assigns the relevant property a value given the column Id and column name in accordance to the object map. The value is ignored if the column is not mapped
        /// </summary>
        /// <param name="mapBy">The name of the column containing the value</param>
        /// <param name="value">The value contained within the field to be assigned to the object</param>
        /// <param name="baseObject">The base object whose property will be set</param>
        protected bool TryAssignToMember(TMapBy mapBy, string value, T baseObject)
        {
            if (!TryGetMapping(mapBy, out var propFieldInfo))
            {
                return false;
            }

            AssignToMember(value, propFieldInfo, baseObject);

            return true;
        }

        public abstract TMap Map(PropertyFieldInfo<T, object> propFieldInfo, TMapBy mapBy);

        public TMap Map(Expression<Func<T, object>> expression, TMapBy columnName)
        {
            var expressionInfo = new PropertyFieldInfo<T, object>(expression);

            return Map(expressionInfo, columnName);
        }

        public TMap Map(MemberInfo member, TMapBy mapBy)
        {
            var memberInfo = new PropertyFieldInfo<T, object>(member);

            return Map(memberInfo, mapBy);
        }

        protected abstract bool TryGetMapping(TMapBy mapBy, out PropertyFieldInfo<T, object> propField);

        protected abstract bool TryGetMapping(PropertyFieldInfo<T, object> propField, out TMapBy mapBy);

        protected abstract bool TryAppendSpecificValue(T record, PropertyFieldInfo<T, object> propFieldInfo,
            IList<string> values);

        public IList<string> MapFromObject(T record)
        {
            string[] fields = new string[TotalColumns()];

            foreach (var property in TProperties)
            {
                var propFieldInfo = new PropertyFieldInfo<T, object>(property);
                TryAppendSpecificValue(record, propFieldInfo, fields);
            }


            foreach (var field in TFields)
            {
                var propFieldInfo = new PropertyFieldInfo<T, object>(field);
                TryAppendSpecificValue(record, propFieldInfo, fields);
            }

            return fields;
        }

        public T MapToObject(IList<KeyValuePair<TMapBy, string>> record)
        {
            T newT = new T();

            for (int columnId = 0; columnId < record.Count; columnId++)
            {
                TryAssignToMember(record[columnId].Key, record[columnId].Value, newT);
            }

            return newT;
        }
    }
}