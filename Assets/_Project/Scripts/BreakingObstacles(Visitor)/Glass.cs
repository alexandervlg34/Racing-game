
    
public class Glass : Obstacle
{
    public override void Accept(IObstacleVisitor obstacleVisitor) => obstacleVisitor.Visit(this);

}
