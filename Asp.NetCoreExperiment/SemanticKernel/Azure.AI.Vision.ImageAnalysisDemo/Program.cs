using Azure.AI.Vision.ImageAnalysis;

var client = new ImageAnalysisClient(new Uri(""), new Azure.AzureKeyCredential(""));
client.Analyze();
