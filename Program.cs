string input;

string safeWordsString = "and but or if because while since for to of in on at with by from about as among before after during throughout within without near next to alongside onto off out about off and away this that there here now";
List<string> safeWordsBase = safeWordsString.Split(' ').ToList();
List<string> punctuation = new List<string>(){".", ",", "!"};
string helpMessage = "Usage: application [input file] [safe words file (optional)] [output file (optional)]";

if (args.Length > 0)
{
    if (args[0] == "help")
    {
        System.Console.WriteLine(helpMessage);
        return;
    }
    input = File.ReadAllText(args[0]);
}
else
{
    System.Console.WriteLine("Need input file");
    System.Console.WriteLine(helpMessage);
    return;
}

if (args.Length >= 2)
{
    safeWordsBase = File.ReadAllText(args[1]).Split(' ').ToList<string>();
}

HashSet<string> safeWordsAll = new HashSet<string>();

foreach (string word in safeWordsBase)
{
    string trimmedWord = word.Trim();
    safeWordsAll.Add(trimmedWord);
    foreach(string character in punctuation)
    {
        safeWordsAll.Add(trimmedWord + character);
    }
}

string[] inputArray = input.Split(' ');
string output = "";

foreach (string word in inputArray)
{
    string trimmedWord = word.Trim();
    string modifiedWord = trimmedWord;

    if (!safeWordsAll.Contains(trimmedWord) && trimmedWord.Length > 0)
    {
        int index = (trimmedWord.Length > 2) ? 2 : 1;
        string firstHalf = trimmedWord.Substring(0, index);
        string secondHalf = "/" + trimmedWord.Substring(index);
        modifiedWord = firstHalf + secondHalf;
    }

    if (modifiedWord.Length > 0) output += modifiedWord + " ";
    System.Console.WriteLine(modifiedWord);
}

string outputPath = "output.txt";
if (args.Length >= 3) outputPath = args[2];

File.WriteAllText(outputPath, output);
