using System;
using System.Collections;
using System.Collections.Generic;
using Ogopogo.FixedMath;
using UnityEngine;

namespace Ogopogo {
    public class FMath {
        public static FNumber Exp(FNumber a) {
            var p = FNumber.one;
            var q = FNumber.one;
            var res = FNumber.one;
            for (var i = 1; i <= 20; i++) {
                p *= a;
                q *= i;
                res += p / q;
            }
            return res;
        }

        /// <summary>
        /// 在特定位置上四舍五入，当传入d==N时，默认N+1位是进位
        /// </summary>
        /// <param name="n">定点数</param>
        /// <param name="d">小数点后的位数</param>
        /// <returns>四舍五入后的结果</returns>
        public static FNumber Round(FNumber n, int d=0) {
            if (d > FNumber.N) {
                throw new ArgumentException($"d is bigger than FNumber defined N {FNumber.N}");
            }

            var mod = FNumber.Pow10[FNumber.N - d];
            var bit = n.ToBit();
            var modRes = bit % mod;
            if (modRes >= mod / 2L) {
                // 需要进位，五入
                bit = bit + mod;
            }
            bit -= modRes;
            return FNumber.Bit(bit);
        }

        /// <summary>
        /// 在特定位置上四舍五入
        /// </summary>
        /// <param name="n">定点数</param>
        /// <param name="d">小数点后的位数</param>
        /// <returns>四舍五入后的结果</returns>
        public static string RoundToString(FNumber n, int d=0) {
            return Round(n, d).ToString();
        }

        public static FNumber Ceil(FNumber n) {
            return Round(n, 0);
        }
    }
}
