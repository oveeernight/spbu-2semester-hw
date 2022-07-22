using System.Text;

namespace Routers;

/// <summary>
/// Represents routers network
/// </summary>
public class RoutersNetwork
{
    private readonly Dictionary<Edge, int> _edges = new ();
    private readonly Dictionary<Edge, int> _availableEdges = new();
    private readonly int _verticesCount;
    
    // я не придумал, как тестировать по-другому
    public readonly Dictionary<Edge, int> TreeEdges = new();
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
                    _edges.Add(new Edge(vertex0, vertex1), value);
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
        UpdateAvailableEdges(1);
        processedVertices[1] = true;
        for (var i = 0;  i < _verticesCount - 1; i++)
        {
            var newTreeEdge = new Edge();
            var maxValue = 0;
            foreach (var (key, value) in _availableEdges)
            {
                //  из _availableEdges нельзя просто так взять максимальное ребро, нужно еще раз проверить,
                // что 1 вершина помечена, а другая нет
                if (value > maxValue && (!processedVertices[key.Vertex0] || !processedVertices[key.Vertex1]))
                {
                    maxValue = value;
                    newTreeEdge = key;
                }
            }
            
            // если не нашлось подходящего ребра но мы все ещё в цикле, то граф был несвязный
            if (maxValue == 0)
            {
                var ew = Console.Error;
                ew.Write("routers network were unconnected");
                return -1;
            }

            _availableEdges.Remove(newTreeEdge);
            _edges.Remove(newTreeEdge);

            if (processedVertices[newTreeEdge.Vertex1]) // если вершина 1 помечена 
            {
                processedVertices[newTreeEdge.Vertex0] = true;
                UpdateAvailableEdges(newTreeEdge.Vertex0);
            }
            else // если вершина 0 помечена
            {
                processedVertices[newTreeEdge.Vertex1] = true;
                UpdateAvailableEdges(newTreeEdge.Vertex1);
            }
            
            TreeEdges.Add(newTreeEdge, maxValue);
            outputLines[newTreeEdge.Vertex0 - 1].Append($" {newTreeEdge.Vertex1} ({maxValue}),");
        }
        
        Print(targetPath, outputLines);
        return 0;
    }

    private void UpdateAvailableEdges(int vertex)
    {
        foreach (var edge in _edges)
        {
            if (!_availableEdges.ContainsKey(edge.Key) &&  (edge.Key.Vertex0 == vertex || edge.Key.Vertex1 == vertex))
            {
                _availableEdges.Add(edge.Key, edge.Value);
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
        using (var sw = new StreamWriter(targetPath, false))
        {
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
}