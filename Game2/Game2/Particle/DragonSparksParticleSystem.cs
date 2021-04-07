using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2.Particle
{
    public class DragonSparksParticleSystem : ParticleSystem
    {
        IParticleEmitter _emitter;
        public DragonSparksParticleSystem(Game g, IParticleEmitter emitter) : base(g, 200) { _emitter = emitter; }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";
            minNumParticles = 1;
            maxNumParticles = 1;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = _emitter.Velocity;



            var lifetime = RandomHelper.NextFloat(.1f, .6f);
            var acceleration = Vector2.UnitY * 400;
            var scale = RandomHelper.NextFloat(.1f, .5f);

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);
            p.Initialize(where, velocity, acceleration, Color.Red, scale: scale, lifetime: lifetime);
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            AddParticles(_emitter.Position);

        }
        public void PlaceExplosion(Vector2 where) => AddParticles(where);
    }
}
