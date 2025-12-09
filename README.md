# 📦 Vosk Unity Package (UPM Refactor)

[![License: MIT/Apache 2.0](https://img.shields.io/badge/License-MIT%2FApache%202.0-blue.svg)](LICENSE)
[![Unity Version](https://img.shields.io/badge/Unity-2022.3%2B-555555.svg)](https://unity3d.com/)
---

## 💡 專案簡介

本專案是 Vosk 語音辨識引擎的 Unity C# 整合包，經全面重構，現以 **Unity Package Manager (UPM)** 格式發佈，旨在提供更便捷、模組化的跨專案整合體驗。

主要功能：
* **UPM 支援：** 透過 Git URL 輕鬆導入 Unity 專案。
* **跨平台支援：** Windows, macOS, Android, iOS。
* 高效的即時語音辨識。

## ⚠️ 來源與版權聲明 (Attribution & License)

**本 Package 是基於 vosk-unity-asr (https://github.com/alphacep/vosk-unity-asr) 的重構版本。**

我們已將其結構從傳統的 Unity Project 轉換為標準的 UPM Package 格式。

* **原始授權：** 本專案程式碼繼承原始專案的 **[Apache License 2.0 / MIT License]**。
* **義務：** 請參閱根目錄下的 `LICENSE` 檔案，以了解完整的版權和使用條款。

---

## 📦 安裝指南 (Installation)

本 Package 僅支援透過 Unity Package Manager (UPM) 的 Git URL 方式導入。

### 步驟 1：開啟 Package Manager

在您的 Unity 專案中，前往 **Window > Package Manager**。

### 步驟 2：使用 Git URL 導入

1.  點擊左上角的 **`+`** 按鈕。
2.  選擇 **`Add package from git URL...`**。
3.  貼上本 Package 的 Git URL：

    ```
    https://github.com/hhs456/org.tinkerpump.kws.git
    ```

---

## 🧠 語言模型設置

由於語言模型檔案較大，本 Package **不包含** Vosk 語言模型。您需要：

1.  從 [Vosk Model 官網連結] 下載您需要的語言模型（例如：`vosk-model-cn-0.22`）。
2.  將下載並解壓縮後的模型資料夾**完整**放入您的 Unity 專案的 `Assets` 資料夾內。
3.  在 Unity 編輯器中，確保模型資料夾的路徑是正確的。

---