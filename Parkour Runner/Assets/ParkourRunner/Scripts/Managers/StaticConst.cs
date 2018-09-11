﻿namespace ParkourRunner.Scripts.Managers
{
    static class StaticConst
    {

        public const float MagnetCoinVelocity = 20f; //Скорость монеток, летящих к игроку при магните

        public const int MaxEnemyDifficulty = 5;
        public const int MinEnemyAttacks = 3;
        public const float AttacksPerDifficulty = 1;

        public const float MaxLaserLen = 20f;

        public const float SpeedGrowPerMetre = 1f;


        //Chances to generate
        //Obstacles
        public const float HeatUpObstaclePercent = 0.0f;
        public const float CallibrationObstaclePercent = 0.8f;
        public const float RewardObstaclePercent = 0.00f;
        public const float ChallengeObstaclePercent = 0.9f;
        public const float RelaxObstaclePercent = 0.05f;
        //Bonuses and coins
        public const float HeatUpPickUpPercent = 0.0f;
        public const float CallibrationPickUpPercent = 0f;
        public const float RewardPickUpPercent = 0.5f;
        public const float ChallengePickUpPercent = 0.1f;
        public const float RelaxPickUpPercent = 0.5f;
        public const float RelaxTrickPercent = 0.5f;
        //TODO /avg per block
    }
}