using System.Text;

namespace Routers;

/// <summary>
/// Represents routers network
/// </summary>
public class RoutersNetwork
{
    private readonly List<Edge> _edges = new ();
    private readonly Heap<Edge> _availableEdges = new();
    private readonly int _verticesCount;
    
    private readonly List<Edge> _treeEdges = new();

    public List<Edge> TreeEdges => _treeEdges.Select(edge => new Edge(edge.Vertex0, edge.Vertex1, edge.Weight)).ToList();
    
    public RoutersNetwork(string inputPath)
    {
        var lines = File.ReadAllLines(inputPath);
        foreach (var line in lines)
        {
            var splitLine = line.Split();
            var vertex0 = int.Parse(splitLine[0][..^1]);
            var vertex1 = 0;
            foreach (var s in splitLine)
            {
                // если вершина
                if (int.TryParse(s, out var number))
                {
                    vertex1 = number;
                    if (number > _verticesCount)
                        _verticesCount = number;
                } // если вес ребра
                else if (s[0] == '(')
                {
                    var value = int.Parse(s[1..s.LastIndexOf(')')]);
                    _edges.Add(new Edge(vertex0, vertex1, weight: value));
                }
            }
        }
    }
    
    /// <summary>
    /// Writes optimal routers network in file
    /// </summary>
    public int BuildNetwork(string targetPath)
    {
        var processedVertices = new bool[_verticesCount + 1]; 
        var outputLines = new StringBuilder[_verticesCount];
        FillOutputPattern(outputLines);
        UpdateAvailableEdges(1, processedVertices);
        processedVertices[1] = true;
        for (var i = 0;  i < _verticesCount - 1; i++)
        {
            var newTreeEdge = _availableEdges.Extract();
            //  из _availableEdges нельзя просто так взять максимальное ребро, нужно еще раз проверить,
            // что 1 вершина помечена, а другая нет
            while (_availableEdges.Count > 0 && processedVertices[newTreeEdge.Vertex0] && processedVertices[newTreeEdge.Vertex1])
            {
                newTreeEdge = _availableEdges.Extract();
            }
            
            // если не нашлось подходящего ребра но мы все ещё в цикле, то граф был несвязный
            if (processedVertices[newTreeEdge.Vertex0] && processedVertices[newTreeEdge.Vertex1])
            {
                var errorWriter = Console.Error;
                errorWriter.Write("routers network was unconnected");
                return -1;
            }
            
            if (processedVertices[newTreeEdge.Vertex1]) // если вершина 1 помечена 
            {
                processedVertices[newTreeEdge.Vertex0] = true;
                UpdateAvailableEdges(newTreeEdge.Vertex0, processedVertices);
            }
            else // если вершина 0 помечена
            {
                processedVertices[newTreeEdge.Vertex1] = true;
                UpdateAvailableEdges(newTreeEdge.Vertex1, processedVertices);
            }
            
            _treeEdges.Add(newTreeEdge);
            outputLines[newTreeEdge.Vertex0 - 1].Append($" {newTreeEdge.Vertex1} ({newTreeEdge.Weight}),");
        }
        
        Print(targetPath, outputLines);
        return 0;
    }

    private void UpdateAvailableEdges(int vertex, bool[] processedVertices)
    {
        processedVertices[vertex] = true;
        foreach (var edge in _edges)
        {
            if (edge.Vertex0 == vertex && !processedVertices[edge.Vertex1] 
                || edge.Vertex1 == vertex && !processedVertices[edge.Vertex0])
            {
                _availableEdges.Insert(edge);
            }
        }
    }
    
    private void FillOutputPattern(StringBuilder[] lines)
    {
        for (var i = 0;  i < lines.Length; i++)
        {
            lines[i] = new StringBuilder();
            lines[i].Append($"{i + 1}:");
        }
    }

    private void Print(string targetPath, StringBuilder[] lines)
    {
        using var sw = new StreamWriter(targetPath, false);
        foreach (var line in lines)
        {
            if (line[^1] == ':')
            {
                continue;
            }
            if (line[^1] == ',')
            {
                sw.WriteLine(line.Remove(line.Length - 1, 1));

            }
            else
            {
                sw.WriteLine(line);
            }
        }
    }
}