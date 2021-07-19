using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoringApplication
{
    /// <summary>
    /// GameManager contains the shared functions that support the rules of Bowling.
    /// </summary>
    public static class GameManager
    {
        #region Fields
        public static Dictionary<char, int> ScoreLegendDict = new Dictionary<char, int>
        {
            {'-', 0},
            {'0', 0},
            {'1', 1},
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'X', 10},
            {'/', 10}
        };

        public const int PINSPERFRAME = 10;
        public const int FRAMESPERGAME = 10;
        public const int MAXPLAYERS = 4;
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a char array of the chars that a valid for the shot input, and filtered by the previous shot and special considerations for the final frame.
        /// </summary>
        /// <param name="FrameNumber">The Number of the Frame</param>
        /// <param name="ShotIndex">The index of the Shot</param>
        /// <param name="PrevShotChar">The char representing the previous shot</param>
        /// <returns></returns>
        public static char[] GetValidScoreChars(int FrameNumber, int ShotIndex, char PrevShotChar)
        {
            List<char> validChars = new List<char>();

            validChars.Add('-'); //A miss is always possible.

            //Set pins standing after last shot.
            int pinsStanding = GameManager.PINSPERFRAME;
            if (GameManager.ScoreLegendDict[PrevShotChar] < GameManager.PINSPERFRAME)
            {
                pinsStanding = pinsStanding - GameManager.ScoreLegendDict[PrevShotChar];
            }

            // Get chars for number of Pins standing on the current Shot. Make sure always less than double digits/PINSPERFRAME.
            for (int i = 0; i <= pinsStanding && i < PINSPERFRAME; i++)
            {
                validChars.Add(char.Parse(i.ToString()));
            }
            // Add Strike char if available.
            if (ShotIndex == 0 || ScoreLegendDict[PrevShotChar] == PINSPERFRAME)
            {
                validChars.Add('X');
            }
            //Add Spare if available.
            if (ShotIndex > 0 && ScoreLegendDict[PrevShotChar] < PINSPERFRAME)
            {
                validChars.Add('/');
            }

            return validChars.ToArray();
        }

        /// <summary>
        /// Calculates the score for the current Frame
        /// </summary>
        /// <param name="FrameNumber">The Number of the Frame to be calculated</param>
        /// <param name="ShotIndex">The index of the shot</param>
        /// <param name="ShotsInFrame">The chars representing the shots completed in the frame</param>
        /// <param name="BonusShots">The chars representing any bonus shots that contribute to the calculation from a strike or spare</param>
        /// <returns></returns>
        public static int CalculateScore(int FrameNumber, int ShotIndex, char[] ShotsInFrame, char[] BonusShots)
        {
            int pointsEarned = 0;

            for (int i = 0; i < ShotsInFrame.Length; i++)
            {
                char shotChar = ShotsInFrame[i];
                char prevShotChar = '-';
                if (i > 0)
                {
                    prevShotChar = ShotsInFrame[i - 1];
                }
                pointsEarned += GetPointsByShot(shotChar, prevShotChar);
            }

            for (int i = 0; i < BonusShots.Length; i++)
            {
                char prevShotChar = '-';
                if (i > 0)
                {
                    prevShotChar = BonusShots[i - 1];
                }
                pointsEarned += GetPointsByShot(BonusShots[i], prevShotChar);
            }

            return pointsEarned;
        }
        /// <summary>
        /// Determines whether the score for the frame can be calculated. If enough subsequent shots have been bowled, then the score can be calculated.
        /// </summary>
        /// <param name="FrameNumber"></param>
        /// <param name="ShotIndex">The index of the shot</param>
        /// <param name="ShotsInFrame">How many shots are in the frame</param>
        /// <param name="BonusShots">The char representations of the bonus shots required to calculate this frame's score</param>
        /// <returns></returns>
        public static bool CanCalculateFrameScore(int FrameNumber, int ShotIndex, char[] ShotsInFrame, char[] BonusShots)
        {
            bool CanCalculate = false;
            int additionalShotsRequired = 0;

            if (ShotIndex > 0 || ShotsInFrame[ShotIndex] == 'X')
            {
                for (int i = 0; i < ShotsInFrame.Length; i++)
                {
                    additionalShotsRequired = GetBonusShotCountByChar(FrameNumber, ShotIndex, ShotsInFrame[i]);
                }

                if (BonusShots.Length >= additionalShotsRequired)
                {
                    CanCalculate = true;
                }
            }

            return CanCalculate;
        }
        /// <summary>
        /// GetBonusShotCountByChar returns the number of Bonus Shots a Closed Frame requires to calculate its score.
        /// Strikes and Spares have diminishing returns in the Final Frame.
        /// </summary>
        /// <param name="FrameNumber">The Frame Number of the Shot</param>
        /// <param name="ShotIndex">The index of the Shot</param>
        /// <param name="ShotChar">The char representation of the shot</param>
        /// <returns></returns>
        public static int GetBonusShotCountByChar(int FrameNumber, int ShotIndex, char ShotChar)
        {
            int bonusShots = 0;
            switch (ShotChar)
            {
                case 'X':
                    bonusShots = 2;
                    break;
                case '/':
                    bonusShots = 1;
                    break;
                default:
                    break;
            }

            if (FrameNumber == FRAMESPERGAME) //Condition to satisfy 10th frame rules.
            {
                bonusShots -= ShotIndex;
            }

            return bonusShots;
        }
        /// <summary>
        /// GetPointsByShot gets the points for the individual shot of the frame.
        /// </summary>
        /// <param name="ShotChar"></param>
        /// <param name="PrevShotChar">Pass '-' if a Strike.</param>
        /// <returns></returns>
        public static int GetPointsByShot(char ShotChar, char PrevShotChar)
        {
            int output = 0;
            if (ShotChar == '/')
            {
                output += PINSPERFRAME - ScoreLegendDict[PrevShotChar];
            }
            else
            {
                output += ScoreLegendDict[ShotChar];
            }

            return output;
        }
        #endregion

    }
}
