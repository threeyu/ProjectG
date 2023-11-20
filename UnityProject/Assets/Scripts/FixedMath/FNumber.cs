using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Ogopogo.FixedMath {
    [JsonConverter(typeof(FNumberConverter))]
    public struct FNumber {
        public static readonly long[] Pow10 = new[]
            { 1L, 10L, 100L, 1000L, 10000L, 100000L, 1000000L, 10000000L, 100000000L };

        public static readonly int N = 6;
        public static readonly long NNumber = 1000000;

        public static FNumber zero = FNumber.Bit(0);
        public static FNumber one = FNumber.Bit(NNumber);

        private long num;

        public static FNumber Bit(long n) {
            var res = new FNumber();
            res.num = n;
            return res;
        }

        public static FNumber Parse(int i) {
            return Bit(i * NNumber);
        }

        public static FNumber Parse(long l) {
            return Bit(l * NNumber);
        }

        public static FNumber Parse(float f) {
            return Bit((long)(f * NNumber));
        }

        public static FNumber Parse(string str) {
            if (str.Length == 0) {
                return FNumber.zero;
            }

            FNumber res = new FNumber();
            int sidx = 0;
            int f = 0;
            int neg = 1;

            if (str[0] == '-') {
                neg = -1;
                sidx++;
            }

            while (sidx < str.Length) {
                if (str[sidx] == '.') {
                    f = 1;
                    sidx++;
                    continue;
                }

                res.num = res.num * 10 + (str[sidx] - '0');
                if (f > 0) {
                    // 小数部分
                    f++;
                }

                if (f - 1 >= N) {
                    break;
                }

                sidx++;
            }

            // 四舍五入

            // f == 0表示没有遇到小数点 .
            if (f == 0) {
                f++;
            }

            while (f <= N) {
                res.num = res.num * 10;
                f++;
            }

            res.num *= neg;
            return res;
        }

        public override string ToString() {
            var sb = new StringBuilder();

            if (num == 0) {
                return "0";
            }

            var cur = num > 0 ? num : -num;
            var i = 0;
            var needDot = false;
            while (cur > 0) {
                var d = cur % 10;
                if (i < N) {
                    if (d > 0) {
                        sb.Append(d);
                        needDot = true;
                    }
                    else if (needDot) {
                        sb.Append(d);
                    }
                }
                else if (i == N) {
                    if (needDot) {
                        sb.Append('.');
                    }

                    sb.Append(d);
                }
                else {
                    sb.Append(d);
                }

                cur = cur / 10;
                i++;
            }

            if (i == N) {
                sb.Append(".0");
            }

            if (num < 0) {
                sb.Append('-');
            }

            return Reverse(sb.ToString());
        }

        public float ToFloat() {
            return num / (float)NNumber;
        }

        public int ToInt() {
            return (int)(num / NNumber);
        }

        public long ToLong() {
            return num / NNumber;
        }

        public long ToBit() {
            return num;
        }

        public static FNumber operator *(FNumber a, FNumber b) {
            return Bit(a.num * b.num / NNumber);
        }

        public static FNumber operator *(FNumber a, int b) {
            return Bit(a.num * b);
        }

        public static FNumber operator *(FNumber a, long b) {
            return Bit(a.num * b);
        }

        public static FNumber operator *(FNumber a, float b) {
            return Bit((long)(a.num * b));
        }

        public static FNumber operator *(int a, FNumber b) {
            return Bit(a * b.num);
        }

        public static FNumber operator *(long a, FNumber b) {
            return Bit(a * b.num);
        }

        public static FNumber operator *(float a, FNumber b) {
            return Bit((long)(a * b.num));
        }

        public static FNumber operator /(FNumber a, FNumber b) {
            return Bit(a.num * NNumber / b.num);
        }

        public static FNumber operator /(FNumber a, int b) {
            return Bit(a.num / b);
        }

        public static FNumber operator /(FNumber a, long b) {
            return Bit(a.num / b);
        }

        public static FNumber operator +(FNumber a, FNumber b) {
            return Bit(a.num + b.num);
        }

        public static FNumber operator +(FNumber a, int b) {
            return Bit(a.num + b * NNumber);
        }

        public static FNumber operator +(FNumber a, long b) {
            return Bit(a.num + b * NNumber);
        }

        public static FNumber operator +(FNumber a, float b) {
            return Bit((long)(a.num + b * NNumber));
        }

        public static FNumber operator +(float a, FNumber b) {
            return Bit((long)(a * NNumber + b.num));
        }


        public static FNumber operator -(FNumber a, FNumber b) {
            return Bit(a.num - b.num);
        }

        public static FNumber operator -(FNumber a, int b) {
            return Bit(a.num - b * NNumber);
        }

        public static FNumber operator -(FNumber a, long b) {
            return Bit(a.num - b * NNumber);
        }

        public static FNumber operator -(float a, FNumber b) {
            return Bit((long)(a * NNumber - b.num));
        }

        public static FNumber operator -(int a, FNumber b) {
            return Bit((long)(a * NNumber - b.num));
        }

        public static FNumber operator -(long a, FNumber b) {
            return Bit((long)(a * NNumber - b.num));
        }

        public static FNumber operator -(FNumber a, float b) {
            return Bit((long)(a.num - b * NNumber));
        }

        public static bool operator >=(FNumber a, FNumber b) {
            return a.num >= b.num;
        }

        public static bool operator <=(FNumber a, FNumber b) {
            return a.num <= b.num;
        }

        public static bool operator >(FNumber a, FNumber b) {
            return a.num > b.num;
        }

        public static bool operator >(FNumber a, int b) {
            return a.num > b * NNumber;
        }

        public static bool operator <(FNumber a, FNumber b) {
            return a.num < b.num;
        }

        public static bool operator <(FNumber a, int b) {
            return a.num < b * NNumber;
        }

        public static bool operator ==(FNumber a, FNumber b) {
            return a.num == b.num;
        }

        public static bool operator ==(FNumber a, int b) {
            return a.num == b * NNumber;
        }

        public static bool operator !=(FNumber a, FNumber b) {
            return a.num != b.num;
        }

        public static bool operator !=(FNumber a, int b) {
            return a.num != b * NNumber;
        }

        private static string Reverse(string s) {
            var sb = new StringBuilder();
            var idx = s.Length - 1;
            while (idx >= 0) {
                sb.Append(s[idx]);
                idx--;
            }

            return sb.ToString();
        }

        public int CompareTo(FNumber other) {
            return num.CompareTo(other.num);
        }

        public override int GetHashCode() {
            return num.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj is FNumber fn) {
                return fn.num == num;
            }

            return false;
        }
    }
}