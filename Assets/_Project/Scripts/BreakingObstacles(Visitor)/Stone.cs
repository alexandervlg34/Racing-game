
    
public class Stone : Obstacle
{
    public override void Accept(IObstacleVisitor obstacleVisitor) => obstacleVisitor.Visit(this);
}
