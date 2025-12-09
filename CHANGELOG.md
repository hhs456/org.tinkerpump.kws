# Changelog

## [1.0.0] - 2025-12-10

### 💥 Breaking Changes (強制性變更)

- **專案結構重組為 UPM Package 格式。**
  - **影響：** 不再以標準 Unity Project 形式提供。整合方式必須從手動複製檔案變更為透過 Unity Package Manager (UPM) 的 Git URL 方式加入專案。
  - **優點：** 大幅提升專案的模組化和跨專案整合效率，更容易進行版本管理。
  
### ✨ Added (新增)

- 新增 `package.json` 文件，正式定義 Package 資訊。
- 新增對 UPM 整合的支持，允許專案使用 Git URL 輕鬆引入 Vosk。

### ♻️ Changed (重構/變更)

- **[Refactor]** 將所有 Vosk 相關腳本和資源移至標準 UPM `Runtime` 目錄結構下。
- **[Refactor]** 清除並重置 Git 歷史，以提供一個乾淨的、專注於 Package 開發的新歷史線。

### 📄 Attribution (版權聲明)

- 此 Package 是基於 [原始 Vosk for Unity 專案連結] 的重構版本。
- 程式碼遵循原始專案的 [Apache License 2.0 / MIT License]。請參閱根目錄下的 `LICENSE` 文件。
