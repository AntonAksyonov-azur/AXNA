﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna
{
    public class World
    {
        public List<GameEntity> AddList = new List<GameEntity>();
        public List<GameEntity> Entities = new List<GameEntity>();
        public List<GameEntity> RemoveList = new List<GameEntity>();

        public World(String name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        #region Public Methods

        public void AddEntity(GameEntity gameEntity)
        {
            AddList.Add(gameEntity);
        }

        public void RemoveEntity(GameEntity gameEntity)
        {
            RemoveList.Add(gameEntity);
        }

        #endregion

        #region Overridable Methods

        public virtual bool OnInitialize()
        {
            return true;
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
            UpdateLists();

            foreach (GameEntity entity in Entities)
            {
                entity.Update(gameTime);
            }
        }

        public virtual void OnDraw(GameTime gameTime)
        {
            AXNA.SpriteBatch.Begin();

            foreach (GameEntity entity in Entities)
            {
                entity.Draw(gameTime);
            }

            AXNA.SpriteBatch.End();
        }

        public virtual void OnRemove()
        {
            Entities.RemoveRange(0, Entities.Count);
        }

        #endregion

        #region Internal Methods

        private void UpdateLists()
        {
            // Добавление
            foreach (GameEntity entity in AddList)
            {
                entity.ParentWorld = this;
                Entities.Add(entity);
            }

            // Удаление
            foreach (GameEntity entity in RemoveList)
            {
                entity.ParentWorld = null;
                Entities.Remove(entity);
            }

            AddList.RemoveRange(0, AddList.Count);
            RemoveList.RemoveRange(0, RemoveList.Count);
        }

        #endregion
    }
}