using System;
using System.Collections.Generic;
using AXNAEngine.com.axna.graphics;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna
{
    public class World
    {
        private readonly List<GameEntity> _addList = new List<GameEntity>();
        private readonly List<GameEntity> _removeList = new List<GameEntity>();
        private readonly List<GameEntity> _entities = new List<GameEntity>();

        public World(String name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        #region Public Methods

        public void AddEntity(GameEntity gameEntity)
        {
            _addList.Add(gameEntity);
        }

        public void RemoveEntity(GameEntity gameEntity)
        {
            _removeList.Add(gameEntity);
        }

        #endregion

        #region Overridable Methods

        public virtual void OnInitialize()
        {
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
            UpdateLists();

            foreach (GameEntity entity in _entities)
            {
                entity.Update(gameTime);
            }
        }

        public virtual void OnDraw(GameTime gameTime)
        {
            AXNA.SpriteBatch.Begin();

            foreach (GameEntity entity in _entities)
            {
                entity.Draw(gameTime);
            }

            AXNA.SpriteBatch.End();
        }

        public virtual void OnRemove()
        {
            _entities.RemoveRange(0, _entities.Count);
        }

        #endregion

        #region Internal Methods

        private void UpdateLists()
        {
            // Добавление
            foreach (GameEntity entity in _addList)
            {
                entity.ParentWorld = this;
                _entities.Add(entity);
            }

            // Удаление
            foreach (GameEntity entity in _removeList)
            {
                entity.ParentWorld = null;
                _entities.Remove(entity);
            }

            _addList.RemoveRange(0, _addList.Count);
            _removeList.RemoveRange(0, _removeList.Count);
        }

        #endregion
    }
}