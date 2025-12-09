using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

// 腳本名稱：VoskDialogText
public class VoskDialogText : MonoBehaviour 
{
    // 公開欄位：在 Unity 編輯器中連結 Vosk 語音識別組件和 UI 文本
    public VoskSpeechToText VoskSpeechToText;
    public Text DialogText;

    // --- 關鍵字偵測 (Regex 替換為中文關鍵字) ---
    // 請根據您的語音模型和使用者的發音，調整這些正規表達式中的中文詞彙
    
    // 一般對話
    Regex hi_regex = new Regex(@"(你好|哈囉|您好)");
    Regex who_regex = new Regex(@"(你是誰|介紹自己)");
    Regex pass_regex = new Regex(@"(好的|開始|確定|進行)");
    Regex help_regex = new Regex(@"(幫我|求助|給提示)");

    // 帶動物/物品向前 (從左岸到右岸)
    Regex goat_regex = new Regex(@"(山羊|帶山羊|開始山羊)");
    Regex wolf_regex = new Regex(@"(狼|帶狼|帶走狼)");
    Regex cabbage_regex = new Regex(@"(高麗菜|帶高麗菜|開始高麗菜)");

    // 帶動物/物品返回 (從右岸到左岸)
    Regex goat_back_regex = new Regex(@"(山羊回去|帶回山羊|山羊回來)");
    Regex wolf_back_regex = new Regex(@"(狼回去|帶回狼|狼回來)");
    Regex cabbage_back_regex = new Regex(@"(高麗菜回去|帶回高麗菜|高麗菜回來)");

    // 農夫空船移動
    Regex forward_regex = new Regex(@"(過去|向前|前進)");
    Regex back_regex = new Regex(@"(回來|返回|往後)");

    // --- 謎題狀態 (State) ---
    // True = 在左岸 (起始點), False = 在右岸 (目的地)
    bool goat_left;     // 山羊在左岸
    bool wolf_left;     // 狼在左岸
    bool cabbage_left;  // 高麗菜在左岸
    bool man_left;      // 農夫在左岸

    void Awake()
    {
        VoskSpeechToText.OnTranscriptionResult += OnTranscriptionResult;
        ResetState();
    }

    void ResetState()
    {
        // 重置所有角色到左岸
        goat_left = true;
        wolf_left = true;
        cabbage_left = true;
        man_left = true;
    }

    // 檢查當前狀態是否導致失敗或成功
    void CheckState() {
        // 檢查失敗條件 (農夫不在時，狼/羊或羊/菜獨處)
        
        // 情況一：農夫在右岸 (!man_left)
        if (man_left == false) {
            // 狼和山羊在左岸
            if (goat_left && wolf_left) {
                AddFinalResponse("狼吃了山羊，請重新開始！");
                return;
            }
            // 山羊和高麗菜在左岸
            if (goat_left && cabbage_left) {
                AddFinalResponse("山羊吃了高麗菜，請重新開始！");
                return;
            }
        }

        // 情況二：農夫在左岸 (man_left)
        if (man_left == true) {
            // 狼和山羊在右岸 (!goat_left && !wolf_left)
            if (!goat_left && !wolf_left) {
                AddFinalResponse("狼吃了山羊，請重新開始！");
                return;
            }
            // 山羊和高麗菜在右岸 (!goat_left && !cabbage_left)
            if (!goat_left && !cabbage_left) {
                AddFinalResponse("山羊吃了高麗菜，請重新開始！");
                return;
            }
        }
        
        // 檢查勝利條件 (所有角色都在右岸)
        if (!goat_left && !wolf_left && !cabbage_left && !man_left) {
            AddFinalResponse("恭喜！你成功了，我們再玩一次吧！");
            return;
        }

        // 如果未失敗也未成功，提示下一步
        AddResponse("好的，那下一步要怎麼走？");
    }

    // 語音輸出函式 (請注意，這通常是針對特定系統的，例如 macOS 的 /usr/bin/say)
    void Say(string response)
    {
        // System.Diagnostics.Process.Start("/usr/bin/say", response); 
    }

    // 顯示最終結果並重置遊戲
    void AddFinalResponse(string response) {
        // Say(response);
        DialogText.text = response + "\n";
        ResetState();
    }

    // 顯示當前狀態並詢問下一步
    void AddResponse(string response) {
        // Say(response);
        DialogText.text = response + "\n\n";

        // 顯示當前所有角色的位置
        DialogText.text += "農夫 " + (man_left ? "在左岸" : "在右岸") + "\n";
        DialogText.text += "狼 " + (wolf_left ? "在左岸" : "在右岸") + "\n";
        DialogText.text += "山羊 " + (goat_left ? "在左岸" : "在右岸") + "\n";
        DialogText.text += "高麗菜 " + (cabbage_left ? "在左岸" : "在右岸") + "\n";

        DialogText.text += "\n";
    }

    // 處理 Vosk 語音識別結果
    private void OnTranscriptionResult(string obj)
    {
        // Save to file (儲存到檔案)
        
        Debug.Log(obj);
        // 假設 RecognitionResult 和 RecognizedPhrase 類別來自 Vosk 函式庫
        var result = new RecognitionResult(obj); 
        foreach (RecognizedPhrase p in result.Phrases)
        {
            // --- 處理一般對話 ---
            if (hi_regex.IsMatch(p.Text))
            {
                AddResponse("你好！");
                return;
            }
            if (who_regex.IsMatch(p.Text))
            {
                AddResponse("我是機器人老師");
                return;
            }
            if (pass_regex.IsMatch(p.Text))
            {
                AddResponse("太棒了！");
                return;
            }
            if (help_regex.IsMatch(p.Text))
            {
                AddResponse("請自己思考");
                return;
            }
            
            // --- 處理帶回 (從右岸到左岸) ---
            if (goat_back_regex.IsMatch(p.Text)) {
                if (goat_left == true) {
                    AddResponse("山羊已經在左岸了");
                } else if (man_left == true) {
                    AddResponse("農夫還在左岸，無法從右岸帶東西回來");
                } else {
                    goat_left = true; // 山羊回到左岸
                    man_left = true; // 農夫回到左岸
                    CheckState();
                }
                return;
            }

            if (wolf_back_regex.IsMatch(p.Text)) {
                if (wolf_left == true) {
                    AddResponse("狼已經在左岸了");
                } else if (man_left == true) {
                    AddResponse("農夫還在左岸，無法從右岸帶東西回來");
                } else {
                    wolf_left = true; // 狼回到左岸
                    man_left = true; // 農夫回到左岸
                    CheckState();
                }
                return;
            }

            if (cabbage_back_regex.IsMatch(p.Text)) {
                if (cabbage_left == true) {
                    AddResponse("高麗菜已經在左岸了");
                } else if (man_left == true) {
                    AddResponse("農夫還在左岸，無法從右岸帶東西回來");
                } else {
                    cabbage_left = true; // 高麗菜回到左岸
                    man_left = true; // 農夫回到左岸
                    CheckState();
                }
                return;
            }
            
            // --- 處理帶走 (從左岸到右岸) ---
            if (goat_regex.IsMatch(p.Text)) {
                if (goat_left == false) {
                    AddResponse("山羊已經在右岸了");
                } else if (man_left == false) {
                    AddResponse("農夫已經在右岸了，無法從左岸帶東西過去");
                } else {
                    goat_left = false; // 山羊到右岸
                    man_left = false; // 農夫到右岸
                    CheckState();
                }
                return;
            }

            if (wolf_regex.IsMatch(p.Text)) {
                if (wolf_left == false) {
                    AddResponse("狼已經在右岸了");
                } else if (man_left == false) {
                    AddResponse("農夫已經在右岸了，無法從左岸帶東西過去");
                } else {
                    wolf_left = false; // 狼到右岸
                    man_left = false; // 農夫到右岸
                    CheckState();
                }
                return;
            }

            if (cabbage_regex.IsMatch(p.Text)) {
                if (cabbage_left == false) {
                    AddResponse("高麗菜已經在右岸了");
                } else if (man_left == false) {
                    AddResponse("農夫已經在右岸了，無法從左岸帶東西過去");
                } else {
                    cabbage_left = false; // 高麗菜到右岸
                    man_left = false; // 農夫到右岸
                    CheckState();
                }
                return;
            }
            
            // --- 處理空船移動 ---
            if (forward_regex.IsMatch(p.Text)) {
                if (man_left == false) {
                    AddResponse("農夫已經在右岸了");
                } else {
                    man_left = false; // 農夫空船到右岸
                    CheckState();
                }
                return;
            }
            
            if (back_regex.IsMatch(p.Text)) {
                if (man_left == true) {
                    AddResponse("農夫還在左岸");
                } else {
                    man_left = true; // 農夫空船回到左岸
                    CheckState();
                }
                return;
            }
        }
        // 如果沒有匹配到任何短語，且有語音輸入
        if (result.Phrases.Length > 0 && result.Phrases[0].Text != "") {
            AddResponse("我聽不懂你在說什麼");
        }
    }
}