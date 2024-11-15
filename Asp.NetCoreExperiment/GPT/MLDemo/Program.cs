using Microsoft.ML.Tokenizers;

var tokenizer = TiktokenTokenizer.CreateForModel("gpt-4o");
string inputText = """
角色：你是一位高级文字信息分析专家，有很强的理解能力，数据提取能力，分类能力，格式转换能力。
任务：
1、你能从User输入的内容中，提取模版中对应的数据项，并组装成Json字符串，只组装有值的数据项，格式为[{"index":1,"value":值1},{"index":2,"value":"值2"}]。
2、在组装Json信息时，要按照模版中每个数据项提供的的Prompt属性要求进行数据报取、转换、汇总。
要求：
1、请仔细分数用户数据，反复提取，找出更多的数据项内容。
2、直接输出的结果是纯Json字符串，不要以```json开始，不要以```结束。
------模版数据项-------
姓名（例：张三）(Index=0)，把姓名㔹换成全角カタカナ（例: チョウサン）(Index=1)，把姓名㔹换成半角カタカナ（例：ﾁｮｳｻﾝ）(Index=2)，把姓名㔹换成英語表記（例：ChoSan）(Index=3)，電話番号（例：030-1234-5678）(Index=4)，メールアドレス(Index=5)，生年月日(格式：2022-02-02)(Index=6)，郵便番号または、〒で始まる郵便番号、〒は不要です（例：111-0032）(Index=7)，住所(Index=8)，職業[無職:0,技術者:1,教師:2,医師:3,芸術家:4,料理人:5,商人:6](Index=9)，性別-男性(Index=10)，性別-女性(Index=11)，性別-その他(Index=12)，趣味-「スポーツ」通过给定内容筛选，有返回true，没有返回false(Index=13)，趣味-「音楽」通过给定内容筛选，有返回true，没有返回false(Index=14)，趣味-「芸術」通过给定内容筛选，有返回true，没有返回false(Index=15)，其他：汇总信息，80字以内(Index=16)，我叫桂素伟，男，1979年6月22号出生，家住山西省黎城县，邮编是0460000，我手机是07090657186，邮箱是axzxs2001@163.com。平时的运动项目不多，喜欢爬山，远足，现在是一名程序架构师。最近一段时间在学习，探索生成式AI方面的课题，想利用这项技术，为人们带来便利，同时也积极参与一些社区活动，分享自己生成AI学习，开发的一些心得。\r\n
""";



// 获取标记 ID 列表
var tokenIds = tokenizer.EncodeToIds(inputText);

// 获取标记列表
var tokens = tokenizer.EncodeToTokens(inputText,out string normalizedContent,true,true);
Console.WriteLine(normalizedContent);

var decodedText = tokenizer.Decode(tokenIds);

int tokenCount = tokenizer.CountTokens(inputText);


// 从文本开头截取指定数量的标记
int startIndex = tokenizer.GetIndexByTokenCount(inputText, 5, out string processedText, out _);
string truncatedFromStart = processedText?.Substring(0, startIndex);

// 从文本结尾截取指定数量的标记
int endIndex = tokenizer.GetIndexByTokenCountFromEnd(inputText, 5, out processedText, out _);
string truncatedFromEnd = processedText?.Substring(endIndex);
Console.ReadLine();