using System.Text.RegularExpressions;

const string DEFAULT_URI = "http://localhost:5000";
const string DEFAULT_WORD = "hello";


Console.WriteLine($"Which web page should we analyze ? (default : {DEFAULT_URI})");
string? userUri = Console.ReadLine();
var uri = new Uri(String.IsNullOrEmpty(userUri) ? DEFAULT_URI : userUri);

Console.WriteLine($"Which word should we count ? (default: {DEFAULT_WORD})");
string userWord = Console.ReadLine() ?? DEFAULT_WORD;
string word = String.IsNullOrEmpty(userWord) ? DEFAULT_WORD : userWord;


using HttpClient httpClient = new();
using HttpResponseMessage response = await httpClient.GetAsync(uri);
string resourceContent = await response.Content.ReadAsStringAsync();

var regex = new Regex($"\\b{word}\\b", RegexOptions.IgnoreCase);
int occurrencesCount = regex.Count(resourceContent);

Console.WriteLine($"We found {occurrencesCount} occurrences of the word {word}");