
using System.Text.RegularExpressions;


Console.WriteLine("Graphql数据：");
var client = new HttpClient();
var content = new StringContent(@"{""query"":""{examPaper{ title,totalScore,questionCount}}""}");

content.Headers.Clear();
content.Headers.Add("Content-Type", "application/json;charset=utf-8");
var response = await client.PostAsync("http://localhost:5000/graphql/", content);

var backContent= await response.Content.ReadAsStringAsync();

Console.WriteLine(Regex.Unescape(backContent));


