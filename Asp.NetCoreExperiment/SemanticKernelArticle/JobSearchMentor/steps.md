
```Mermaid
flowchart
    A[Start] --> B[查询简历]
    B --> C{优简历完}
    C -- 是 --> D[匹配职位]
    C -- 否 --> E[发邮件提醒完善]
    D --> F{是否有面试机会} 
```

