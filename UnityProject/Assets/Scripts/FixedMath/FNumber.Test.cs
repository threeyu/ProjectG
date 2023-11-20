using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Ogopogo;
using ppy;
using UnityEngine;

namespace Ogopogo.FixedMath {
    public class FNumberTests {
        public static void Test() {
            /*TestAllParse();
            TestAllMath();
            TestAllToString();
            */
            TestAllToString();
        }

        public static void TestAllToString() {
            Log.Debug(FNumber.Parse("100.00").ToString());
            Log.Debug(FNumber.Parse("100.01").ToString());
            Log.Debug(FNumber.Parse("1000000.01").ToString());
            Log.Debug(FNumber.Parse("0.01").ToString());
            Log.Debug(FNumber.Parse("0.35").ToString());
            
            Log.Debug(FNumber.Parse("-0.3").ToString());
            Log.Debug(FNumber.Parse("-3").ToString());
            Log.Debug(FNumber.Parse("-33").ToString());

            /*for (var i = 0; i < 100; i++) {
                var f = UnityEngine.Random.Range(0f, 100f);
                Log.Debug(f, FNumber.Parse(f).ToString());
            }*/
        }

        public static void TestAllParse() {
            TestParse("0.000001");
            TestParse("0.00001");
            TestParse("0.0001");
            TestParse("0.001");
            TestParse("0.01");
            TestParse("0.1");
            TestParse("1");
            TestParse("0");
            TestParse("-1");
            TestParse("-0");
            TestParse("-0.000001");
            TestParse("-0.00001");
            TestParse("-0.0001");
            TestParse("-0.001");
            TestParse("-0.01");
            TestParse("-0.1");
            TestParse("0.64");
            TestParse("64.64");
            TestParse("64.6464");
            TestParse("64.64646466");
            TestParse("878764.64646466");
            TestParse("-878764.64646466");
            TestParse("-99999999.99999999999");
            TestParse("99999999999999999999999999999.99999999999");
            TestParse("-99999999999999999999999999999.99999999999");

            // TestParseFile();
            // TestRoundFile();
        }

        public static void TestParseFile() {
            Log.Debug("[TestParseFile] Start =====================");
            var ctx = File.ReadAllText("E:/decimalTest");
            var lines = ctx.Split('\n');
            foreach (var line in lines) {
                var parts = line.Split(' ');
                var res = FNumber.Parse(parts[0]);
                var bit = long.Parse(parts[1]);
                if (bit == res.ToBit()) {
                    Log.Debug("[TestParseFile] Pass:", line);
                } else {
                    Log.Error("[TestParseFile] Failed:", line, "bit:", bit, "FNumber.ToBit", res.ToBit());
                }
            }
            Log.Debug("[TestParseFile] End =====================");
        }

        public static void TestRoundFile() {
            Log.Debug("[TestRoundFile] Start =====================");
            var ctx = File.ReadAllText("C:/Users/pc/Downloads/decimalTestRound2");
            var lines = ctx.Split('\n');
            foreach (var line in lines) {
                var parts = line.Split(' ');
                if (parts.Length != 3) {
                    Log.Error("[TestRoundFile] Syntax error:", line);
                    continue;
                }
                var num = FNumber.Parse(parts[0]);
                var bit = long.Parse(parts[1]);
                var rounded = long.Parse(parts[2]);

                if (bit == num.ToBit() && rounded == FMath.Round(num, 0).ToBit()) {
                    Log.Debug("[TestRoundFile] Pass:", line);
                } else {
                    Log.Error("[TestRoundFile] Failed:", line, "num:", num.ToBit());
                }
            }
            Log.Debug("[TestRoundFile] End =====================");
        }

        public static void TestAllMath() {
            Log.Debug(FNumber.one + FNumber.one);
            Log.Debug(FNumber.one + 1);
            Log.Debug(FNumber.one + 1L);
            
            Log.Debug(FNumber.one - FNumber.one);
            Log.Debug(FNumber.one - 1);
            Log.Debug(FNumber.one - 1L);
            
            Log.Debug(FNumber.one * 10);
            Log.Debug(FNumber.one * 3);
            Log.Debug(FNumber.one * 3L);
            
            Log.Debug(FNumber.one / 10);
            Log.Debug(FNumber.one / 5);
            Log.Debug(FNumber.one / 7);
            Log.Debug(FNumber.one / 3);
            Log.Debug(FNumber.one / 5L);
            
            Log.Debug(FMath.Exp(FNumber.zero).ToFloat());
            Log.Debug(FMath.Exp(FNumber.one).ToFloat());
            Log.Debug(FMath.Exp(FNumber.one + FNumber.one).ToFloat());
            
            Log.Debug(FMath.Round(FNumber.Parse("10.55"), 0).ToFloat());
            Log.Debug(FMath.Round(FNumber.Parse("10.55"), 1).ToFloat());
            Log.Debug(FMath.Round(FNumber.Parse("10.55"), 2).ToFloat());
            Log.Debug(FMath.Round(FNumber.Parse("10.55"), 3).ToFloat());
            Log.Debug(FMath.Round(FNumber.Parse("10.55"), 6).ToBit());
            
            //
            // var two = FNumber.one + FNumber.one;
            // Stopwatch w = Stopwatch.StartNew();
            // for (var i = 0; i < 100000; i++) {
            //     FMath.Exp(two);
            // }
            // w.Stop();
            //
            // Log.Debug(w.ElapsedMilliseconds);
        }

        public static void TestParse(string str) {
            var f = FNumber.Parse(str);
            Log.Debug("[TestParse] str:", str, "f:", f);
        }
    }
}