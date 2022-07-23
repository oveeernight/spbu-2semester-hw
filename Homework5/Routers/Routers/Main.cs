using Routers;

Console.WriteLine("type path with origin network");
var inputPath = Console.ReadLine();
Console.WriteLine("type path to which you want to display the optimal network");
var outputPath = Console.ReadLine();
if (inputPath != null && outputPath != null)
{
    var routersNetwork = new RoutersNetwork(inputPath);
    routersNetwork.BuildNetwork(targetPath: outputPath);
}