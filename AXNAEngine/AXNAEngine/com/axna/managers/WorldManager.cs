using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace AXNAEngine.com.axna.managers
{
    public class WorldManager
    {
        private readonly List<World> _worlds = new List<World>();
        private World _nextWorld;
        private World _prevWorld;

        private World _activeWorld;

        internal World GetActiveWorld()
        {
            return _activeWorld;
        }

        internal void AddWorld(World world)
        {
            if (_worlds.Any(scr => scr.Name == world.Name))
            {
                throw new Exception("World with this name already registered!");
            }

            _worlds.Add(world);
        }

        internal int GetWorldsCount()
        {
            return _worlds.Count;
        }

        internal World GetWorldByIndex(int index)
        {
            return _worlds[index];
        }

        internal World GetWorldByName(string name)
        {
            foreach (World scr in _worlds)
            {
                if (scr.Name == name)
                {
                    return scr;
                }
            }
            throw new Exception(String.Format("World with name {0} is not found", name));
        }

        internal void ActivateWorld(World world)
        {
            _nextWorld = world;
        }

        internal void ActivateWorldByIndex(int index)
        {
            ActivateWorld(GetWorldByIndex(index));
        }

        internal void ActivateWorldByName(string name)
        {
            ActivateWorld(GetWorldByName(name));
        }

        internal void ActivatePreviousWorld()
        {
            if (_prevWorld != null)
            {
                ActivateWorldByName(_prevWorld.Name);
            }
        }

        //
        internal void Update(GameTime gameTime)
        {
            if (_nextWorld != null)
            {
                if (_activeWorld != null)
                {
                    _activeWorld.OnRemove();
                }
                _prevWorld = _activeWorld;

                _activeWorld = _nextWorld;
                _activeWorld.Initialize();

                _nextWorld = null;
            }

            //
            if (_activeWorld != null)
            {
                _activeWorld.OnUpdate(gameTime);
            }
        }

        internal void Draw(GameTime gameTime)
        {
            if (_activeWorld != null)
            {
                _activeWorld.OnDraw(gameTime);
            }
        }
    }
}