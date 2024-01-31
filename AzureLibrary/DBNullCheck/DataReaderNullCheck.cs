using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.DBNullCheck
{
    public static class DataReaderNullCheck
    {
        public static byte? TryGetByte(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (byte?)reader.GetByte(index) : null;
        }

        public static short? TryGetInt16(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (short?)reader.GetInt16(index) : null;
        }

        public static int? TryGetInt32(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (int?)reader.GetInt32(index) : null;
        }

        public static long? TryGetInt64(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (long?)reader.GetInt64(index) : null;
        }

        public static string TryGetString(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? reader.GetString(index) : null;
        }

        public static float? TryGetFloat(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (float?)reader.GetFloat(index) : null;
        }

        public static double? TryGetDouble(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (double?)reader.GetDouble(index) : null;
        }

        public static decimal? TryGetDecimal(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (decimal?)reader.GetDecimal(index) : null;
        }

        public static char? TryGetChar(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            if (!reader.IsDBNull(index))
            {
                string value = reader.GetString(index);
                return !string.IsNullOrEmpty(value) ? value[0] : (char?)null;
            }

            return null;
        }

        public static bool? TryGetBoolean(this IDataReader reader, string name)
        {
            int index = reader.GetOrdinal(name);
            return !reader.IsDBNull(index) ? (bool?)reader.GetBoolean(index) : null;
        }

        //

        public static bool GetBoolean(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetBoolean method.");
            }

            return reader.GetBoolean(ordinalOrThrow);
        }

        public static DateTime GetDateTime(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetDateTime method.");
            }

            return reader.GetDateTime(ordinalOrThrow);
        }

        public static decimal GetDecimal(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetDecimal method.");
            }

            return reader.GetDecimal(ordinalOrThrow);
        }

        public static double GetDouble(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetDouble method.");
            }

            return reader.GetDouble(ordinalOrThrow);
        }

        public static float GetFloat(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetFloat method.");
            }

            return reader.GetFloat(ordinalOrThrow);
        }

        public static Guid GetGuid(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetGuid method.");
            }

            return reader.GetGuid(ordinalOrThrow);
        }

        public static short GetInt16(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetInt16 method.");
            }

            return reader.GetInt16(ordinalOrThrow);
        }

        public static int GetInt32(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetInt32 method.");
            }

            return reader.GetInt32(ordinalOrThrow);
        }

        public static long GetInt64(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetInt64 method.");
            }

            return reader.GetInt64(ordinalOrThrow);
        }

        public static string GetString(this IDataReader reader, string name)
        {
            int ordinalOrThrow = reader.GetOrdinalOrThrow(name);
            if (reader.IsDBNull(ordinalOrThrow))
            {
                throw new SqlNullValueException($"\"{name}\" is Null. This method cannot be called on Null values. Code should be modified to call the TryGetString method.");
            }

            return reader.GetString(ordinalOrThrow);
        }

        private static int GetOrdinalOrThrow(this IDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            if (ordinal == -1)
            {
                string message = $"Column \"{name}\" does not exist in the result set.";
                throw new ArgumentOutOfRangeException("name", message);
            }

            return ordinal;
        }
    }

}
