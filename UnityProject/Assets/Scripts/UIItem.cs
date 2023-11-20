using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Ogopogo.FixedMath;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ppy.ui {
    public class UIItem : MonoBehaviour {
        public TextMeshProUGUI titleTxt;
        public InputField inputField;
        public TextMeshProUGUI outputTxt;

        public int index;
        public Func<float, string> onInputChange;
        public Action<int> onImport;
        public Action<int> onExport;

        private List<GameData.BaseRaceConfigData> mDatas;
        private GameData.OrderType mOrderType;
        private float mMinGrade;
        private float mMaxGrade;

        private void OnEnable() {
            // Debug.Log("[UIItem.OnEnable]");
            inputField.onValueChanged.AddListener(OnInputChanged);
        }

        private void OnDisable() {
            // Debug.Log("[UIItem.OnDisable]");
            inputField.onValueChanged.RemoveAllListeners();
        }

        private void Awake() {
            outputTxt.text = "";
        }

        private void OnInputChanged(string str) {
            if (string.IsNullOrEmpty(str)) {
                outputTxt.text = "";
                return;
            }

            var inputGrade = FNumber.Parse(str).ToFloat();
            if (inputGrade < 0) {
                outputTxt.text = "";
                return;
            }
            Debug.Log($"[UIItem.OnInputChanged] str: {str}, inputGrade: {inputGrade}");

            if (inputGrade <= this.mMinGrade) {
                inputGrade = this.mMinGrade;
            }
            else if (inputGrade >= this.mMaxGrade) {
                inputGrade = this.mMaxGrade;
            }

            var output = FindScoreByGrade(inputGrade);
            outputTxt.text = output.ToString();
        }

        // private void OnImportBtnClick() {
        //     Log.Debug("[UIItem.OnImportBtnClick] index:");
        //     this.onImport?.Invoke(this.index);
        // }
        //
        // private void OnExportBtnClick() {
        //     Log.Debug("[UIItem.OnExportBtnClick] index:");
        //     this.onExport?.Invoke(this.index);
        // }

        private int FindScoreByGrade(float grade) {
            Debug.Log($"[UIItem.FindScoreByGrade] grade: {grade}");

            var orderType = this.mOrderType;
            var cDatas = this.mDatas;
            var targetIdx = -1;
            var dataLen = this.mDatas.Count;

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

        public void SetData(int index, string title, GameData.OrderType orderType,
            List<GameData.BaseRaceConfigData> datas) {
            var len = datas.Count;
            if (len <= 0) {
                Debug.LogError("[UIItem.SetData] no cfg, please check");
                return;
            }

            // Debug.Log($"[UIItem.SetData] idx: {index} len: {len}");
            this.index = index;
            this.titleTxt.text = title;

            this.mOrderType = orderType;
            this.mDatas = datas;
            this.mMinGrade = orderType == GameData.OrderType.OrderBy ? datas[0].grade.ToFloat() : datas[len - 1].grade.ToFloat();
            this.mMaxGrade = orderType == GameData.OrderType.OrderBy ? datas[len - 1].grade.ToFloat() : datas[0].grade.ToFloat();
        }

        // public void SetData(int index, string title, 
        //     float minGrade, float maxGrade,
        //     Func<float, string> onInput, Action<int> onImport, Action<int> onExport) {
        //
        //     this.index = index;
        //     this.titleTxt.text = title;
        //     this.mMinGrade = minGrade;
        //     this.mMaxGrade = maxGrade;
        //     this.onInputChange = onInput;
        //     this.onImport = onImport;
        //     this.onExport = onExport;
        // }
    }
}