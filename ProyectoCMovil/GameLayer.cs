using System;
using System.Collections.Generic;
using CocosSharp;

namespace ProyectoCMovil
{
    public class GameLayer : CCLayerColor
    {
        CCSprite paddleSprite;
        CCSprite ballSprite;
        CCLabel scoreLabel;
        CCLabel endLabel;



        float ballXVelocity;
        float ballYVelocity;

        // How much to modify the ball's y velocity per second:
        const float gravity = 400;

        int score;

        public GameLayer() : base(CCColor4B.AliceBlue)
        {
            // "paddle" refers to the paddle.png image
            paddleSprite = new CCSprite("paddle");
            paddleSprite.PositionX = 320;
            paddleSprite.PositionY = 300;
            AddChild(paddleSprite);

            ballSprite = new CCSprite("ball");
            ballSprite.PositionX = 320;
            ballSprite.PositionY = 800;
            AddChild(ballSprite);

            scoreLabel = new CCLabel("Score: 0", "Arial", 20, CCLabelFormat.SystemFont);
            scoreLabel.Color = CCColor3B.Black;
            scoreLabel.PositionX = 50;
            scoreLabel.PositionY = 1000;
            scoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            AddChild(scoreLabel);

            /*endLabel = new CCLabel("", "Arial", 20, CCLabelFormat.SystemFont);
            endLabel.PositionX = 50;
            endLabel.PositionY = 1000;
            endLabel.AnchorPoint = CCPoint.AnchorLowerRight;
            AddChild(endLabel);*/

            Schedule(RunGameLogic);
        }

        void RunGameLogic(float frameTimeInSeconds)
        {
            // This is a linear approximation, so not 100% accurate
            ballYVelocity += frameTimeInSeconds * -gravity;
            ballSprite.PositionX += ballXVelocity * frameTimeInSeconds;
            ballSprite.PositionY += ballYVelocity * frameTimeInSeconds;

            // New Code:
            // Check if the two CCSprites overlap...
            bool doesBallOverlapPaddle = ballSprite.BoundingBoxTransformedToParent.IntersectsRect(
                paddleSprite.BoundingBoxTransformedToParent);

            /*bool doesBallFall = ballSprite.BoundingBoxTransformedToParent.IntersectsRect(
                ballSprite.BoundingBoxTransformedToParent);*/

            // ... and if the ball is moving downward.
            bool isMovingDownward = ballYVelocity < 0;

            // bool overPass = ballSprite

            if (doesBallOverlapPaddle && isMovingDownward)
            {
                // First let's invert the velocity:
                ballYVelocity *= -1;
                // Then let's assign a random to the ball's x velocity:
                const float minXVelocity = -300;
                const float maxXVelocity = 300;
                ballXVelocity = CCRandom.GetRandomFloat(minXVelocity, maxXVelocity);
                // New code:
                score++;
                scoreLabel.Text = "Score: " + score;
            }

            /* if (doesBallFall && isMovingDownward)
              {

              }*/

            // First let’s get the ball position:   
            float ballRight = ballSprite.BoundingBoxTransformedToParent.MaxX;
            float ballLeft = ballSprite.BoundingBoxTransformedToParent.MinX;
            float ballDown = ballSprite.BoundingBoxTransformedToParent.MaxY;
            // Then let’s get the screen edges
            float screenRight = VisibleBoundsWorldspace.MaxX;
            float screenLeft = VisibleBoundsWorldspace.MinX;
            float screenDown = VisibleBoundsWorldspace.MaxY;
            float screenUp = VisibleBoundsWorldspace.MinY;

            //scoreLabel.Text ;

            // Check if the ball is either too far to the right or left:    
            bool shouldReflectXVelocity =
                (ballRight > screenRight && ballXVelocity > 0) ||
                (ballLeft < screenLeft && ballXVelocity < 0);

            if (shouldReflectXVelocity)
            {
                ballXVelocity *= -1;
            }

            /* // Si el balon sale de la pantalla
             Boolean bandera = false;
              if (ballSprite.PositionY > 200 )
              {
                  bandera = true;
                  endLabel.Text = "Fin del juego";
              }

              if (bandera == true)
              {


              }
              */
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            // Use the bounds to layout the positioning of our drawable assets
            CCRect bounds = VisibleBoundsWorldspace;

            // Register for touch events
            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = OnTouchesEnded;
            touchListener.OnTouchesMoved = HandleTouchesMoved;
            AddEventListener(touchListener, this);
        }

        void OnTouchesEnded(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                // Perform touch handling here
            }
        }

        void HandleTouchesMoved(System.Collections.Generic.List<CCTouch> touches, CCEvent touchEvent)
        {
            // we only care about the first touch:
            var locationOnScreen = touches[0].Location;
            paddleSprite.PositionX = locationOnScreen.X;
        }

    }
}
