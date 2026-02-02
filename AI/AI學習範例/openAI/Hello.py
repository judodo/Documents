import openai

# 1. 建立連線工具
client = openai.OpenAI(api_key="你的金鑰")

# 2. 定義 Prompt
system_instruction = "你是一位資深工程師，請用簡短的 JSON 格式回覆。"
task_content = "請解釋什麼是遞迴。"

# 3. 操作模型 (這是 Prompt 真正被「餵」進去的地方)
response = client.chat.completions.create(
    model="orion-preview",      # 指定要執行這段 Prompt 的大腦
    messages=[
        {"role": "system", "content": system_instruction}, # 注入指令
        {"role": "user", "content": task_content}           # 注入資料
    ]
)

# 4. 取得 AI 執行的結果
print(response.choices[0].message.content)
