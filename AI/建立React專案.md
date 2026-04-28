3. Start Building a ReAct ProjectThe AGENTS.md file can be used to customize the behavior of the agent:Create a project folder and add an AGENTS.md file.Define rules in the file (this is the system prompt):
markdown
你是一個開發助手，請遵循 ReAct 模式：
1. Thought: 說明你的推論。
2. Action: 從可用工具中選擇（例如：shell, read, write）。
3. Observation: 根據工具回傳的結果進行分析。
不斷循環直到解決問題。

##我想要知道為什麼Gemini cli 寫完程式，反饋沒問題，執行時卻錯誤百出，這過程犯了哪個錯誤? 我需要如何指示才不會拿到一個不可以使用的程式碼


這是一個非常經典的 「幻覺 (Hallucination)」與「環境隔閡」 問題。雖然 AI 在文字層面覺得邏輯通順，
但因為它沒有真正「腳踏實地」地驗證環境，導致產出的程式碼與現實脫節。在 ReAct 循環中，
這通常是因為 Agent 犯了「跳過觀察 (Skipping Observation)」或「過度自信 (Over-reliance on internal knowledge)」的錯誤。
以下是常見錯誤分析以及如何下達指令的解決方案：

# 1. 過程中的核心錯誤
## 沒有執行驗證 (Dry Run only)： AI 寫完程式後，直接假設它會動，沒有執行「運行」或「測試」指令來獲取 Observation。
## 環境不匹配： AI 假設你已經安裝了某些函式庫（如 requests, pandas），但你的本機環境其實沒有。
## 路徑問題： AI 寫入檔案的路徑與它執行時的路徑不一致。
## 缺少錯誤處理： AI 產出的範例通常是「快樂路徑 (Happy Path)」，沒有考慮到網路斷線、檔案不存在等異常情況。

# 2. 如何指示才能拿到可用的程式碼？
若要 AI 產出可執行的程式，需要在指示中加入 「強制閉環」 與 「環境檢查」 的指令。建議使用以下結構：
## 策略一：強制執行與自癒 (Self-Healing)不要只要求它「寫程式」，
   要要求它「寫完並確保能跑通」。
指令範例：
  ````
  「請開發一個 [功能描述] 的 Python 腳本。在交付程式碼前，你必須使用 shell 工具執行該腳本，並觀察輸出結果。
  如果報錯，請根據錯誤訊息修正程式碼，直到腳本能成功運行並輸出預期結果為止。」
  ````
## 策略二：要求「測試先行」強迫 Agent 寫出測試案例，這是減少 Bug 最有效的方法。
指令範例：
````
「請先為 [功能] 撰寫測試計畫。在寫主程式前，先寫一個 test.py。接著開發主程式，並反覆執行 pytest 或 python test.py。只有當所有測試通過時，才算完成任務。」
````
## 策略三：環境依賴檢查解決「找不到模組」的問題。
指令範例：
````
「撰寫程式前，請先檢查目前環境中必要的套件是否已安裝。如果缺少，請主動執行 pip install。 請確保所有 import 的函式庫都能在本機執行。」

# 3. 進階技巧：設定 AGENTS.md (或系統提示)如果希望一勞永逸，可以在專案目錄下放一個 AGENTS.md 檔案，寫入以下規範：
```markdown
# 你的行為準則
1. 嚴禁在未執行驗證的情況下宣稱任務完成。
2. 每次修改代碼後，必須執行一次該檔案以確認語法正確。
3. 如果執行結果與預期不符，必須在 Thought 階段分析原因並修正。
4. 優先使用本機已存在的工具與環境。
```

請謹慎使用程式碼。關鍵觀念：將 AI 視為「實習生」當發現程式碼無法使用時，不要直接修改程式碼，
而是要告訴它：「這段程式碼在執行時報錯了 [貼上報錯內容]，請重新進入 ReAct 循環，先診斷原因再修正。」 
這樣 AI 才會學會利用 Observation 來修正錯誤。



請謹慎使用程式碼。Perform tasks: Execute gemini "開發一個能抓取天氣的 Python 腳本" 
(which translates to "Develop a Python script that can fetch the weather") in the directory. 
Gemini will autonomously invoke the terminal to install the requests library, 
write code, and perform testing according to the defined ReAct logic.4. 
Advanced Learning ResourcesTool list: Refer to the Gemini CLI Tools official documentation to understand how to expand the agent's capabilities, such as run_shell_command or web_fetch.Build from scratch: If you want to write ReAct logic by hand using Python, refer to the LangGraph tutorial in Google AI Studio to learn how to create nodes and state machines.
