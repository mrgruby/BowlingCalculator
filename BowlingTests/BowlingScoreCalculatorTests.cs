using Bowling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BowlingTests
{
    public class BowlingScoreCalculatorTests
    {
        //https://www.bowlinggenius.com/
        [Fact]
        public void CalculateScore_AllStrikes_Returns300Points()
        {
            int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            var score = BowlingCalculator.CalculateScore(rolls);
            Assert.Equal(300, score.Item1);
        }

        [Fact]
        public void CalculateScore_AllSpares_Returns150Points()
        {
            int[] rolls = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
            var score = BowlingCalculator.CalculateScore(rolls);
            Assert.Equal(150, score.Item1);
        }

        [Fact]
        public void CalculateScore_SpareInLastFrame_ReturnsCorrectScore()
        {
            int[] rolls = { 9, 1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 9, 1, 10 };
            var score = BowlingCalculator.CalculateScore(rolls);
            Assert.Equal(279, score.Item1);
        }

        [Fact]
        public void CalculateScore_StrikeInLastFrame_ReturnsCorrectScorePoints()
        {
            int[] rolls = { 10, 9, 1, 10, 9, 1, 10, 9, 1, 10, 9, 1, 10, 9, 1, 10 };
            var score = BowlingCalculator.CalculateScore(rolls);
            Assert.Equal(200, score.Item1);
        }

        [Fact]
        public void CalculateScore_RandomScores_ReturnsCorrectScore()
        {
            int[] rolls = BowlingCalculator.ConvertToIntArray("[7,1], [0,0], [6,2], [0,5], [4,4], [4,0], [7,2], [3,2], [5,5], [1,6]");
            var score = BowlingCalculator.CalculateScore(rolls);
            Assert.Equal(65, score.Item1);
        }

        [Fact]
        public void CalculateScore_RandomScores_ReturnsCorrectScore2()
        {
            int[] rolls = BowlingCalculator.ConvertToIntArray("[2,7], [1,5], [7,2], [9,0], [3,4], [4,4], [9,0], [8,1], [6,4], [6,4,2]");
            var score = BowlingCalculator.CalculateScore(rolls);
            Assert.Equal(94, score.Item1);
        }
    }
}
