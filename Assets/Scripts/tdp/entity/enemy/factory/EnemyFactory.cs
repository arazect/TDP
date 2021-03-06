﻿using Assets.Scripts.sprite.manager;
using Assets.Scripts.tdp.configuration;
using Assets.Scripts.tdp.constants;
using Assets.Scripts.tdp.entity.enemy.behaviour.death;
using Assets.Scripts.tdp.entity.enemy.behaviour.movement;
using UnityEngine;

namespace Assets.Scripts.tdp.entity.enemy.factory {
    public class EnemyFactory : MonoBehaviour {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private SpriteManager spriteManager;

        private IEnemyDieStrategy deathStrategy;
        private IEnemyMoveStrategy movementStrategy;

        public void Start() {
            movementStrategy = new MoveToLeft();
            deathStrategy = new EnemyDeath();
        }

        public GameObject CreateEnemy(Vector3 position, int lineId, EnemyType enemyType) {
            var enemyGameObject = (GameObject) Instantiate(enemyPrefab, position, Quaternion.identity);

            var enemy = enemyGameObject.GetComponent<Enemy>();
            enemy.lineId = lineId;
            enemy.maxHealth = Configuration.Enemies[enemyType].MaxHealth;
            enemy.currentHealth = enemy.maxHealth;
            enemy.speed = Configuration.Enemies[enemyType].Speed * Configuration.CellWidth;

            enemy.movementStrategy = movementStrategy;
            enemy.deathStrategy = deathStrategy;

            enemyGameObject.GetComponent<BoxCollider>().size = Configuration.EnemySize;

            // Объект уже в мире из-за вызова Instantiate

            // Добавляем спрайт
            Rect enemyFrame = Configuration.Enemies[enemyType].SpriteFrame;

            var sprite = 
                spriteManager.AddSprite(enemyGameObject,
                                        enemyFrame.width,
                                        enemyFrame.height,
                                        (int) enemyFrame.x,
                                        (int) (enemyFrame.y + enemyFrame.height),
                                        (int) enemyFrame.width, (int) enemyFrame.height,
                                        false);
            enemy.sprite = sprite;

            return enemyGameObject;
        }

        public void SetSpriteManager(SpriteManager spriteManager) {
            this.spriteManager = spriteManager;
        }
    }
}