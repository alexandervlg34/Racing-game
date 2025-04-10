using UnityEngine;
using System.Collections.Generic;
public class Score
{
    public int Value => _obstacleVisitor.Score;
    
    private ObstacleVisitor _obstacleVisitor;
    private List<Obstacle> _obstacles;
    public Score(List<Obstacle> obstacles)
    {
        _obstacles = obstacles; 
        foreach (var obstacle in _obstacles)
        {
            obstacle.Destroyed += OnObstacleDestroyed;
        }
        
        _obstacleVisitor = new ObstacleVisitor();
    }
    
    
    public void OnObstacleDestroyed(Obstacle obstacle)
    {
        obstacle.Accept(_obstacleVisitor);
        Debug.Log(Value);
    }
    
    private class ObstacleVisitor : IObstacleVisitor
    {
        public int Score { get; private set; }
        public void Visit(Glass glass) => Score += 10;
        public void Visit(Stone stone) => Score += 30;
        public void Visit(ExplodingObject explodingObject) => Score += 50;
    }
}


