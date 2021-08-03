﻿using System;
using System.Collections;
using System.Reflection;

namespace ReassureTest
{
    public static class ReusableProjectors
    {
        public static Projection SkipUnharvestableTypes(object parent, object fieldValue, PropertyInfo info)
        {
            var typename = fieldValue.GetType().ToString();
            if (typename.StartsWith("System.Reflection", StringComparison.Ordinal)
                || typename.StartsWith("System.Runtime", StringComparison.Ordinal)
                || typename.StartsWith("System.SignatureStruct", StringComparison.Ordinal)
                || typename.StartsWith("System.Func", StringComparison.Ordinal))
                return Projection.Ignore;
            return Projection.Use(fieldValue);
        }
    }

    /// <summary>
    /// Reusable implementations for field value translation
    /// </summary>
    public static class FieldValueTranslatorImplementations
    {
        public static object SimplifyExceptions(object o)
        {
            if (o is Exception ex)
                return new SimplifiedException(ex);
            return o;
        }

        //public static object IgnoreUnharvestableTypes(object o)
        //{
        //    var typename = o.GetType().ToString();
        //    if (typename.StartsWith("System.Reflection", StringComparison.Ordinal)
        //        || typename.StartsWith("System.Runtime", StringComparison.Ordinal)
        //        || typename.StartsWith("System.SignatureStruct", StringComparison.Ordinal)
        //        || typename.StartsWith("System.Func", StringComparison.Ordinal))
        //        return null;
        //    return o;
        //}

        public static object FixDefaultImmutableArrayCanNotBeTraversed(object o)
        {
            var type = o.GetType().ToString();
            if (!type.StartsWith("System.Collections.Immutable.ImmutableArray", StringComparison.Ordinal))
                return o;

            try
            {
                ((IEnumerable)o).GetEnumerator();
            }
            catch (InvalidOperationException)
            {
                return null;
            }

            return o;
        }
    }

    public class SimplifiedException
    {
        public string Message { get; set; }
        public IDictionary Data { get; set; }
        public string Type { get; set; }

        public SimplifiedException(Exception e)
        {
            Message = e.Message;
            Data = e.Data;
            Type = e.GetType().ToString();
        }
    }

}