using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingScoringApplication
{
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
        public static char[] GetValidScoreChars(int FrameNumber, int ShotIndex, int PinsStanding, char PrevShotChar)
        {
            List<char> validChars = new List<char>();

            validChars.Add('-');

            // Get chars for number of Pins standing on the current Shot. Make sure always less than double digits/PINSPERFRAME.
            for (int i = 0; i <= PinsStanding && i < PINSPERFRAME; i++)
            {
                validChars.Add(i.ToString().ToCharArray()[0]);
            }
            // Add Strike char if available.
            if (ShotIndex == 0 || ScoreLegendDict[PrevShotChar] == PINSPERFRAME)
            {
                validChars.Add('X');
            }
            if (ShotIndex > 0 && ScoreLegendDict[PrevShotChar] < PINSPERFRAME)
            {
                validChars.Add('/');
            }

            return validChars.ToArray();
        }

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
