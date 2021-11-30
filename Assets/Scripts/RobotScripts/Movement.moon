class Movement part of Robot {

    property canJump =    false     ;

    property jumpForce =     ;

    property leftMove;
    property rightMove;

    property yMove;

    everyFrame = {
        if (jumpKey.isPressed()) {
            jump();
        }
        
        // Becarful, really clever code below    
        // If press left go to Left    
        this.leftMove = key(     "Left"    ).isPressed() ?     1     :     0     ;
        // If press right go to Right
        this.rightMove = key(    "Right"   ).isPressed() ?     1     :     0     ;
    
        moveRobot();
    };

    function moveRobot() {
        Robot.x = Robot.x + this.rightMove - this.leftMove;
        Robot.y = Robot.y + this.yMove;
    }

    function jump()
    {
        if (player.foot.isOn("Ground") && canJump == true) {
            this.yMove = jumpForce;
        }
    }
}
