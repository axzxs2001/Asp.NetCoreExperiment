﻿
```Mermaid
flowchart
    A[Start] --> B[提取用户信息]
    B --> C{预订机票}
    C -- 是 --> D{预订酒店}
    C -- 否 --> E[发邮件提醒]
    
    D --> F{预订用车}
    D --> G[发邮件提醒]
    F -- 是 --> H[生成行程单]
    F -- 否 --> I[发邮件提醒]
    H --> I[订单完成]
    I --> J[End]
```


##
##
##









```Mermaid
flowchart
    A[Start] --> B[查询简历]
    B --> C{评测简历}
    C -- 是 --> D[匹配职位]
    C -- 否 --> E[发邮件提提出建议]
    D --> F[投递简历] 
```



