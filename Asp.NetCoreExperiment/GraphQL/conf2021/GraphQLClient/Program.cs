
Console.WriteLine("Hello, World!");
var client = new HttpClient();
var content = new StringContent(@"{
  examPaper{
    title,
    totalScore,
    questionCount
  }
}");

//content.Headers.Clear();
//content.Headers.Add("Content-Type", "application/json");
var response = await client.PostAsync("http://localhost:5000/graphql/", content);
var backContent = await response.Content.ReadAsStringAsync();
Console.WriteLine(backContent);