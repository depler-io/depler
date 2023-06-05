namespace Depler.Core.Graph;

public class Graph<TVertex, TEdge>
    where TVertex : IVertex
    where TEdge : IEdge<TVertex>
{
    private readonly Dictionary<Guid, IVertex> _vertices = new();

    public bool AddVertex(TVertex vertex)
    {
        if (_vertices.ContainsKey(vertex.Id))
        {
            return false;
        }
        
        _vertices.Add(vertex.Id, vertex);
        return true;
    }

    public bool AddEdge(TEdge edge)
    {
        return true;
    }

    public bool ContainsVertex(TVertex vertex)
    {
        return true;
    }
}
