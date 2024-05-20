using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class BowlingCalculator
    {
        public static void RunBowlingCalculator(string input)
        {
            // Test scores
            int[] finalRoundRollPins = ConvertToIntArray(input);

            var roundNumber = 1;
            var score = CalculateScore(finalRoundRollPins);

            //Print awesome UI!
            Console.WriteLine("==== Round Scores ====");
            foreach(var round in score.Item2)
            {
                Console.WriteLine($"   Round {roundNumber} score: {round}");
                roundNumber++;
            }
            Console.WriteLine("======================");
            Console.WriteLine("                      ");
            Console.WriteLine("==== Total Score =====");
            Console.WriteLine($"   Total score: {score.Item1}");
            Console.WriteLine("                      ");
            Console.WriteLine("======================");
            Console.ReadLine();
        }

        /// <summary>
        /// Takes a line of tests scores from the test data sheet in its original form, converts it into an int array, 
        /// which is then used in the score calculator.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>An integer array with the individual roundscores</returns>
        public static int[] ConvertToIntArray(string input)
        {
            var numbers = input
                .Replace("[", "")
                .Replace("]", "")
                .Replace(" ", "")
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            return numbers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="finalRoundRollPins"></param>
        /// <returns>A Tuple with the total score and a List<int> containing the round scores</int></returns>
        public static (int, List<int>) CalculateScore(int[] finalRoundRollPins)
        {
            var scorePerRound = new List<int>();
            int totalScore = 0;
            int currentFrameIndex = 0;

            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(finalRoundRollPins[currentFrameIndex]))
                {
                    var pointsForBothRollsInNextFrame = StrikePoints(finalRoundRollPins, currentFrameIndex);
                    scorePerRound.Add(pointsForBothRollsInNextFrame + 10);//Add score for round to list of rounds
                    totalScore += 10 + pointsForBothRollsInNextFrame;

                    //Gå til næste frame. Da det er en strike, er der kun et kast i en frame, og der skal hoppes 1 index i arrayet
                    //for at komme til næste frame
                    currentFrameIndex++;
                }
                else if (IsSpare(finalRoundRollPins[currentFrameIndex], finalRoundRollPins[currentFrameIndex + 1]))
                {
                    var pointForFirstRollInNextFrame = SparePoints(finalRoundRollPins, currentFrameIndex);
                    scorePerRound.Add(pointForFirstRollInNextFrame + 10);//Add score for round to list of rounds
                    totalScore += 10 + pointForFirstRollInNextFrame;

                    //Gå til næste frame. Da det er en spare, er der to kast i en frame, og der skal hoppes 2 indexes i arrayet
                    //for at komme til næste frame
                    currentFrameIndex += 2;
                }
                else
                {
                    var pointsForCurrentFrame = NormalPinPoints(finalRoundRollPins, currentFrameIndex);
                    scorePerRound.Add(pointsForCurrentFrame);//Add score for round to list of rounds
                    totalScore += pointsForCurrentFrame;

                    //Gå til næste frame. Da det er en "almindelig score", er der to kast i en frame, og der skal hoppes 2 indexes i arrayet
                    //for at komme til næste frame
                    currentFrameIndex += 2;
                }
            }

            return (totalScore, scorePerRound);
        }

        /// <summary>
        /// Gets the number of pins in a single throw. If it is 10, it is a strike.
        /// </summary>
        /// <param name="roll"></param>
        /// <returns>True if it is a strike, //Score for round if it is not</returns>
        private static bool IsStrike(int numberOfPins)
        {
            return numberOfPins == 10;
        }

        /// <summary>
        /// Gets the number of pins in two throws. If the total number is 10, it is a spare.
        /// </summary>
        /// <param name="roll"></param>
        /// <returns>True if it is a spare, False if it is not</returns>
        private static bool IsSpare(int pinsInFirstRoll, int pinsInSecondRoll)
        {
            return pinsInFirstRoll + pinsInSecondRoll == 10;
        }

        /// <summary>
        /// Calculate points for a strike. Initially, a strike is 10 point. We then add the points for the next frame.
        /// </summary>
        /// <param name="finalRoundRollPins">The array with all the pins knocked over per roll</param>
        /// <param name="currentFrameIndex">Current frame, in which the strike was scored</param>
        /// <returns>The points for the next frame</returns>
        private static int StrikePoints(int[] finalRoundRollPins, int currentFrameIndex)
        {
            return finalRoundRollPins[currentFrameIndex + 1] + finalRoundRollPins[currentFrameIndex + 2];
        }

        /// <summary>
        /// Calculate points for a spare. Initially, a spare is 10 point. We then add the points for the first roll in the next frame.
        /// </summary>
        /// <param name="finalRoundRollPins">The array with all the pins knocked over per roll</param>
        /// <param name="currentFrameIndex">Current frame, in which the spare was scored</param>
        /// <returns>The points for the next frame</returns>
        private static int SparePoints(int[] finalRoundRollPins, int currentFrameIndex)
        {
            //It's a spare, so we jump two places from current index and return the number of pins in that index
            return finalRoundRollPins[currentFrameIndex + 2];
        }

        /// <summary>
        /// Calculate points for a "normal" throw. We add the points for the first and second roll in the current frame.
        /// </summary>
        /// <param name="finalRoundRollPins">The array with all the pins knocked over per roll</param>
        /// <param name="currentFrameIndex">Current frame, in which the "normal" throw was made</param>
        /// <returns>The points for the current frame</returns>
        private static int NormalPinPoints(int[] finalRoundRollPins, int currentFrameIndex)
        {
            return finalRoundRollPins[currentFrameIndex] + finalRoundRollPins[currentFrameIndex + 1];
        }
    }
}
