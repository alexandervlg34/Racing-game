
    
public class ExplodingObject : Obstacle
{
    public override void Accept(IObstacleVisitor obstacleVisitor) => obstacleVisitor.Visit(this);
}
