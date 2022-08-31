namespace Tile_System.Player
{
    public class Movement
    {
        private readonly Main player;
        private readonly int size;
        public Vector2 velocity;

        private readonly int gravity = 60;
        private readonly float jumpForce = 14;
        private readonly float jumpBufferTime = .1f;
        private readonly float coyoteTime = .125f;
        private readonly float terminalVelocity = 30;

        private float jumpTimer;
        private float coyoteTimer;

        private readonly float moveSpeed = 2.5f;
        private readonly float maxMoveSpeed = 10;
        private readonly float testMovementMulti = 5;
        private readonly float frictionMulti = .85f;

        public bool grounded;
        public bool useTestMovement;

        public Movement(Main player, int size)
        {
            this.player = player;
            this.size = size;
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Inputs(deltaTime);
            Move(deltaTime);
            player.position += velocity;
        }

        private void Inputs(float deltaTime)
        {
            coyoteTimer -= deltaTime;
            if(grounded) coyoteTimer = coyoteTime;

            jumpTimer -= deltaTime;
            if(player.input.moveVector.Y > 0) jumpTimer = jumpBufferTime;
        }

        private void Move(float deltaTime)
        {
            if(useTestMovement) {
                TestMovement(deltaTime);
                return;
            }

            XMovement(deltaTime);
            YMovement(deltaTime);
        }

        private void TestMovement(float deltaTime)
        {
            Vector2 moveVector = player.input.moveVector * new Vector2(1, -1);
            velocity = moveVector.Normalized() * deltaTime * size * moveSpeed * testMovementMulti;
        }

        private void XMovement(float deltaTime)
        {
            float moveVectorX = player.input.moveVector.X;
            if(moveVectorX.GetValue() * velocity.X < maxMoveSpeed)
                velocity.X += moveVectorX * moveSpeed * deltaTime * size;

            if(moveVectorX == 0) velocity.X *= frictionMulti;
            if(MathF.Abs(velocity.X) < 1f) velocity.X = 0;
        }

        private void YMovement(float deltaTime)
        {
            velocity.Y += gravity * deltaTime;

            if(jumpTimer > 0 && coyoteTimer > 0) Jump();

            if(velocity.Y > terminalVelocity) velocity.Y = terminalVelocity;
        }

        private void Jump()
        {
            velocity.Y = -jumpForce;
            coyoteTimer = 0;
            jumpTimer = 0;
            player.position.Y -= .1f;
        }
    }
}