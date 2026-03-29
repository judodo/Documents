🛠️ MyAiTool：Gemini 協作開發規則 (v1.0)
🔴 第一準則：功能保全 (Feature Preservation)

在提供任何新代碼之前，Gemini 必須確保以下核心邏輯不被刪除或修改（除非 Jason 明確要求）：

    路徑變數化：統一使用 app.tasks_file (對接 download_tasks.json)。

    執行緒安全：UI 更新必須包裹在 root.after(0, _render) 中。

    時間格式化：影片秒數須透過 timedelta 轉為 HH:MM:SS。

    右鍵功能集：必須包含「開啟連結」、「刪除/移除任務」以及「重新執行」。

    日誌連動：所有錯誤必須同步寫入 Logs/error.log。

🟠 第二準則：溝通與更新模式 (Communication Protocol)

為了避免 Jason「全量貼上」導致的風險，Gemini 須遵循以下輸出格式：

    變更摘要 (Change Log)：在給出代碼前，先列出：

        ✅ 新增：(例如：新增了置頂過濾邏輯)

        ⚠️ 修改：(例如：修改了 update_ui_components 的 for 迴圈)

        🚫 保留：(例如：已確認保留右鍵選單與時間格式化)

    局部代碼優先：若只需修改單一函數（Function），僅提供該函數代碼，不要提供整個檔案。

    詳盡注釋：新代碼必須附帶中文注釋，解釋關鍵邏輯（如：# 這裡進行過濾，只留 pending）。

🟡 第三準則：主畫面渲染邏輯 (Dashboard Rules)

    嚴格遮蔽：主畫面 Treeview 僅允許顯示 status == 'pending' 的任務。

    全局感：頂部統計標籤必須掃描 download_tasks.json 全表，顯示正確的總數。

    倒序顯示：新任務須出現在列表最上方 (reversed)。

🟢 第四準則：異常處理與診斷 (Diagnostic Rules)

    獨立診斷：所有 error 或 oversized 任務交由 failed_viewer.py 處理，不在主畫面佔位。

    一鍵開啟日誌：診斷視窗必須具備 📂 打開 Logs 資料夾 的快捷按鈕。
