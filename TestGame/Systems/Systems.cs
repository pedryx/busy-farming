using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;


namespace TestGame.Systems
{
    internal abstract class EntityUpdateSystem<T1> : EntityUpdateSystem
    where T1 : class
    {
        private ComponentMapper<T1> componentMapper1;

        protected int EntityID { get; private set; }

        protected EntityUpdateSystem()
            : base(Aspect.All(typeof(T1))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            componentMapper1 = mapperService.GetMapper<T1>();
        }

        public override void Update(GameTime gameTime)
        {
            PreUpdate(gameTime);

            foreach (var entity in ActiveEntities)
            {
                EntityID = entity;
                var component1 = componentMapper1.Get(entity);

                Update(component1, gameTime);
            }

            PostUpdate(gameTime);
        }

        public virtual void PreUpdate(GameTime gameTime) { }
        public virtual void PostUpdate(GameTime gameTime) { }
        public virtual void Update(T1 component1, GameTime gameTime) { }
    }

    internal abstract class EntityUpdateSystem<T1, T2> : EntityUpdateSystem
        where T1 : class
        where T2 : class
    {
        private ComponentMapper<T1> componentMapper1;
        private ComponentMapper<T2> componentMapper2;

        protected int EntityID { get; private set; }

        protected EntityUpdateSystem()
            : base(Aspect.All(typeof(T1), typeof(T2))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            componentMapper1 = mapperService.GetMapper<T1>();
            componentMapper2 = mapperService.GetMapper<T2>();
        }

        public override void Update(GameTime gameTime)
        {
            PreUpdate(gameTime);

            foreach (var entity in ActiveEntities)
            {
                EntityID = entity;
                var component1 = componentMapper1.Get(entity);
                var component2 = componentMapper2.Get(entity);

                Update(component1, component2, gameTime);
            }

            PostUpdate(gameTime);
        }

        public virtual void PreUpdate(GameTime gameTime) { }
        public virtual void PostUpdate(GameTime gameTime) { }
        public virtual void Update(T1 component1, T2 component2, GameTime gameTime) { }
    }

    internal abstract class EntityUpdateSystem<T1, T2, T3> : EntityUpdateSystem
        where T1 : class
        where T2 : class
        where T3 : class
    {
        private ComponentMapper<T1> componentMapper1;
        private ComponentMapper<T2> componentMapper2;
        private ComponentMapper<T3> componentMapper3;

        protected int EntityID { get; private set; }

        protected EntityUpdateSystem()
            : base(Aspect.All(typeof(T1), typeof(T2), typeof(T3))) { }

        public override void Initialize(IComponentMapperService mapperService)
        {
            componentMapper1 = mapperService.GetMapper<T1>();
            componentMapper2 = mapperService.GetMapper<T2>();
            componentMapper3 = mapperService.GetMapper<T3>();
        }

        public override void Update(GameTime gameTime)
        {
            PreUpdate(gameTime);

            foreach (var entity in ActiveEntities)
            {
                EntityID = entity;
                var component1 = componentMapper1.Get(entity);
                var component2 = componentMapper2.Get(entity);
                var component3 = componentMapper3.Get(entity);

                Update(component1, component2, component3, gameTime);
            }

            PostUpdate(gameTime);
        }

        public virtual void PreUpdate(GameTime gameTime) { }
        public virtual void PostUpdate(GameTime gameTime) { }
        public virtual void Update(T1 component1, T2 component2, T3 component3, GameTime gameTime) { }
    }

    internal abstract class EntityDrawSystem<T1> : EntityDrawSystem
        where T1 : class
    {
        private ComponentMapper<T1> componentMapper1;

        protected SpriteBatch SpriteBatch { get; private set; }
        protected int EntityID { get; private set; }

        protected EntityDrawSystem(SpriteBatch spriteBatch) 
            : base(Aspect.All(typeof(T1))) 
        {
            SpriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            componentMapper1 = mapperService.GetMapper<T1>();
        }

        public override void Draw(GameTime gameTime)
        {
            PreDraw(gameTime);

            foreach (var entity in ActiveEntities)
            {
                EntityID = entity;
                var component1 = componentMapper1.Get(entity);

                Draw(component1, gameTime);
            }

            PostDraw(gameTime);
        }

        public virtual void PreDraw(GameTime gameTime) { }
        public virtual void PostDraw(GameTime gameTime) { }
        public virtual void Draw(T1 component1, GameTime gameTime) { }
    }

    internal abstract class EntityDrawSystem<T1, T2> : EntityDrawSystem
        where T1 : class
        where T2 : class
    {
        private ComponentMapper<T1> componentMapper1;
        private ComponentMapper<T2> componentMapper2;

        protected SpriteBatch SpriteBatch { get; private set; }
        protected int EntityID { get; private set; }

        protected EntityDrawSystem(SpriteBatch spriteBatch)
            : base(Aspect.All(typeof(T1), typeof(T2)))
        {
            SpriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            componentMapper1 = mapperService.GetMapper<T1>();
            componentMapper2 = mapperService.GetMapper<T2>();
        }

        public override void Draw(GameTime gameTime)
        {
            PreDraw(gameTime);

            foreach (var entity in ActiveEntities)
            {
                EntityID = entity;
                var component1 = componentMapper1.Get(entity);
                var component2 = componentMapper2.Get(entity);

                Draw(component1, component2, gameTime);
            }

            PostDraw(gameTime);
        }

        public virtual void PreDraw(GameTime gameTime) { }
        public virtual void PostDraw(GameTime gameTime) { }
        public virtual void Draw(T1 component1, T2 component2, GameTime gameTime) { }
    }

    internal abstract class EntityDrawSystem<T1, T2, T3> : EntityDrawSystem
        where T1 : class
        where T2 : class
        where T3 : class
    {
        private ComponentMapper<T1> componentMapper1;
        private ComponentMapper<T2> componentMapper2;
        private ComponentMapper<T3> componentMapper3;

        protected SpriteBatch SpriteBatch { get; private set; }
        protected int EntityID { get; private set; }

        protected EntityDrawSystem(SpriteBatch spriteBatch)
            : base(Aspect.All(typeof(T1), typeof(T2), typeof(T3)))
        {
            SpriteBatch = spriteBatch;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            componentMapper1 = mapperService.GetMapper<T1>();
            componentMapper2 = mapperService.GetMapper<T2>();
            componentMapper3 = mapperService.GetMapper<T3>();
        }

        public override void Draw(GameTime gameTime)
        {
            PreDraw(gameTime);

            foreach (var entity in ActiveEntities)
            {
                EntityID = entity;
                var component1 = componentMapper1.Get(entity);
                var component2 = componentMapper2.Get(entity);
                var component3 = componentMapper3.Get(entity);

                Draw(component1, component2, component3, gameTime);
            }

            PostDraw(gameTime);
        }

        public virtual void PreDraw(GameTime gameTime) { }
        public virtual void PostDraw(GameTime gameTime) { }
        public virtual void Draw(T1 component1, T2 component2, T3 component3, GameTime gameTime) { }
    }
}
