using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GameServer.Movement.StateMachine;

namespace GameServer.Movement.States.Player.Airborne.Swinging
{
    public class PlayerRopeGrabbingState : PlayerAirborneState
    {
        private Vector3 _anchorPoint;
        private float _ropeLength;
        private float _swingForce = 15f;
        private float _climbSpeed = 2f;

        public PlayerRopeGrabbingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {

        }

        public override void Enter()
        {
            base.Enter();
            _ropeLength = Vector3.Distance(_stateMachine.Player.position, _anchorPoint);
            StartAnimation("RopeSwing");
        }

        public override void HandleInput(bool[] movementInputs)
        {
            base.HandleInput(movementInputs);

            if (_stateMachine.Player.PressingSpace)
            {
                _stateMachine.Player.VelocityY = Constants.JumpForce;
                _stateMachine.ChangeState(_stateMachine.JumpingState);
                return;
            }
        }

        public override void Update(float deltaTime)
        {
            ApplySwingPhysics(deltaTime);
        }


        public void SetAnchorPoint(Vector3 anchor)
        {
            _anchorPoint = anchor;
        }

        private void ApplySwingPhysics(float deltaTime)
        {
            var player = _stateMachine.Player;

            // 1. Áp dụng trọng lực (Gravity)
            player.VelocityY -= Constants.Gravity * deltaTime;

            // 2. Xử lý Input đu (Swing) - A/D hoặc Left/Right
            // Tạo lực vuông góc với hướng dây để đẩy nhân vật
            Vector3 ropeDir = Vector3.Normalize(player.position - _anchorPoint);

            // Giả sử camera nhìn theo trục Z, vector bên phải là Cross(Up, Forward)
            // Tính toán hướng Swing dựa trên Camera Rotation (nếu có) hoặc trục thế giới
            Vector3 swingDirection = Vector3.Zero;

            // Logic đơn giản: Swing theo hướng Input X (A/D)
            if (_inputDirection.X != 0)
            {
                // Tìm vector tiếp tuyến (tangent) để đẩy nhân vật
                // Cross với trục Y để tìm hướng ngang, sau đó project lên mặt phẳng vuông góc dây
                Vector3 rightVector = Vector3.Transform(Vector3.UnitX, player.rotation);
                swingDirection = rightVector * _inputDirection.X;
            }

            // Áp dụng lực Swing vào vận tốc (chỉ áp dụng khi ở dưới thấp để tạo đà)
            player.position += swingDirection * _swingForce * deltaTime * deltaTime; // Verlet integration đơn giản hoặc cộng thẳng vào velocity

            // 3. Xử lý Input leo dây (W/S) - Thay đổi chiều dài dây
            if (_inputDirection.Y != 0)
            {
                _ropeLength -= _inputDirection.Y * _climbSpeed * deltaTime;
                _ropeLength = Math.Max(_ropeLength, 1.0f); // Không cho dây ngắn hơn 1m
            }

            // 4. Dự đoán vị trí tiếp theo (Tích hợp vận tốc)
            Vector3 velocity = new Vector3(0, player.VelocityY, 0);
            // Lưu ý: Code gốc của bạn lưu VelocityY riêng, còn X/Z nằm trong MoveSpeed/Logic khác. 
            // Ở đây ta cần gộp lại để tính toán vector 3D chuẩn.

            Vector3 predictedPos = player.position + velocity * deltaTime;

            // 5. RÀNG BUỘC DÂY (The Constraint)
            // Kéo vị trí dự đoán về đúng bán kính dây
            Vector3 directionToAnchor = predictedPos - _anchorPoint;
            if (directionToAnchor.Length() > _ropeLength)
            {
                directionToAnchor = Vector3.Normalize(directionToAnchor) * _ropeLength;
                predictedPos = _anchorPoint + directionToAnchor;
            }

            // 6. Cập nhật lại Velocity thực tế dựa trên sự thay đổi vị trí
            // (Điều này giúp bảo toàn động lượng khi thả dây)
            Vector3 actualMove = predictedPos - player.position;
            player.VelocityY = actualMove.Y / deltaTime;

            // Cập nhật vị trí
            player.position = predictedPos;

            // 7. Quay nhân vật hướng vào điểm neo hoặc theo hướng di chuyển
            // player.rotation = ... (Logic quay mặt)
        }
    }
}
