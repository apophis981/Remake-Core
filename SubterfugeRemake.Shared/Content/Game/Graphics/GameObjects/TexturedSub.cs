﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SubterfugeCore.Entities;
using SubterfugeCore.Entities.Base;

namespace SubterfugeFrontend.Shared.Content.Game.Graphics.GameObjects
{
    class TexturedSub : TexturedGameObject
    {

        public TexturedSub(GameObject gameObject) : base(gameObject, SubterfugeApp.SpriteLoader.getSprite("SubFill"),
            100, 100)
        {
            this.rotation = (float)Math.PI / 4f;
        }

        public override void render(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(
                texture: this.getTexture(),
                destinationRectangle: Camera.getRelativeLocation(this),
                color: Color.Blue,
                origin: this.getTexture().Bounds.Size.ToVector2() / 2f,
                rotation: this.rotation + (float)((Sub)gameObject).getRotation());

            SpriteFont font = SubterfugeApp.FontLoader.getFont("Arial");
            string drillerCount = ((Sub) this.gameObject).getDrillerCount().ToString();
            Vector2 stringSize = font.MeasureString(drillerCount);


            spriteBatch.DrawString(
                spriteFont: SubterfugeApp.FontLoader.getFont("Arial"),
                text: drillerCount,
                position: Camera.getRelativePosition(this.getPosition()),
                color: Color.White,
                rotation: (float)(this.rotation),
                origin: stringSize / 2f,
                layerDepth: 1f,
                scale: 1.5f,
                effects: SpriteEffects.None);
        }
    }
}
