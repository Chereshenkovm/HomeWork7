using Game;
using NUnit.Framework;
using UnityEngine;

public class GetBotActionTest
{
    [Test]
    public void GetBotActionTestPlayer()
    {
        float playerDist = 8f;
        float rockDist = 10f;
        bool foundPlayer = true;
        bool rocksExist = true;
        var botDiff = BotDifficulty.HARD;
        bool reachable = true;
        bool hasRock = false;

        var ExpectedAction = ActionName.PLAYER;

        var action = ZombieComponent.getActionName(playerDist,
            rockDist,
            foundPlayer,
            rocksExist,
            botDiff,
            reachable,
            hasRock
            );
        
        Assert.That(action, Is.EqualTo(ExpectedAction));
    }
    
    [Test]
    public void GetBotActionTestRock()
    {
        float playerDist = 20f;
        float rockDist = 10f;
        bool foundPlayer = true;
        bool rocksExist = true;
        var botDiff = BotDifficulty.HARD;
        bool reachable = true;
        bool hasRock = false;

        var ExpectedAction = ActionName.ROCK;

        var action = ZombieComponent.getActionName(playerDist,
            rockDist,
            foundPlayer,
            rocksExist,
            botDiff,
            reachable,
            hasRock
        );
        
        Assert.That(action, Is.EqualTo(ExpectedAction));
    }
    
    [Test]
    public void GetBotActionTestThrowRock()
    {
        float playerDist = 4f;
        float rockDist = 2f;
        bool foundPlayer = true;
        bool rocksExist = true;
        var botDiff = BotDifficulty.HARD;
        bool reachable = true;
        bool hasRock = true;

        var ExpectedAction = ActionName.THROWROCK;

        var action = ZombieComponent.getActionName(playerDist,
            rockDist,
            foundPlayer,
            rocksExist,
            botDiff,
            reachable,
            hasRock
        );
        
        Assert.That(action, Is.EqualTo(ExpectedAction));
    }
    
    [Test]
    public void GetBotActionTestTakeRock()
    {
        float playerDist = 5f;
        float rockDist = 0.5f;
        bool foundPlayer = true;
        bool rocksExist = true;
        var botDiff = BotDifficulty.HARD;
        bool reachable = true;
        bool hasRock = false;

        var ExpectedAction = ActionName.PICKROCK;

        var action = ZombieComponent.getActionName(playerDist,
            rockDist,
            foundPlayer,
            rocksExist,
            botDiff,
            reachable,
            hasRock
        );
        
        Assert.That(action, Is.EqualTo(ExpectedAction));
    }
    
    [Test]
    public void GetBotActionTestHitPlayer()
    {
        float playerDist = 0.9f;
        float rockDist = 10f;
        bool foundPlayer = true;
        bool rocksExist = true;
        var botDiff = BotDifficulty.HARD;
        bool reachable = true;
        bool hasRock = false;

        var ExpectedAction = ActionName.HITPLAYER;

        var action = ZombieComponent.getActionName(playerDist,
            rockDist,
            foundPlayer,
            rocksExist,
            botDiff,
            reachable,
            hasRock
        );
        
        Assert.That(action, Is.EqualTo(ExpectedAction));
    }
}
