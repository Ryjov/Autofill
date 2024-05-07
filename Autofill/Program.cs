using System.Text;

var path = "../../../TestData/test.in";
string text;
using (FileStream fs = File.OpenRead(path))
{
    byte[] textBytes = new byte[fs.Length];
    await fs.ReadAsync(textBytes, 0, textBytes.Length);
    text = Encoding.Default.GetString(textBytes);
}

var lines = text.Split("\r\n").ToList();

int amountOfWords = int.Parse(lines[0]);

Dictionary<string, int> wordsAndCounts = new Dictionary<string, int>();

int i;

for (i = 1; i <= amountOfWords; i++)
{
    var wordAndCount = lines[i].Split(" ");
    wordsAndCounts.Add(wordAndCount[0], int.Parse(wordAndCount[1]));
}

wordsAndCounts = wordsAndCounts.OrderByDescending(word => word.Value).ToDictionary();

int amountOfUserInputs = int.Parse(lines[i]);
i++;
var userInputs = lines.GetRange(i, amountOfUserInputs);

using (FileStream fstream = new FileStream("../../../TestData/result.txt", FileMode.OpenOrCreate))
{
    foreach (var userInput in userInputs)
    {
        // var matches = new List<string>();
        // matches.Add(wordsAndCounts.FirstOrDefault(word => word.Key.StartsWith(userInput)).Key);

        var matches = wordsAndCounts.Where(word => word.Key.StartsWith(userInput)).Take(10).Select(word => word.Key).ToList();

        byte[] buffer = Encoding.Default.GetBytes("\r\n" + userInput + "\r\n    " + string.Join("\r\n    ", matches) +"\r\n");
        await fstream.WriteAsync(buffer, 0, buffer.Length);
    }
}