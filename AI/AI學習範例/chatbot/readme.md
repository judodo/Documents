# 🤖 Gemini AI 具備記憶力與自動摘要的聊天機器人

這是一個基於 Google Gemini 2.5/3.0 系列模型開發的智能聊天機器人專案。它不僅能記憶對話紀錄，還具備了自動摘要功能，能有效節省 API Token 並維持長期的對話連貫性。

---

## 🌟 核心功能

* **持久化記憶**：使用 `JSON` 格式儲存對話，即使重啟程式 AI 依然記得你。
* **自動摘要壓縮**：當對話過長時，系統會自動呼叫 AI 進行關鍵資訊摘要，避免超過 Token 限制。
* **多模型支援**：相容於 `gemini-2.5-flash`、`gemini-3-flash-preview` 等最新模型。
* **環境安全**：透過 `.env` 檔案管理 API Key，確保私鑰不外洩。

## 🛠️ 技術棧

- <font color="#4285F4">**Google GenAI SDK**</font>: 官方最新版 API 調用工具。
- <font color="#34A853">**Python-dotenv**</font>: 環境變數管理。
- <font color="#FBBC05">**JSON Storage**</font>: 本地端資料持久化。

## 📦 安裝與設定

### 1. 複製專案與安裝套件
```bash
git clone [https://github.com/你的帳號/my_ai_chatbot.git](https://github.com/你的帳號/my_ai_chatbot.git)
cd my_ai_chatbot
pip install -U google-genai python-dotenv
