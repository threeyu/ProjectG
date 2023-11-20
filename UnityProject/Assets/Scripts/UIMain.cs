using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ppy.ui {
    public class UIMain : MonoBehaviour {
        // 成绩数据类型
        public class RaceCalData {
            public string category;
            public string tableName;
            public GameData.OrderType orderType;
            public List<GameData.BaseRaceConfigData> datas = new List<GameData.BaseRaceConfigData>();
        }

        public Button backBtn;
        public Button loadBtn;
        public Button nextBtn;
        public Button preBtn;
        public Button calBtn;
        public Button importBtn;
        public Button exportBtn;

        public GameObject menGridGo;
        public GameObject womenGridGo;
        public UIActiveGroup contentGroup;

        public UIItem[] menItems;
        public UIItem[] womenItems;

        private int mCurrentPage;
        private int mTotalPage;

        private Dictionary<string, RaceCalData> mCategory2RaceData = new Dictionary<string, RaceCalData>();

        // private string[] mCNTableNames = new string[] {
        //     "男子100米跑", "男子立定三级跳远", "男子原地推铅球", "男子篮球运球绕杆定点投篮", "男子排球助跑摸高", "排球隔网定向垫球、传球", "男子乒乓球左推右攻",
        //     "女子100米跑", "女子立定三级跳远", "女子原地推铅球", "女子篮球运球绕杆定点投篮", "女子排球助跑摸高", "排球隔网定向垫球、传球", "女子乒乓球左推右攻",
        // };

        // <index, name>
        private Dictionary<long, string> mIndex2Name = new Dictionary<long, string>();

        // <category, index>
        private Dictionary<string, long> mCategory2Index = new Dictionary<string, long>();

        private void OnEnable() {
            // Debug.Log("[UIMain.OnEnable]");
            backBtn.onClick.AddListener(OnBackClick);
            loadBtn.onClick.AddListener(OnLoadClick);
            calBtn.onClick.AddListener(OnCalClick);
            importBtn.onClick.AddListener(OnImportClick);
            exportBtn.onClick.AddListener(OnExportClick);
            nextBtn.onClick.AddListener(OnNextClick);
            preBtn.onClick.AddListener(OnPreClick);
        }

        private void OnDisable() {
            // Debug.Log("[UIMain.OnDisable]");
            backBtn.onClick.RemoveAllListeners();
            loadBtn.onClick.RemoveAllListeners();
            calBtn.onClick.RemoveAllListeners();
            importBtn.onClick.RemoveAllListeners();
            exportBtn.onClick.RemoveAllListeners();
            nextBtn.onClick.RemoveAllListeners();
            preBtn.onClick.RemoveAllListeners();
        }

        private void Start() {
            mCurrentPage = 0;
            mTotalPage = 2;

            contentGroup.SetActive("Load");
        }

        private void OnBackClick() {
            Log.Debug("[UIMain.OnBackClick]");

            contentGroup.SetActive("Menu");
        }

        private void OnLoadClick() {
            Log.Debug("[UIMain.OnLoadClick]");

            contentGroup.SetActive("Menu");

            DoLoad();
            ShowInfo();
        }

        private void OnCalClick() {
            Log.Debug("[UIMain.OnLoadClick]");

            contentGroup.SetActive("Calculate");
        }

        private void OnImportClick() {
            Log.Debug("[UIMain.OnImportClick]");

            // DoImport();
            DoOneKeyCalculate();
        }

        private void OnExportClick() {
            Log.Debug("[UIMain.OnExportClick]");

            DoExport();
        }

        private void OnNextClick() {
            if (mCurrentPage < mTotalPage - 1) {
                mCurrentPage++;
                ShowCurrentPage(this.mCurrentPage);
            }
        }

        private void OnPreClick() {
            if (mCurrentPage > 0) {
                mCurrentPage--;
                ShowCurrentPage(this.mCurrentPage);
            }
        }

        private void DoLoad() {
            // load category
            GetCategoryNameTable();

            // load formula
            var tableNames = GameData.I.GetAllFormulaTablenames();
            LoadConfig(Category.Male, tableNames, menItems);
            LoadConfig(Category.Female, tableNames, womenItems);
        }

        private void GetCategoryNameTable() {
            mIndex2Name.Clear();
            mCategory2Index.Clear();

            var tableName = GameData.Category2NameTable;
            var tableObj = GameData.I.GetTable(tableName);

            foreach (var pair in tableObj) {
                var id = Convert.ToInt64(pair.Key);
                var cfgData = GameData.I.Get<GameData.CategoryConfigData>(tableName, id);

                mIndex2Name[id] = cfgData.name;
                mCategory2Index[cfgData.category] = id;
            }
        }

        private enum Category {
            Male,
            Female,
        }

        private void LoadConfig(Category category, string[] tableNames, UIItem[] uiItems) {
            var tableLen = tableNames.Length;
            var total = Mathf.FloorToInt(tableLen * 0.5f);
            var startIdx = category == Category.Male ? 0 : total;
            var curTableNames = new string[total];
            Array.Copy(tableNames, startIdx, curTableNames, 0, total);

            mCategory2RaceData.Clear();

            var dataLen = curTableNames.Length;
            for (var i = 0; i < uiItems.Length; i++) {
                var item = uiItems[i];

                if (i < dataLen) {
                    var tableName = curTableNames[i];
                    var tableObj = GameData.I.GetTable(tableName);
                    var cfgDatas = new List<GameData.BaseRaceConfigData>();
                    var orderType = TableName2OrderType(tableName);

                    foreach (var pair in tableObj) {
                        var id = Convert.ToInt64(pair.Key);
                        var cfgData = GameData.I.Get<GameData.BaseRaceConfigData>(tableName, id);
                        cfgDatas.Add(cfgData);
                    }

                    cfgDatas.Sort((a, b) => a.id.CompareTo(b.id));

                    var title = TableName2Title(tableNames, tableName);
                    item.SetData(i, title, orderType, cfgDatas);

                    var calData = new RaceCalData {
                        category = tableName,
                        tableName = title,
                        orderType = orderType,
                    };
                    calData.datas.AddRange(cfgDatas);
                    mCategory2RaceData[tableName] = calData;
                }

                item.gameObject.SetActive(i < dataLen);
            }
        }

        private GameData.OrderType TableName2OrderType(string tableName) {
            if (tableName == GameData.Men100meterRaceTable
                || tableName == GameData.MenBasketBallTable
                || tableName == GameData.Women100meterRaceTable
                || tableName == GameData.WomenBasketBallTable) {
                // 降序
                return GameData.OrderType.OrderByDescending;
            }

            // 升序
            return GameData.OrderType.OrderBy;
        }

        private string TableName2Title(string[] allTableNames, string tableName) {
            var idx = -1;
            for (var i = 0; i < allTableNames.Length; i++) {
                if (allTableNames[i] == tableName) {
                    idx = (i + 1);
                    break;
                }
            }

            if (idx == -1) {
                return "";
            }

            if (this.mIndex2Name.TryGetValue(idx, out var ret)) {
                return ret;
            }
            else {
                Log.Error("[UIMain.TableName2Title] not found, idx:", idx);
                return "";
            }
        }

        private void ShowInfo() {
            ShowCurrentPage(this.mCurrentPage);
        }

        private void ShowCurrentPage(int curPage) {
            if (curPage == 0) {
                this.menGridGo.SetActive(true);
                this.womenGridGo.SetActive(false);

                this.nextBtn.gameObject.SetActive(true);
                this.preBtn.gameObject.SetActive(false);
            }
            else if (curPage == 1) {
                this.menGridGo.SetActive(false);
                this.womenGridGo.SetActive(true);

                this.nextBtn.gameObject.SetActive(false);
                this.preBtn.gameObject.SetActive(true);
            }
        }

        private void DoImport() {
            // 1. 检查Input路径下合法性
            // 2. 输入表格数据并反序列化
            // 3. 将表格数据存入缓存
        }

        private void DoExport() {
            // 1. 检查缓存数据合法性
            // 2. 计算表格内数据
            // 3. 序列化最新数据并输出
        }

        private void DoOneKeyCalculate() {
            // 1. 检查Input路径下合法性
            // 2. 输入表格数据并反序列化
            // 3. 将表格数据存入缓存
            // 4. 计算表格内数据
            // 5. 序列化最新数据并输出Output

            var fp = $"{IOUtils.StreamingAssetsPath()}/Input/Score.xlsx";
            if (!File.Exists(fp)) {
                Log.Error("[UIMain.DoOneKeyCalculate] file is not exist, path:", fp);
                return;
            }

            var outputPath = $"{IOUtils.StreamingAssetsPath()}/Output/Score.xlsx";
            File.Copy(fp, outputPath, true);
            Log.Debug("[UIMain.DoOneKeyCalculate] copy success!");

            var fileInfo = new FileInfo(outputPath);
            Log.Debug("[UIMain.DoOneKeyCalculate] ready to cal...");

            using (var excelPackage = new ExcelPackage(fileInfo)) {
                var workSheet = excelPackage.Workbook.Worksheets["Score"];
                var rows = workSheet.Dimension.Rows;
                var cols = workSheet.Dimension.Columns;

                // 成绩列的下标
                var itemColIndexs = new int[] {
                    5, 8, 11, 14
                };

                var startRowIdx = 6;
                var endRowIdx = rows;

                for (var i = 0; i < itemColIndexs.Length; i++) {
                    var item = workSheet.Cells[4, itemColIndexs[i]]; // 项目
                    var itemName = item.Value.ToString();
                    Log.Debug("item name:", itemName);

                    if (!this.mCategory2Index.TryGetValue(itemName, out var categoryIdx)) {
                        Log.Error($"[UIMain.DoOneKeyCalculate] category: {itemName} is not exist.");
                        continue;
                    }

                    // for (var row = startRowIdx; row <= endRowIdx; row++) {
                    //     var grade = workSheet.Cells[rows, i + 1];
                    //     
                    // }
                }

                excelPackage.Save();
            }
        }

        private int FindScoreByGrade(float grade, GameData.OrderType orderType,
            List<GameData.BaseRaceConfigData> cDatas) {
            Log.Debug("[UIMain.FindScoreByGrade]");

            var targetIdx = -1;
            var dataLen = cDatas.Count;

            // Use binary search for optimization
            int start = 0, end = dataLen - 1;
            while (start <= end) {
                var mid = start + (end - start) / 2;
                var cData = cDatas[mid];
                var cGrade = cData.grade.ToFloat();

                if (cGrade == grade) {
                    targetIdx = mid;
                    break;
                }

                if (orderType == GameData.OrderType.OrderBy) {
                    if (grade < cGrade) {
                        end = mid - 1;
                    }
                    else {
                        start = mid + 1;
                    }
                }
                else {
                    if (grade > cGrade) {
                        end = mid - 1;
                    }
                    else {
                        start = mid + 1;
                    }
                }
            }

            if (targetIdx == -1) {
                targetIdx = orderType == GameData.OrderType.OrderBy ? start - 1 : end;
            }

            var ret = cDatas[targetIdx].score;
            return ret;
        }

        // --------------------------------------------------------------------

        #region Read/Write

        #endregion

        // --------------------------------------------------------------------
    }
}