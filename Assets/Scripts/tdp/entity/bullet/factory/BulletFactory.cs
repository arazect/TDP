﻿using Assets.Scripts.sprite.manager;
using Assets.Scripts.tdp.configuration;
using Assets.Scripts.tdp.entity.bullet.behaviour.collision;
using Assets.Scripts.tdp.entity.bullet.behaviour.destruction;
using Assets.Scripts.tdp.entity.bullet.behaviour.movement;
using UnityEngine;

namespace Assets.Scripts.tdp.entity.bullet.factory {
    public class BulletFactory : MonoBehaviour {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private SpriteManager spriteManager;

        private IBulletBehaviourInCollisionStrategy bulletBehaviourInCollision;
        private IBulletDestroyStrategy destroyStrategy;
        private IBulletMovementStrategy movementStrategy;

        public void Start() {
            bulletBehaviourInCollision = new CollideAndDamageEnemyThenDie();
            destroyStrategy = new DestroyBullet();
            movementStrategy = new MoveToRight();
        }

        public GameObject CreateBullet(int lineId, int damage, Vector3 position) {
            var bulletGameObject = (GameObject) Instantiate(bulletPrefab, position, Quaternion.identity);

            var bullet = bulletGameObject.GetComponent<Bullet>();
            bullet.lineId = lineId;
            bullet.speed = Configuration.BulletMovementSpeed * Configuration.CellWidth;
            bullet.damage = damage;

            bullet.movementStrategy = movementStrategy;
            bullet.bulletBehaviourInCollision = bulletBehaviourInCollision;
            bullet.destroyStrategy = destroyStrategy;

            bulletGameObject.GetComponent<BoxCollider>().size = Configuration.BulletSize;

            Rect bulletFrame = Configuration.BulletFrame;

            bullet.sprite = 
                spriteManager.AddSprite(bulletGameObject,
                                        bulletFrame.width,
                                        bulletFrame.height,
                                        (int) bulletFrame.x,
                                        (int) (bulletFrame.y + bulletFrame.height),
                                        (int) bulletFrame.width, (int) bulletFrame.height,
                                        false);

            return bulletGameObject;
        }

        public void SetSpriteManager(SpriteManager spriteManager) {
            this.spriteManager = spriteManager;
        }

        public SpriteManager GetSpriteManager() {
            return spriteManager;
        }
    }
}