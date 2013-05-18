MoeDictNet
==========

####教育部國語辭典JSON檔解析及應用(.NET版)

解析 [dict-revised.json](https://github.com/g0v/moedict-data) 轉換為.NET物件，
原造字部分配合Unicode轉碼表 [sym.txt](https://github.com/g0v/moedict-epub) 統一轉為Unicode，
程式讀入 dict-revised.json 及 sym.txt，透過JSON.NET解析轉為.NET物件並序列化保存，
最後提供簡單應用範例，展示以LINQ方式查詢及顯示辭典內容。

![Screenshot](https://raw.github.com/darkthread/MoeDictNet/master/MoeDictJsonConvExample.gif)
