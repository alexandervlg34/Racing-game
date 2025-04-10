
    
public interface IObstacleVisitor
{
    void Visit(Glass glass);
    void Visit(Stone stone);
    void Visit(ExplodingObject explodingObject);
}
