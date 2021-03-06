﻿using Assets.Scripts.sprite.manager;
using Assets.Scripts.tdp.configuration;
using Assets.Scripts.tdp.constants;
using Assets.Scripts.tdp.entity.bullet.factory;
using Assets.Scripts.tdp.entity.tower.behaviour.shooting;
using Assets.Scripts.tdp.entity.tower.behaviour.targeting;
using Assets.Scripts.tdp.gui;
using UnityEngine;

namespace Assets.Scripts.tdp.entity.tower.factory {
    public class TowerFactory : MonoBehaviour {
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] private SpriteManager spriteManager;
        [SerializeField] private BulletFactory bulletFactory;

        private IFindTargetStrategy targetingStrategy;
        private IShootStrategy shootingStrategy;

        public void Start() {
            targetingStrategy = new FindEnemy();
            shootingStrategy = new CreateBullet(bulletFactory);
        }

        public GameObject CreateTower(TowerSlot towerSlot, TowerType towerType) {
            var towerGameObject = (GameObject) Instantiate(
                towerPrefab,
                towerSlot.gameObject.transform.position,
                Quaternion.identity);

            Tower tower = towerGameObject.GetComponent<Tower>();
            tower.lineId = towerSlot.GetLineId();

            tower.attackRange = Configuration.Towers[towerType].AttackRange * Configuration.CellWidth;
            tower.damage = Configuration.Towers[towerType].Damage;
            tower.shootsPerSecond = Configuration.Towers[towerType].ShootsPerSecond;

            tower.shootingStrategy = shootingStrategy;
            tower.targetingStrategy = targetingStrategy;

            towerGameObject.GetComponent<BoxCollider>().size = Configuration.TowerSize;

            // Объект уже в мире из-за вызова Instantiate

            // Добавляем спрайт
            Rect towerFrame = Configuration.Towers[towerType].SpriteFrame;

            tower.sprite =
                spriteManager.AddSprite(towerGameObject,
                                        towerFrame.width,
                                        towerFrame.height,
                                        (int) towerFrame.x,
                                        (int) (towerFrame.y + towerFrame.height),
                                        (int) towerFrame.width, (int) towerFrame.height,
                                        false);

            return towerGameObject;
        }

        public void SetSpriteManager(SpriteManager spriteManager) {
            this.spriteManager = spriteManager;
        }
    }
}