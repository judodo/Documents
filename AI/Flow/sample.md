# 簡易流程圖範例

這是一個使用 Mermaid 語法製作的標準流程圖。

## 專案審核流程

```mermaid
graph TD
    A[開始] --> B(提交專案提案)
    B --> C{主管審核?}
    C -->|通過| D[進入開發階段]
    C -->|退回| E[修改提案]
    E --> B
    D --> F[測試與驗收]
    F --> G{測試通過?}
    G -->|是| H[專案上線]
    G -->|否| I[修復 Bug]
    I --> F
    H --> J((結束))

    %% 樣式設定 (可選)
    style A fill:#f9f,stroke:#333,stroke-width:2px
    style J fill:#f9f,stroke:#333,stroke-width:2px
    style C fill:#ff9,stroke:#333,stroke-width:2px
    style G fill:#ff9,stroke:#333,stroke-width:2px
