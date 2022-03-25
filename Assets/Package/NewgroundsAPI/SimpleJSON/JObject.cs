﻿using System;
using System.Collections.Generic;

namespace SimpleJSON {
    public enum JObjectKind {
        Object,
        Array,
        String,
        Number,
        Boolean,
        Null
    }

    public enum IntegerSize {
        UInt64,
        Int64,
        UInt32,
        Int32,
        UInt16,
        Int16,
        UInt8,
        Int8,
    }

    public enum FloatSize {
        Double,
        Single
    }

    public class JObject {
        public JObjectKind Kind { get; private set; }

        public Dictionary<string, JObject> ObjectValue { get; private set; }
        public List<JObject> ArrayValue { get; private set; }
        public string StringValue { get; private set; }
        public bool BooleanValue { get; private set; }

        public int Count {
            get {
                return Kind == JObjectKind.Array ? ArrayValue.Count
                     : Kind == JObjectKind.Object ? ObjectValue.Count
                     : 0;
            }
        }

        public double DoubleValue { get; private set; }
        public float FloatValue { get; private set; }
        public ulong ULongValue { get; private set; }
        public long LongValue { get; private set; }
        public uint UIntValue { get; private set; }
        public int IntValue { get; private set; }
        public ushort UShortValue { get; private set; }
        public short ShortValue { get; private set; }
        public byte ByteValue { get; private set; }
        public sbyte SByteValue { get; private set; }

        public bool IsNegative { get; private set; }
        public bool IsFractional { get; private set; }
        public IntegerSize MinInteger { get; private set; }
        public FloatSize MinFloat { get; private set; }

        public JObject this[string key] { get { return ObjectValue[key]; } }
        public JObject this[int key] { get { return ArrayValue[key]; } }

        public static explicit operator string(JObject obj) { return obj.StringValue; }
        public static explicit operator bool(JObject obj) { return obj.BooleanValue; }
        public static explicit operator double(JObject obj) { return obj.DoubleValue; }
        public static explicit operator float(JObject obj) { return obj.FloatValue; }
        public static explicit operator ulong(JObject obj) { return obj.ULongValue; }
        public static explicit operator long(JObject obj) { return obj.LongValue; }
        public static explicit operator uint(JObject obj) { return obj.UIntValue; }
        public static explicit operator int(JObject obj) { return obj.IntValue; }
        public static explicit operator ushort(JObject obj) { return obj.UShortValue; }
        public static explicit operator short(JObject obj) { return obj.ShortValue; }
        public static explicit operator byte(JObject obj) { return obj.ByteValue; }
        public static explicit operator sbyte(JObject obj) { return obj.SByteValue; }

        public static JObject CreateString(string str) { return new JObject(str); }
        public static JObject CreateBoolean(bool b) { return new JObject(b); }
        public static JObject CreateNull() { return new JObject(); }
        public static JObject CreateNumber(bool isNegative, bool isFractional, bool negativeExponent, ulong integerPart, ulong fractionalPart, int fractionalPartLength, ulong exponent) { return new JObject(isNegative, isFractional, negativeExponent, integerPart, fractionalPart, fractionalPartLength, exponent); }
        public static JObject CreateArray(List<JObject> list) { return new JObject(list); }
        public static JObject CreateObject(Dictionary<string, JObject> dict) { return new JObject(dict); }

        private JObject(string str) {
            Kind = JObjectKind.String;
            StringValue = str;
        }

        private JObject(bool b) {
            Kind = JObjectKind.Boolean;
            BooleanValue = b;
        }

        private JObject() {
            Kind = JObjectKind.Null;
        }

        private JObject(bool isNegative, bool isFractional, bool negativeExponent, ulong integerPart, ulong fractionalPart, int fractionalPartLength, ulong exponent) {
            Kind = JObjectKind.Number;
            if (!isFractional) {
                MakeInteger(isNegative, integerPart);
            } else {
                MakeFloat(isNegative, negativeExponent, integerPart, fractionalPart, fractionalPartLength, exponent);
            }
        }

        private JObject(List<JObject> list) {
            Kind = JObjectKind.Array;
            ArrayValue = list;
        }

        private JObject(Dictionary<string, JObject> dict) {
            Kind = JObjectKind.Object;
            ObjectValue = dict;
        }

        private void MakeInteger(bool isNegative, ulong integerPart) {
            IsNegative = isNegative;

            if (!IsNegative) {
                ULongValue = integerPart;
                MinInteger = IntegerSize.UInt64;

                if (ULongValue <= Int64.MaxValue) {
                    LongValue = (long)ULongValue;
                    MinInteger = IntegerSize.Int64;
                }

                if (ULongValue <= UInt32.MaxValue) {
                    UIntValue = (uint)ULongValue;
                    MinInteger = IntegerSize.UInt32;
                }

                if (ULongValue <= Int32.MaxValue) {
                    IntValue = (int)ULongValue;
                    MinInteger = IntegerSize.Int32;
                }

                if (ULongValue <= UInt16.MaxValue) {
                    UShortValue = (ushort)ULongValue;
                    MinInteger = IntegerSize.UInt16;
                }

                if (ULongValue <= (ulong)Int16.MaxValue) {
                    ShortValue = (short)ULongValue;
                    MinInteger = IntegerSize.Int16;
                }

                if (ULongValue <= Byte.MaxValue) {
                    ByteValue = (byte)ULongValue;
                    MinInteger = IntegerSize.UInt8;
                }

                if (ULongValue <= (ulong)SByte.MaxValue) {
                    SByteValue = (sbyte)ULongValue;
                    MinInteger = IntegerSize.Int8;
                }

                DoubleValue = ULongValue;
                MinFloat = FloatSize.Double;

                if (DoubleValue <= Single.MaxValue) {
                    FloatValue = (float)DoubleValue;
                    MinFloat = FloatSize.Single;
                }
            } else {
                LongValue = -(long)integerPart;
                MinInteger = IntegerSize.Int64;

                if (LongValue >= Int32.MinValue) {
                    IntValue = (int)LongValue;
                    MinInteger = IntegerSize.Int32;
                }

                if (LongValue >= Int16.MinValue) {
                    ShortValue = (short)LongValue;
                    MinInteger = IntegerSize.Int16;
                }

                if (LongValue >= SByte.MinValue) {
                    SByteValue = (sbyte)LongValue;
                    MinInteger = IntegerSize.Int8;
                }

                DoubleValue = LongValue;
                MinFloat = FloatSize.Double;

                if (DoubleValue >= -Single.MaxValue) {
                    FloatValue = (float)DoubleValue;
                    MinFloat = FloatSize.Single;
                }
            }
        }

        private void MakeFloat(bool isNegative, bool negativeExponent, ulong integerPart, ulong fractionalPart, int fractionalPartLength, ulong exponent) {
            DoubleValue = (isNegative ? -1 : 1) * ((double)integerPart + (double)fractionalPart / Math.Pow(10, fractionalPartLength)) * Math.Pow(10, (negativeExponent ? -1 : 1) * (long)exponent);
            MinFloat = FloatSize.Double;
            IsFractional = true;

            if (DoubleValue < 0) {
                IsNegative = true;

                if (DoubleValue >= -Single.MaxValue) {
                    FloatValue = (float)DoubleValue;
                    MinFloat = FloatSize.Single;
                }
            } else if (DoubleValue <= Single.MaxValue) {
                FloatValue = (float)DoubleValue;
                MinFloat = FloatSize.Single;
            }
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(obj, this)) return true;
            if (!(obj is JObject)) return false;

            var jobj = (JObject)obj;
            if (jobj.Kind != Kind) return false;

            switch (Kind) {
            case JObjectKind.Array:
                if (ArrayValue.Count != jobj.ArrayValue.Count) return false;
                for (var i = 0; i < ArrayValue.Count; ++i) {
                    if (!ArrayValue[i].Equals(jobj.ArrayValue[i])) return false;
                }
                return true;
            case JObjectKind.Boolean:
                return BooleanValue == jobj.BooleanValue;
            case JObjectKind.Number:
                return EqualNumber(this, jobj);
            case JObjectKind.Object:
                if (ObjectValue.Count != jobj.ObjectValue.Count) return false;
                foreach (var pair in ObjectValue) {
                    if (!jobj.ObjectValue.ContainsKey(pair.Key) ||
                        !pair.Value.Equals(jobj.ObjectValue[pair.Key])) return false;
                }
                return true;
            case JObjectKind.String:
                return StringValue == jobj.StringValue;
            }

            return true;
        }

        public override int GetHashCode() {
            switch (Kind) {
            case JObjectKind.Array: return ArrayValue.GetHashCode();
            case JObjectKind.Boolean: return BooleanValue.GetHashCode();
            case JObjectKind.Null: return 0;
            case JObjectKind.Object: return ObjectValue.GetHashCode();
            case JObjectKind.String: return StringValue.GetHashCode();
            case JObjectKind.Number:
                if (IsFractional) return DoubleValue.GetHashCode();
                if (IsNegative) return LongValue.GetHashCode();
                return ULongValue.GetHashCode();
            }
            return 0;
        }

        public static bool EqualNumber(JObject o1, JObject o2) {
            if (o1.MinFloat != o2.MinFloat ||
                o1.MinInteger != o2.MinInteger ||
                o1.IsNegative != o2.IsNegative ||
                o1.IsFractional != o2.IsFractional) return false;

            if (o1.IsFractional) {
                return o1.DoubleValue == o2.DoubleValue;
            }
            if (o1.IsNegative) {
                return o1.LongValue == o2.LongValue;
            }

            return o1.ULongValue == o2.ULongValue;
        }
    }
}