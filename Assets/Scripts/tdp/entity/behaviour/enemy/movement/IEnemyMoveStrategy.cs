﻿namespace Assets.Scripts.tdp.entity.behaviour.enemy.movement {
    public interface IEnemyMoveStrategy {
        void Move(Enemy contextEnemy, float elapsedTime);
    }
}