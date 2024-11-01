using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class Builder : IDesignPattern
    {
        public void ExecuteDesignPattern()
        {
            var gameInfo1 = new GameInfoBuilder().WithIsOver(true)
                .WithWinner("Player 1")
                .WithhasFork(true)
                .WithNumberOfMoves(10)
                .build();
            var gameInfo2 = new GameInfoBuilder().WithIsOver(true)
                .WithWinner("Player 1")
                .WithhasFork(true)
                .build();
            var gameInfo3 = new GameInfoBuilder().WithIsOver(true)
                .WithhasFork(false)
                .WithNumberOfMoves(10)
                .build();
            Console.WriteLine("GameInfo1: " + gameInfo1.ToString());
            Console.WriteLine("GameInfo1: " + gameInfo2.ToString());
            Console.WriteLine("GameInfo1: " + gameInfo3.ToString());
        }

        public class GameInfo
        {
            private bool isOver;
            private String winner;
            private bool hasFork;
            private int numberOfMoves;

            public GameInfo(bool isOver, String winner, bool hasFork, int numberOfMoves)
            {
                this.isOver = isOver;
                this.winner = winner;
                this.hasFork = hasFork;
                this.numberOfMoves = numberOfMoves;
            }
            public override string ToString()
            {
                return $"isOver: {isOver}, winner:{winner}, hasFork:{hasFork}, numberOfMoves:{numberOfMoves}";
            }
        }
        public class GameInfoBuilder
        {
            private bool isOver;
            private String winner;
            private bool hasFork;
            private int numberOfMoves;

            public GameInfoBuilder()
            {
                // Default values
                isOver = false;
                winner = null;
                hasFork = false;
                numberOfMoves = 0;
            }

            public GameInfoBuilder WithIsOver(bool isOver)
            {
                this.isOver = isOver;
                return this;
            }

            public GameInfoBuilder WithWinner(String winner)
            {
                this.winner = winner;
                return this;
            }

            public GameInfoBuilder WithhasFork(bool hasFork)
            {
                this.hasFork = hasFork;
                return this;
            }

            public GameInfoBuilder WithNumberOfMoves(int numberOfMoves)
            {
                this.numberOfMoves = numberOfMoves;
                return this;
            }

            public GameInfo build()
            {
                return new GameInfo(isOver, winner, hasFork, numberOfMoves);
            }
        }
    }
}
