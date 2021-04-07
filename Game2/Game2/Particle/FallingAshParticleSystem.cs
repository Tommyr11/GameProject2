using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2.Particle
{
    class FallingAshParticleSystem : ParticleSystem
    {
        Rectangle _source;
        bool isRaining { get; set; } = true;

        public FallingAshParticleSystem(Game game, Rectangle source) : base(game, 4000)
        {
            _source = source;
        }
        protected override void InitializeConstants()
        {
            textureFilename = "drop";
            minNumParticles = 10;
            maxNumParticles = 20;
        }
        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, Vector2.UnitY * 200, Vector2.Zero, Color.Black, scale: RandomHelper.NextFloat(0.1f, 0.4f), lifetime: 3);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isRaining) AddParticles(_source);
        }
    }
}
