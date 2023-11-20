using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ogopogo.FixedMath;
using UnityEngine;

namespace ppy {
    public class GameData : MonoSingleton<GameData> {
        public enum OrderType {
            OrderBy, // 升序
            OrderByDescending, // 降序
        }

        public static readonly string Men100meterRaceTable = "Men100meterRace";
        public static readonly string MenStandTripleJumpTable = "MenStandTripleJump";
        public static readonly string MenShotPutTable = "MenShotPut";
        public static readonly string MenBasketBallTable = "MenBasketball";
        public static readonly string MenVolleyBallTouchHeightTable = "MenVolleyballTouchHeight";
        public static readonly string MenVolleyBallPassTable = "MenVolleyBallPass";
        public static readonly string MenPingpangBallTable = "MenPingpangBall";
        public static readonly string Women100meterRaceTable = "Women100meterRace";
        public static readonly string WomenStandTripleJumpTable = "WomenStandTripleJump";
        public static readonly string WomenShotPutTable = "WomenShotPut";
        public static readonly string WomenBasketBallTable = "WomenBasketBall";
        public static readonly string WomenVolleyBallTouchHeightTable = "WomenVolleyBallTouchHeight";
        public static readonly string WomenVolleyBallPassTable = "WomenVolleyBallPass";
        public static readonly string WomenPingpangBallTable = "WomenPingpangBall";

        public static readonly string ScoreTable = "Score";
        public static readonly string Category2NameTable = "Category2Name";

        private string[] mTableNames = new string[] {
            Men100meterRaceTable, MenStandTripleJumpTable, MenShotPutTable,
            MenBasketBallTable, MenVolleyBallTouchHeightTable, MenVolleyBallPassTable, MenPingpangBallTable,
            Women100meterRaceTable, WomenStandTripleJumpTable, WomenShotPutTable,
            WomenBasketBallTable, WomenVolleyBallTouchHeightTable, WomenVolleyBallPassTable, WomenPingpangBallTable,
            
            ScoreTable, Category2NameTable,
        };

        private Dictionary<string, Newtonsoft.Json.Linq.JObject> mTables =
            new Dictionary<string, Newtonsoft.Json.Linq.JObject>();

        public async UniTask Init() {
            Debug.Log("[GameData.Init]");
            
            for (var i = 0; i < mTableNames.Length; i++) {
                var tableName = mTableNames[i];
                // var ta = await Resources.LoadAsync<TextAsset>($"{tableName}") as TextAsset;
                // if (ta == null) {
                //     Debug.LogError($"[GameData.Init] failed to loadAsync table: {tableName}");
                //     continue;
                // }
                // var val = GetCellValue(ta.text);

                try {
                    var text = AssetManager.I.ReadAllText($"GameData/{tableName}.bytes");
                    var val = GetCellValue(text);
                    
                    I.mTables[tableName] = JsonUtils.FromJson(val) as Newtonsoft.Json.Linq.JObject;
                }
                catch (Exception e) {
                    Debug.LogError($"[GameData.Init] failed to load table: {tableName}");
                }
            }
        }

        public T Get<T>(string name, long id) where T : class {
            var idStr = id.ToString();
            if (I.mTables[name].TryGetValue(idStr, out var jt)) {
                var res = jt.ToObject<T>();
                return res;
            }

            return null;
        }

        public Newtonsoft.Json.Linq.JObject GetTable(string name) {
            return I.mTables[name];
        }

        public string[] GetAllTableNames() {
            return I.mTableNames;
        }

        public string[] GetAllFormulaTablenames() {
            var ret = new string[] {
                Men100meterRaceTable, MenStandTripleJumpTable, MenShotPutTable,
                MenBasketBallTable, MenVolleyBallTouchHeightTable, MenVolleyBallPassTable, MenPingpangBallTable,
                Women100meterRaceTable, WomenStandTripleJumpTable, WomenShotPutTable,
                WomenBasketBallTable, WomenVolleyBallTouchHeightTable, WomenVolleyBallPassTable, WomenPingpangBallTable,
            };
            return ret;
        }

        private string GetCellValue(string value) {
            value = value.TrimEnd();
            value = value.Replace("<换行>", "\\n");
            return value;
        }

        // ------------------ GameData
        public class BaseRaceConfigData {
            public long id;
            public int score;
            public FNumber grade;
        }
        
        public class CategoryConfigData {
            public long id;
            public string category;
            public string name;

            public override string ToString() {
                var ret = $"id: {id}, category: {category}, name: {name}";
                return ret;
            }
        }
    }
}