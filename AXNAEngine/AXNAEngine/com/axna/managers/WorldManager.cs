using System;
using System.Collections.Generic;
using System.Linq;
using AXNAEngine.com.axna.worlds;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.managers
{
    public class WorldManager
    {
        private readonly List<World> _worlds = new List<World>();
        private World _activeWorld;
        private World _nextWorld;
        private World _prevWorld;

        public World GetActiveWorld()
        {
            return _activeWorld;
        }

        #region Control Methods

        /// <summary>
        ///     Регистрирует мир в списке для дальнейшего использования
        ///     Бросает исключение, если мир с таким именем уже присутствует в списке.
        /// </summary>
        /// <param name="world"></param>
        public void AddWorld(World world)
        {
            if (_worlds.Any(scr => scr.Name == world.Name))
            {
                throw new Exception("World with this name already registered!");
            }

            _worlds.Add(world);
        }

        /// <summary>
        ///     Активирует мир.
        ///     Если такой мир еще не был добавлен в список зарегистрированных, добавляет его.
        /// </summary>
        /// <param name="world"></param>
        public void ActivateWorld(World world)
        {
            if (_worlds.All(scr => scr.Name != world.Name))
            {
                _worlds.Add(world);
            }

            _nextWorld = world;
        }

        /// <summary>
        ///     Активирует мир.
        ///     Использует его порядкой номер в списке
        /// </summary>
        /// <param name="index"></param>
        public void ActivateWorldByIndex(int index)
        {
            ActivateWorld(GetWorldByIndex(index));
        }

        /// <summary>
        ///     Активирует мир.
        ///     Использует имя, под котороым он был зарегистрирован
        /// </summary>
        /// <param name="name"></param>
        public void ActivateWorldByName(string name)
        {
            ActivateWorld(GetWorldByName(name));
        }

        /// <summary>
        ///     Активирует предыдущий активный мир.
        ///     Ничего не делает, если ранее ни один мир не был активирован
        /// </summary>
        public void ActivatePreviousWorld()
        {
            if (_prevWorld != null)
            {
                ActivateWorldByName(_prevWorld.Name);
            }
        }

        #endregion

        #region InformationMethods

        /// <summary>
        ///     Возвращает количество зарегистрированных миров
        /// </summary>
        /// <returns></returns>
        public int GetWorldsCount()
        {
            return _worlds.Count;
        }

        /// <summary>
        ///     Возвращает мир, используя его порядкой номер в списке зарегистрированных.
        ///     Бросает исключение, если индекс выходит за границы массива
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public World GetWorldByIndex(int index)
        {
            return _worlds[index];
        }

        /// <summary>
        ///     Вовзращает мир по его имени.
        ///     Если мир не найден, возвращает null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public World GetWorldByName(string name)
        {
            foreach (World scr in _worlds)
            {
                if (scr.Name == name)
                {
                    return scr;
                }
            }
            // throw new Exception(String.Format("World with name {0} is not found", name));
            return null;
        }

        #endregion

        #region Engine Methods

        /// <summary>
        ///     Базовый метод обновления всех сущностей в мире.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (_nextWorld != null)
            {
                if (_activeWorld != null)
                {
                    _activeWorld.OnRemove();
                }
                _prevWorld = _activeWorld;

                _activeWorld = _nextWorld;
                _activeWorld.OnInitialize();

                _nextWorld = null;
            }

            //
            if (_activeWorld != null)
            {
                _activeWorld.OnUpdate(gameTime);
            }
        }

        /// <summary>
        ///     Базовый метод рисования всех графики всех сущностей в мире.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            if (_activeWorld != null)
            {
                _activeWorld.OnDraw(gameTime);
            }
        }

        #endregion

        //
    }
}