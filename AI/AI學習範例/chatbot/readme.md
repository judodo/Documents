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

### 2. 設定環境變數

在專案根目錄建立 .env 檔案，內容如下：
```bash
GOOGLE_API_KEY=你的_GEMINI_API_KEY

## 2. 執行程式

直接執行主程式即可進入對話模式：
```bash
python main_record.py


## 3.核心程式碼解析
自動摘要邏輯

為了優化長對話的效能，我們實作了以下壓縮機制：
```bash
def summarize_history(chat_history):
    # 當對話超過 20 則時，請 AI 進行總結
    prompt = "請將以下對話紀錄縮減為 200 字以內的摘要..."
    # ... 呼叫 API 進行摘要 ...
    return response.text

### 4.安全存檔機制

我們手動將 SDK 物件序列化為 JSON，解決 UserContent is not JSON serializable 的問題：

```bash
def save_history(chat_history):
    serializable_history = []
    for content in chat_history:
        # 提取角色與純文字內容
        role = content.role
        parts = [{"text": p.text} for p in content.parts if hasattr(p, 'text')]
        serializable_history.append({"role": role, "parts": parts})
    # ... 寫入檔案 ...


常見問題解決 (Troubleshooting)

    429 Resource Exhausted: 代表 API 頻率達到上限。程式已加入等待機制，或者請嘗試更換 gemini-flash-latest 模型。

    404 Not Found: 請確認您的 API Key 權限是否包含該模型名稱。

    💡 小撇步：輸入 exit 或 quit 離開程式時，系統會自動完成最後一次的存檔。
