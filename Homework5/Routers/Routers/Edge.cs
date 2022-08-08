namespace Routers;

/// <summary>
/// Represents an edge of an undirected graph
/// </summary>
public struct Edge : IComparable<Edge>
{
    public int Vertex0 { get; }= 0;
    public int Vertex1 { get; } = 0;
    public int Weight { get; } = 0;

    public Edge(int vertex0, int vertex1, int weight)
    {
        Vertex0 = vertex0;
        Vertex1 = vertex1;
        Weight = weight;
    }

    public Edge()
    {
    }

    public int CompareTo(Edge other) => Weight.CompareTo(other.Weight);
    
}