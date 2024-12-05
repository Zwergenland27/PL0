using Pl0;

var filePath = args[0];

var parser = new Parser(filePath);
bool result = parser.Start();
Console.WriteLine(result);