namespace Routers;

/// <summary>
/// Represents an edge of an undirected graph
/// </summary>
public struct Edge
{
    public int Vertex0;
    public int Vertex1;

    public Edge(int vertex0, int vertex1)
    {
        Vertex0 = vertex0;
        Vertex1 = vertex1;
    }

    public Edge()
    {
        Vertex0 = 0;
        Vertex1 = 0;
    }
}