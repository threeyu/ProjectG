using System;
using System.IO;

namespace Ogopogo.FixedMath {
    public class Fixed64 : IComparable<Fixed64> {
        public static int fracBits = 12;
        
        public static Fixed64 zero = BitConvert(0);
        public static Fixed64 one = ParseFrom(1.0f);
        
        private long num;

        public bool IsPositive() {
            return num > 0;
        }

        public static bool operator <=(Fixed64 a, Fixed64 b) {
            return a.num <= b.num;
        }

        public static bool operator >=(Fixed64 a, Fixed64 b) {
            return a.num >= b.num;
        }

        public static bool operator ==(Fixed64 a, Fixed64 b) {
            return a.num == b.num;
        }

        public static bool operator !=(Fixed64 a, Fixed64 b) {
            return a.num != b.num;
        }
        
        public static Fixed64 operator +(Fixed64 a, Fixed64 b) {
            var n = a.num + b.num;
            return BitConvert(n);
        }

        public static Fixed64 operator +(Fixed64 a, long b) {
            var n = a.num + (b << fracBits);
            return BitConvert(n);
        }
        
        public static Fixed64 operator +(Fixed64 a, int b) {
            var n = a.num + (b << fracBits);
            return BitConvert(n);
        }

        public static Fixed64 operator -(Fixed64 a, Fixed64 b) {
            var n = a.num - b.num;
            return BitConvert(n);
        }
        
        public static Fixed64 operator -(Fixed64 a, long b) {
            var n = a.num - (b << fracBits);
            return BitConvert(n);
        }
        
        public static Fixed64 operator -(Fixed64 a, int b) {
            var n = a.num - (b << fracBits);
            return BitConvert(n);
        }

        public static Fixed64 operator /(Fixed64 a, Fixed64 b) {
            var n = (a.num << fracBits) / b.num;
            return BitConvert(n);
        }

        public static Fixed64 operator *(Fixed64 a, int b) {
            return BitConvert(a.num * b);
        }
        
        public static Fixed64 operator *(Fixed64 a, long b) {
            return BitConvert(a.num * b);
        }
        
        public static Fixed64 operator *(Fixed64 a, float b) {
            var fb = Fixed64.ParseFrom(b);
            return BitConvert((a.num * fb.num) >> fracBits);
        }

        public static Fixed64 operator *(Fixed64 a, Fixed64 b) {
            return BitConvert((a.num * b.num) >> fracBits);
        }
        
        public static Fixed64 ParseFrom(float f) {
            return ParseFrom((double)f);
        }

        public static Fixed64 ParseFrom(int i) {
            var n = i << fracBits;
            return BitConvert(n);
        }

        public static Fixed64 ParseFrom(long l) {
            var n = l << fracBits;
            return BitConvert(n);
        }

        public static Fixed64 ParseFrom(double d) {
            var negative = false;
            if (d < 0) {
                negative = true;
                d = -d;
            }
            var intPart = (long) d;
            var floatFrac = d - (double) intPart;
            var fracPart = 0L;
            var i = fracBits-1;
            while (i > 0) {
                floatFrac = floatFrac * 2f;
                if (floatFrac >= 1f) {
                    fracPart = fracPart | (1L << i);
                    floatFrac -= 1f;
                }
                i--;
            }

            var n = (intPart << fracBits) | fracPart;
            if (negative) {
                return BitConvert(-n);
            }
            return BitConvert(n);
        }

        public float ToFloat() {
            var mask = 0x800L;
            var b = 0.5f;
            var res = 0.0f;
            while (mask > 0) {
                if ((num & mask) > 0) {
                    res += b;
                }

                b *= 0.5f;
                mask >>= 1;
            }

            res += (num >> fracBits);
            return res;
        }

        public int ToInt() {
            return (int)(num >> fracBits);
        }

        public long ToLong() {
            return (long) (num >> fracBits);
        }

        public static Fixed64 BitConvert(long n) {
            var r = new Fixed64();
            r.num = n;
            return r;
        }

        public long ToBit() {
            return num;
        }

        public int CompareTo(Fixed64 other) {
            return num.CompareTo(other.num);
        }

        public override bool Equals(object obj) {
            if (!(obj is Fixed64 typed)) {
                return false;
            }

            return typed.num == num;
        }

        public override int GetHashCode() {
            return num.GetHashCode();
        }

        public override string ToString() {
            return num.ToString();
        }
    }
}